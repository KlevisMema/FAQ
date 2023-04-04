#region Usings
using System.Text;
using FAQ.DAL.Models;
using FAQ.DTO.Mappings;
using FAQ.DAL.DataBase;
using System.Reflection;
using FAQ.EMAIL.EmailService;
using Microsoft.OpenApi.Models;
using FAQ.HELPERS.Helpers.Email;
using FAQ.LOGGER.ServiceInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using FAQ.LOGGER.ServiceImplementation;
using FAQ.ACCOUNT.AuthenticationService;
using FAQ.EMAIL.EmailService.ServiceInterface;
using FAQ.ACCOUNT.AuthorizationService.Interfaces;
using FAQ.ACCOUNT.AccountService.ServiceInterface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FAQ.ACCOUNT.AuthorizationService.Implementation;
using FAQ.ACCOUNT.AccountService.ServiceImplementation;
using FAQ.ACCOUNT.AuthenticationService.ServiceInterface;
using FAQ.ACCOUNT.AuthenticationService.ServiceImplementation;
using FAQ.API.ControllerResponse;
#endregion

namespace FAQ.API.Startup
{
    /// <summary>
    ///     A static class that provides an extension of services registration before app is build.
    /// </summary>
    public static class ProgramExtension
    {
        #region Method

        /// <summary>
        ///     Injects all services method
        /// </summary>
        /// <param name="Services"> Services collection </param>
        /// <param name="Configuration"> Configuration collection </param>
        /// <returns> Registered Services </returns>
        public static IServiceCollection InjectServices(this IServiceCollection Services, IConfiguration Configuration)
        {
            #region Default services
            Services.AddControllers();
            Services.AddEndpointsApiExplorer();
            #endregion

            #region Settigs services
            Services.Configure<AuthenticationSettings>(Configuration.GetSection("Jwt"));
            Services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            #endregion

            var jwtSetting = Configuration.GetSection("Jwt");

            #region Database and identity services
            Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedEmail = true)
                            .AddEntityFrameworkStores<ApplicationDbContext>();
            #endregion

            #region Authentication services
            Services.AddAuthentication(options =>
               {
                   options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

               })
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateAudience = true,
                       ValidateIssuer = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = jwtSetting.GetSection("Issuer").Value,
                       ValidAudience = jwtSetting.GetSection("Audience").Value,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.GetSection("Key").Value!)),
                   };
               });
            #endregion

            #region Swagger service
            Services.AddSwaggerGen(options =>
               {
                   options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                   {
                       Description = @"Enter 'Bearer' [Space] and then your token in the input field below. 
                                    Example : 'Bearer 1234dsfhj'",
                       Name = "Authorization",
                       In = ParameterLocation.Header,
                       Type = SecuritySchemeType.ApiKey,
                       Scheme = "Bearer",
                       BearerFormat = "Jwt"
                   });

                   options.AddSecurityRequirement(new OpenApiSecurityRequirement
                  {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "0auth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                  });

                   options.SwaggerDoc("v1", new OpenApiInfo
                   {
                       Version = "v1",
                       Title = "FAQ API",
                       License = new OpenApiLicense
                       {
                           Name = "Web Api created by Klevis Mema",
                           Url = new Uri("https://www.linkedin.com/in/klevis-m-ab1b3b140/")
                       }
                   });

                   var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                   var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                   options.IncludeXmlComments(xmlPath);
               });
            #endregion

            #region Automapper services
            Services.AddAutoMapper(typeof(UserMappings));
            #endregion

            #region BLL, ACCOUNT, LOG and StatusCodeResponse Services registration.
            Services.AddTransient<ILogService, LogService>();
            Services.AddTransient<IEmailSender, EmailSender>();
            Services.AddTransient<ILoginService, LoginService>();
            Services.AddTransient<IAccountService, AccountService>();
            Services.AddTransient<IRegisterService, RegisterService>();
            Services.AddTransient<IOAuthJwtTokenService, OAuthJwtTokenService>();
            #endregion

            return Services;
        }

        #endregion
    }
}