using WebApi.Config;
using WebApi.BL;
using WebApi.BE;
using WebApi.DAL;
using WebApi.BE.Mappers;
using WebApi.Common;
using WebApi.Common.Jwt;
using WebApi.DAL.Auth;

var builderOptions = new WebApplicationOptions 
{
    Args = args
};

var builder = WebApplication.CreateBuilder(builderOptions);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddSymmetricJwtAuthentication(configuration)
    .AddAuthorization();

builder.Services
    .AddSingleton<IConfigManager, ConfigManager>()
    .AddSingleton<IWebApiDatabase, WebApiDatabase>()
    .AddSingleton<IAccountsBL, AccountsBL>()
    .AddSingleton<IAccountsRepository, AccountsRepository>()
    .AddSingleton<IAccountsMapper, AccountsMapper>()
    .AddSingleton<ISectionBL, SectionBL>()
    .AddSingleton<ISectionsRepository, SectionRepository>()
    .AddSingleton<ISectionMapper, SectionMapper>()
    .AddSingleton<IAuthBL, AuthBL>()
    .AddSingleton<IAuthRepository, AuthRepository>()
    .AddSingleton<IAuthMapper, AuthMapper>();

builder.Services
    .AddTransient<ISectionModel, SectionModel>();

var application = builder.Build();

application.UseUrlLogger();
application.MapControllers();

application.Run();