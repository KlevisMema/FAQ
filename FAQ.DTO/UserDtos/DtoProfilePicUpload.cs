#region Usings
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
#endregion

namespace FAQ.DTO.UserDtos
{
    /// <summary>
    ///     A dto class used for user profile image  upload. 
    /// </summary>
    public class DtoProfilePicUpload
    {
        /// <summary>
        ///     A <see cref="IFormFile"/>  field  that  will 
        ///     hold the image file. It's nullable and required 
        ///     when used in a form etc.
        /// </summary>
        [Required(ErrorMessage = "Image is required")]
        public IFormFile? FormFile { get; set; }
    }
}