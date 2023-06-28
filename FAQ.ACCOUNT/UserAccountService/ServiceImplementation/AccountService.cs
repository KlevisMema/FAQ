#region Usings
using FAQ.DAL.Models;
using FAQ.DAL.DataBase;
using FAQ.SHARED.ResponseTypes;
using FAQ.LOGGER.ServiceInterface;
using Microsoft.AspNetCore.Identity;
using FAQ.ACCOUNT.AccountService.ServiceInterface;
using FAQ.SHARED.ServicesMessageResponse;
using Microsoft.Extensions.Options;
using FAQ.DTO.UserDtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using FAQ.SECURITY.UserAccountService.Settings;
using FAQ.EMAIL.EmailService.ServiceInterface;
#endregion

namespace FAQ.ACCOUNT.AccountService.ServiceImplementation
{
    /// <summary>
    ///     A Service class providing account functionalities by implementing IAccountService interface.
    /// </summary>
    public class AccountService : IAccountService
    {
        #region Constructor and Properties
        /// <summary>
        ///     Log service
        /// </summary>
        private readonly ILogService _log;
        /// <summary>
        ///     User Manager service
        /// </summary>
        private readonly UserManager<User> _userManager;
        /// <summary>
        ///     Database Context
        /// </summary>
        private readonly ApplicationDbContext _db;
        /// <summary>
        ///     Message response register 
        /// </summary>
        private AccountMessageResponse _accountMessageResponse { get; set; } = new();
        /// <summary>
        ///     Exception message response
        /// </summary>
        private string ExceptionMessageResponse { get; set; }
        /// <summary>
        ///     Option settings for <see cref="ProfilePictureImagePath"/>
        /// </summary>
        private readonly IOptions<ProfilePictureImagePath> _optionsProfilePicturePath;
        /// <summary>
        ///     A <see cref="UploadProfilePictureMessageResponse"/> object
        /// </summary>
        private UploadProfilePictureMessageResponse _uploadProfilePictureMessageResponse = new();
        /// <summary>
        /// 
        /// </summary>
        private readonly IEmailSender _emailSender;

        /// <summary>
        ///     The <see cref="AccountService"/> constructor
        /// </summary>
        /// <param name="log"> Log Service </param>
        /// <param name="userManager"> User Manager Service </param>
        /// <param name="db"> Database Context </param>
        /// <param name="emailSender"></param>
        /// <param name="messageResponses"> Message response </param>
        /// <param name="optionsProfilePicturePath"> Option settings <see cref="ProfilePictureImagePath"/> </param>
        public AccountService
        (
            ILogService log,
            ApplicationDbContext db,
            IEmailSender emailSender,
            UserManager<User> userManager,
            IOptions<ServiceMessageResponseContainer> messageResponses,
            IOptions<ProfilePictureImagePath> optionsProfilePicturePath
        )
        {
            _db = db;
            _log = log;
            _userManager = userManager;
            _emailSender = emailSender;
            _optionsProfilePicturePath = optionsProfilePicturePath;
            ExceptionMessageResponse = messageResponses.Value.Exception;
            _accountMessageResponse = messageResponses.Value.AccountMessageResponse!;
            _uploadProfilePictureMessageResponse = messageResponses.Value.UploadProfilePictureMessageResponse!;
        }
        #endregion

