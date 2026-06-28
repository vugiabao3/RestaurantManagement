

using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestaurantManagement.Application.Categories.Queries.GetAllCategories;
using RestaurantManagement.Application.Interfaces;
using RestaurantManagement.Infrastructure.Authentication;
using RestaurantManagement.Infrastructure.Persistence;
using RestaurantManagement.Infrastructure.Repositories;
using RestaurantManagement.Infrastructure.Services;
using System.Reflection;
using System.Text;
using RestaurantManagement.Application.Interfaces.Kitchen;
using RestaurantManagement.Infrastructure.Services.Kitchen;
using RestaurantManagement.Infrastructure.BackgroundJobs;
using RestaurantManagement.API.Middlewares;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


// thêm nhập token

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "Restaurant Management API",
            Version = "v1"
        });

    options.AddSecurityDefinition("Bearer",
        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "Nhập JWT Token"

            // ví dụ:
            // eyJhbGciOi...
        });

    options.AddSecurityRequirement(
        new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference =
                        new Microsoft.OpenApi.Models.OpenApiReference
                        {
                            Type =
                                Microsoft.OpenApi.Models.ReferenceType
                                    .SecurityScheme,

                            Id = "Bearer"
                        }
                },

                Array.Empty<string>()
            }
        });
});

//cho fe truy cập
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
/// thêm `MediatR`

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(
        Assembly.Load(
            "RestaurantManagement.Application"));
});

builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IEmailService, EmailService>();

//
builder.Services.AddScoped<IDishRepository, DishRepository>();
// di
builder.Services.AddScoped<
    IReportRepository,
    ReportRepository>();


builder.Services.AddScoped<
    IMenuRepository,
    MenuRepository>();

// thêm DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString(
            "DefaultConnection"));
});
//thêm repository
builder.Services.AddScoped< ICategoryRepository,CategoryRepository>();

builder.Services.AddScoped<
    IUserRepository,
    UserRepository>();

builder.Services.AddScoped<
    IJwtTokenGenerator,
    JwtTokenGenerator>();

// Kitchen tracking status module
builder.Services.AddScoped<IKitchenTrackingService, KitchenTrackingService>();
builder.Services.AddHostedService<DelayAutoDetectJob>();

builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer =
                    builder.Configuration["Jwt:Issuer"],

                ValidAudience =
                    builder.Configuration["Jwt:Audience"],

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration["Jwt:Key"]!))
            };
    });

builder.Services.AddAuthorization();



var app = builder.Build();
app.UseCors("AllowReact");

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
