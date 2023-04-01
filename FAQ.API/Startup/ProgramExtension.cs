﻿#region Usings
using System.Text;
using FAQ.DAL.Models;
using FAQ.DTO.Mappings;
using FAQ.DAL.DataBase;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using FAQ.ACCOUNT.AuthenticationService;
using FAQ.SERVICES.RepositoryService.Interfaces;
using FAQ.ACCOUNT.AuthorizationService.Interfaces;
using FAQ.SERVICES.RepositoryService.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FAQ.ACCOUNT.AuthenticationService.ServiceInterface;
using FAQ.ACCOUNT.AuthenticationService.ServiceImplementation;
using FAQ.ACCOUNT.AuthorizationService.Implementation;
using FAQ.ACCOUNT.AccountService.ServiceInterface;
using FAQ.ACCOUNT.AccountService.ServiceImplementation;
#endregion

namespace FAQ.API.Startup
{
    /// <summary>
    ///     A static class that provides a extension of services registration
    /// </summary>
    public static class ProgramExtension
    {
        #region Method

        /// <summary>
        ///     Injects all services method
        /// </summary>
        /// <param name="Services">Services</param>
        /// <param name="Configuration">Configuration</param>
        /// <returns> Registered Services </returns>
        public static IServiceCollection InjectServices(this IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddControllers();
            Services.AddEndpointsApiExplorer();

            Services.Configure<AuthenticationSettings>(Configuration.GetSection("Jwt"));

            var jwtSetting = Configuration.GetSection("Jwt");

            Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedEmail = true)
                            .AddEntityFrameworkStores<ApplicationDbContext>();

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

            Services.AddAutoMapper(typeof(UserMappings));

            Services.AddTransient<ILogService, LogService>();
            Services.AddTransient<ILoginService, LoginService>();
            Services.AddTransient<IAccountService, AccountService>();
            Services.AddTransient<IRegisterService, RegisterService>();
            Services.AddTransient<IOAuthJwtTokenService, OAuthJwtTokenService>();

            return Services;
        }

        #endregion
    }
}