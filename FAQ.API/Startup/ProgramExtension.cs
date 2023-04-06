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
using AspNetCoreRateLimit;
using FAQ.SECURITY.ApplicationAuthorizationService.ServiceImplementation;
using FAQ.SECURITY.ApplicationAuthorizationService.ServiceInterface;
using Microsoft.Extensions.DependencyInjection;
using FAQ.SHARED.ServicesMessageResponse;
using Microsoft.Extensions.Options;
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
        ///     Injects all services
        /// </summary>
        /// <param name="Services"> Services collection </param>
        /// <param name="Configuration"> Configuration collection </param>
        /// <returns> Registered Services </returns>
        public static IServiceCollection InjectServices(this IServiceCollection Services, IConfiguration Configuration)
        {
            #region Default services
            Services.AddControllers();
            Services.AddMemoryCache();
            Services.AddEndpointsApiExplorer();
            #endregion

            #region CORS
            Services.AddCors(options =>
                {
                    options.AddPolicy(Configuration.GetSection("Cors:Policy:Name").Value!, builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
                });
            #endregion

            #region Settigs services
            Services.Configure<AuthenticationSettings>(Configuration.GetSection(AuthenticationSettings.SectionName));
            Services.Configure<EmailSettings>(Configuration.GetSection(EmailSettings.SectionName));
            Services.Configure<ServiceMessageResponseContainer>(Configuration.GetSection(ServiceMessageResponseContainer.SectionName));
            var jwtSetting = Configuration.GetSection(AuthenticationSettings.SectionName);

            #endregion

            #region Database and Identity services
            Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedEmail = true)
                            .AddEntityFrameworkStores<ApplicationDbContext>();
            #endregion

            #region Authentication services
            // Add the custom auth filter, a filter that is used by all enpoints
            Services.AddScoped<IApiKeyAuthorizationFilter>(provider =>
            {
                var config = provider.GetService<IConfiguration>();
                string apikey = config!.GetValue<string>("API_KEY")!;
                return new ApiKeyAuthorizationFilter(apikey);
            });
            // Cofigure Authetication
            Services.AddAuthentication(options =>
               {
                   options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

               })
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateAudience = Configuration.GetValue<bool>("Jwt:ValidateAudience"),
                       ValidateIssuer = Configuration.GetValue<bool>("Jwt:ValidateIssuer"),
                       ValidateLifetime = Configuration.GetValue<bool>("Jwt:ValidateLifetime"),
                       ValidateIssuerSigningKey = Configuration.GetValue<bool>("Jwt:ValidateIssuerSigningKey"),
                       ValidIssuer = jwtSetting.GetSection("Issuer").Value,
                       ValidAudience = jwtSetting.GetSection("Audience").Value,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.GetSection("Key").Value!)),
                   };
               });
            #endregion

            #region Swagger service

            Services.AddSwaggerGen(options =>
            {
                #region Application Auth

                // add support for api authentication, application level.
                options.AddSecurityDefinition(Configuration.GetSection("Swagger:ApplicationAuth:SecurityDefinition:Definition").Value, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = Configuration.GetSection("Swagger:ApplicationAuth:SecurityDefinition:Name").Value,
                    Type = SecuritySchemeType.ApiKey,
                    Description = Configuration.GetSection("Swagger:ApplicationAuth:SecurityDefinition:Description").Value
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = Configuration.GetSection("Swagger:ApplicationAuth:SecurityRequirement:Id").Value },
                            Name = Configuration.GetSection("Swagger:ApplicationAuth:SecurityRequirement:Name").Value,
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });

                #endregion

                #region Jwt Auth

                // add support for JWT Bearer token authentication, user level.
                options.AddSecurityDefinition(Configuration.GetSection("Swagger:JwtAuth:SecurityDefinition:Definition").Value, new OpenApiSecurityScheme
                {
                    Description = Configuration.GetSection("Swagger:JwtAuth:SecurityDefinition:Description").Value,
                    Name = Configuration.GetSection("Swagger:JwtAuth:SecurityDefinition:Name").Value,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = Configuration.GetSection("Swagger:JwtAuth:SecurityDefinition:Scheme").Value,
                    BearerFormat = Configuration.GetSection("Swagger:JwtAuth:SecurityDefinition:BearerFormat").Value
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = Configuration.GetSection("Swagger:JwtAuth:SecurityRequirement:Reference:Id").Value
                            },
                            Scheme = Configuration.GetSection("Swagger:JwtAuth:SecurityRequirement:Scheme").Value,
                            Name = Configuration.GetSection("Swagger:JwtAuth:SecurityRequirement:Name").Value,
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });

                #endregion

                options.SwaggerDoc(Configuration.GetSection("Swagger:Doc:Version").Value, new OpenApiInfo
                {
                    Version = Configuration.GetSection("Swagger:Doc:Version").Value,
                    Title = Configuration.GetSection("Swagger:Doc:Tittle").Value,
                    License = new OpenApiLicense
                    {
                        Name = Configuration.GetSection("Swagger:Doc:Licence:Name").Value,
                        Url = new Uri(Configuration.GetSection("Swagger:Doc:Licence:Url-Linkedin").Value!)
                    }
                });

                #region Xml Comments
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                options.IncludeXmlComments(xmlPath);
                #endregion
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

            #region Api Rate Limit

            // Limit 100 requests per minute for all endpoints. 
            Services.Configure<IpRateLimitOptions>(options =>
                {
                    options.GeneralRules = new List<RateLimitRule>
                    {
                        new RateLimitRule
                        {
                            Endpoint = "*",
                            Limit = 100,
                            PeriodTimespan = TimeSpan.FromMinutes(1)
                        }
                    };
                });

            Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();

            #endregion

            return Services;
        }

        #endregion
    }
}