        #region Methods implementation
        /// <summary>
        ///     Confirm email of a user, method implementation.
        /// </summary>
        /// <param name="userId"> Id of the user of type <see cref="string"/> </param>
        /// <param name="otp"> otp of type <see cref="string"/>  </param>
        /// <returns> A Object response of type : <see cref="CommonResponse{T}"/> where T is <seealso cref="string"/> </returns>
        public async Task<CommonResponse<string>> 
        ConfirmEmail
        (
            string userId,
            string otp
        )
        {
            try
            {
                if (String.IsNullOrEmpty(otp))
                    return CommonResponse<string>.Response(_accountMessageResponse.OtpEmpty, false, System.Net.HttpStatusCode.NotFound, otp);

                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                    return CommonResponse<string>.Response(_accountMessageResponse.UserNotFound, false, System.Net.HttpStatusCode.NotFound, string.Empty);

                if (!user.OTP.Equals(otp))
                    return CommonResponse<string>.Response(_accountMessageResponse.OtpCodeIncorrect, false, System.Net.HttpStatusCode.NotFound, otp);

                user.EmailConfirmed = true;

                _db.Users.Update(user);
                await _db.SaveChangesAsync();

                await _log.CreateLogAction($"User with id {userId} updated confiremd his email", "ConfirmEmail", Guid.Parse(userId));

                return CommonResponse<string>.Response(_accountMessageResponse.AccountConfirmed, true, System.Net.HttpStatusCode.OK, otp);

            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "Log In", Guid.Parse(userId));

                await _emailSender.SendEmailToDevTeam(Guid.Parse(userId));

                return CommonResponse<string>.Response(ExceptionMessageResponse, false, System.Net.HttpStatusCode.InternalServerError, string.Empty);
            }
        }

        /// <summary>
        ///     Upload profile picture for the user, method implementation.
        /// </summary>
        /// <param name="userId"> <see cref="Guid"/> ID of the user </param>
        /// <param name="picUpload"> <see cref="DtoProfilePicUpload"/> object </param>
        /// <param name="webHostEnvironment"> <see cref="IWebHostEnvironment"/> information </param>
        /// <returns> <see cref="CommonResponse{T}"/> where T is <see cref="DtoProfilePicUpload"/> </returns>
        public async Task<CommonResponse<DtoProfilePicUpload>> 
        UploadProfilePicture
        (
            Guid userId,
            DtoProfilePicUpload picUpload,
            IWebHostEnvironment webHostEnvironment
        )
        {
            try
            {
                if (picUpload.FormFile is null)
                    return CommonResponse<DtoProfilePicUpload>.Response(_uploadProfilePictureMessageResponse.ProfilePicNull, false, System.Net.HttpStatusCode.BadRequest, picUpload);

                var user = await _userManager.FindByIdAsync(userId.ToString());

                if (user == null)
                    return CommonResponse<DtoProfilePicUpload>.Response(_accountMessageResponse.UserNotFound, false, System.Net.HttpStatusCode.NotFound, null);

                string wwwRootPath = webHostEnvironment.WebRootPath;

                var imagePath = wwwRootPath + _optionsProfilePicturePath.Value.ProfilePicturePathV2 + user.ProfilePicture;

                if (File.Exists(imagePath))
                    File.Delete(imagePath);

                if (!Directory.Exists(Path.Combine(wwwRootPath + _optionsProfilePicturePath.Value.ProfilePicturePath)))
                    Directory.CreateDirectory(wwwRootPath + _optionsProfilePicturePath.Value.ProfilePicturePath);

                string extension = Path.GetExtension(picUpload.FormFile.FileName);
                string path = Path.Combine(wwwRootPath + _optionsProfilePicturePath.Value.ProfilePicturePath, user.Id + extension);

                using (var fileSteam = new FileStream(path, FileMode.Create))
                {
                    await picUpload.FormFile.CopyToAsync(fileSteam);
                }

                user.ProfilePicture = user.Id + extension;
                var updateUserResult = await _userManager.UpdateAsync(user);

                if (!updateUserResult.Succeeded)
                    return IdentityResponse<DtoProfilePicUpload>.Response
                        (
                            _uploadProfilePictureMessageResponse.Unsuccsessfull,
                            false,
                            System.Net.HttpStatusCode.BadRequest,
                            picUpload,
                            updateUserResult.Errors
                        );

                await _log.CreateLogAction($"User with id {userId} updated his profile picture", "UploadProfilePicture", userId);

                return CommonResponse<DtoProfilePicUpload>.Response(_uploadProfilePictureMessageResponse.Succsessfull, true, System.Net.HttpStatusCode.OK, picUpload);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "UploadProfilePicture", userId);

                await _emailSender.SendEmailToDevTeam(userId);

                return CommonResponse<DtoProfilePicUpload>.Response(ExceptionMessageResponse, false, System.Net.HttpStatusCode.InternalServerError, picUpload);
            }
        }
        #endregion
    }
}