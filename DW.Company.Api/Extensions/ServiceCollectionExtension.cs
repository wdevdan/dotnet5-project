using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using DW.Company.Api.Helpers;
using DW.Company.Api.Services;
using DW.Company.Contracts.Data;
using DW.Company.Contracts.Helpers;
using DW.Company.Contracts.Services;
using DW.Company.Contracts.Settings;
using DW.Company.Data;
using DW.Company.Entities.Value;
using DW.Company.Services;
using DW.Company.Services.Helpers;
using DW.Company.Services.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace DW.Company.Api.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void Injections(this IServiceCollection services)
        {
            // db
            services.AddScoped<IDBContext, DBContext>();
            // settings
            services.AddScoped<IDBSettings, DBSettings>();
            services.AddScoped<ISessionSettings, SessionSettings>();
            services.AddSingleton<IMasterSettings, MasterSettings>();
            services.AddSingleton<IEnvironmentSettings, EnvironmentSettings>();
            // helpers
            services.AddTransient<IDBHelper, DBHelper>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IDocumentValidator, DocumentValidator>();
            // services
            services.AddTransient<IImageHandlerService, ImageHandlerService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ILeatherService, LeatherService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ICustomerProductService, CustomerProductService>();
            services.AddTransient<IDesignerService, DesignerService>();
            services.AddTransient<IItemVersionService, ItemVersionService>();
            services.AddTransient<ILeatherTypeService, LeatherTypeService>();
        }

        public static void Mapper(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(UserAutoMapperProfile),
                typeof(ProductAutoMapperProfile),
                typeof(LeatherAutoMapperProfile),
                typeof(CustomerAutoMapperProfile),
                typeof(FileAutoMapperProfile),
                typeof(DesignerAutoMapperProfile),
                typeof(CategoryAutoMapperProfile),
                typeof(ItemVersionAutoMapperProfile)
            );
        }

        public static void Authentication(this IServiceCollection services, IConfiguration configuration)
        {
            var _section = configuration.GetSection("Jwt");
            var _keySecret = _section.GetValue(typeof(string), "KeySecret").ToString();
            var _key = Encoding.ASCII.GetBytes(_keySecret);
            services.AddAuthentication(
                    x =>
                    {
                        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    }
                )
                .AddJwtBearer(
                    x =>
                    {
                        x.RequireHttpsMetadata = false;
                        x.SaveToken = true;
                        x.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(_key),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    }
                );
        }

        public static void Options(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<HashingOptions>(options => configuration.GetSection("HashingOptions").Bind(options));
        }

        public static void Swagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc(
                        "v1", 
                        new OpenApiInfo { 
                            Title = "DW.Company", 
                            Version = "v1",
                            Contact = new OpenApiContact
                            {
                                Name = "DW Developer",
                                Email = "contato@dwdeveloper.com.br",
                                Url = new Uri("http://www.dwdeveloper.com.br"),
                            },
                        }
                    );
                    c.AddSecurityDefinition(
                        "Bearer",
                        new OpenApiSecurityScheme
                        {
                            Description = @"JWT Authorization Bearer. Example: 'Bearer 12345678abcdefdef'",
                            Name = "Authorization",
                            In = ParameterLocation.Header,
                            Type = SecuritySchemeType.ApiKey,
                            Scheme = "Bearer"
                        }
                    );

                    c.AddSecurityRequirement(
                        new OpenApiSecurityRequirement() {
                            {
                                new OpenApiSecurityScheme
                                    {
                                        Reference = new OpenApiReference
                                            {
                                                Type = ReferenceType.SecurityScheme,
                                                Id = "Bearer"
                                            },
                                        Scheme = "oauth2",
                                        Name = "Bearer",
                                        In = ParameterLocation.Header,
                                    },
                                new List<string>()
                            }
                        }
                    );
                }
            );
        }

    }
}
