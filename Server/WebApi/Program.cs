using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebApi;
using WebApi.Config;
using WebApi.BL;
using WebApi.BE;
using WebApi.DAL;
using WebApi.BE.Mappers;
using WebApi.Common;
using WebApi.Common.Helpers;

var builderOptions = new WebApplicationOptions 
{
    Args = args
};

var builder = WebApplication.CreateBuilder(builderOptions);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => 
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER_TOKEN,
            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE_TOKEN,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true
        };
    });

builder.Services
    .AddAuthorization();

builder.Services
    .AddSingleton<IConfigManager, ConfigManager>()
    .AddSingleton<IUsersBL, UsersBL>()
    .AddSingleton<IUsersRepository, UsersRepository>()
    .AddSingleton<IWebApiDatabase, WebApiDatabase>()
    .AddSingleton<IUsersMappers, UsersMappers>()
    .AddSingleton<IDataBaseHelpers, DataBaseHelpers>()
    .AddSingleton<ISectionBL, SectionBL>()
    .AddSingleton<ISectionsRepository, SectionRepository>()
    .AddSingleton<ISectionMapper, SectionMapper>();

builder.Services
    .AddTransient<IUserModel, UserModel>()
    .AddTransient<IUserResponse, UserResponse>()
    .AddTransient<ISectionModel, SectionModel>();

var application = builder.Build();

application.UseUrlLogger();
application.MapControllers();

application.Run();