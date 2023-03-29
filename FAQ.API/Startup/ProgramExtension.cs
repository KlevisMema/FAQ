using FAQ.DAL.DataBase;
using FAQ.DAL.Models;
using FAQ.SERVICES.AuthenticationService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using FAQ.DTO.Mappings;
using FAQ.SERVICES.AuthorizationService.Interfaces;
using FAQ.SERVICES.AuthorizationService.Implementation;
using FAQ.SERVICES.AuthenticationService.ServiceInterface;
using FAQ.SERVICES.AuthenticationService.ServiceImplementation;

namespace FAQ.API.Startup
{
    public static class ProgramExtension
    {
        public static IServiceCollection InjectServices(this IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddControllers();
            Services.AddEndpointsApiExplorer();

            Services.Configure<AuthenticationSettings>(Configuration.GetSection("Jwt"));

            var jwtSetting = Configuration.GetSection("Jwt");

            Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedEmail = false)
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

            Services.AddTransient<ILoginService, LoginService>();
            Services.AddTransient<IRegisterService, RegisterService>();
            Services.AddTransient<IOAuthJwtTokenService, OAuthJwtTokenService>();

            return Services;
        }
    }
}