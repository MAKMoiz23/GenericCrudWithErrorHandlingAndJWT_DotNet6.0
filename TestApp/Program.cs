using DataAccess.Data.IDataModel;
using DataAccess.Data.DataModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using TestApp.Middleware;
using TestApp.Controllers;
using DataAccess.Services.Service;
using DataAccess.Services.IService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
//{
//    options.RequireHttpsMetadata = false;
//    options.SaveToken = true;
//    options.TokenValidationParameters = new TokenValidationParameters()
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidAudience = builder.Configuration["Jwt:Audience"],
//        ValidIssuer = builder.Configuration["Jwt:Issuer"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//    };
//});

//var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
//builder.Services.AddAuthentication(x =>
//{
//    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(x =>
//{
//    x.RequireHttpsMetadata = false;
//    x.SaveToken = false;
//    x.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(key),
//        ValidateIssuer = false,
//        ValidateAudience = false,
//    };
//});

//builder.Services.AddSwaggerGen(option =>
//{
//    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
//    {
//        Name = "Authorization",
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = "Bearer",
//        BearerFormat = "JWT",
//        In = ParameterLocation.Header,
//        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
//    });
//    option.AddSecurityRequirement(new OpenApiSecurityRequirement
//                     {
//                         {
//                                 new OpenApiSecurityScheme
//                                 {
//                                     Reference = new OpenApiReference
//                                     {
//                                         Type = ReferenceType.SecurityScheme,
//                                         Id = "Bearer"
//                                     }
//                                 },
//                                 new string[] {}
//                         }
//                     });
//});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region Cors
builder.Services.AddCors();
#endregion

#region Services
builder.Services.AddScoped<IGenericCrudServices, GenericCrudServices>();
builder.Services.AddScoped<IAuthData, AuthData>();
builder.Services.AddScoped<IBrandData, BrandData>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<JWTMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

//app.UseAuthorization();

app.MapControllers();

app.Run();
