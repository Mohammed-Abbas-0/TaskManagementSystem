using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Text;
using TaskManagementSystem.Application.Commands.Classess;
using TaskManagementSystem.Application.OrganizingEndpoints;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Interface;
using TaskManagementSystem.Domain.ValueObjects;
using TaskManagementSystem.Infrastructures.Persistence;
using TaskManagementSystem.Infrastructures.Repositories;
using TaskManagementSystem.Interface.Dtos;
using TaskManagementSystem.Interface.Repositories;
using TaskManagementSystemAPI.Middlewares;


var builder = WebApplication.CreateBuilder(args);

// إعداد Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day) // تخزين في ملف يومي
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();

// استبدال اللوجر الافتراضي بـ Serilog
builder.Host.UseSerilog();
// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
builder.Services.AddSingleton<WhatsAppService>();

// Add Redis caching
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "TaskManagementSystem_";
});

// Add JWT Authentication
builder.Services
    .AddAuthentication(idx => {


        idx.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        idx.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        idx.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(idx =>
    {
        idx.RequireHttpsMetadata = false;
        idx.SaveToken = false;
        idx.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateLifetime = true, // تقدر تفحص ان مازال التوكن صالح قبل وقت ال expire
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true, 
            ValidAudience = builder.Configuration["JWT:Audience"],
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]??"")),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();
// Add MediatR
builder.Services.AddMediatR(typeof(CreateTaskCommand).Assembly);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(idx =>
{
    idx.SwaggerDoc("v1", new OpenApiInfo() { Title = "Api V1", Version = "v1.0" });

    // إعداد الأمان باستخدام Bearer
    var securitySchema = new OpenApiSecurityScheme
    {
        Description = "Enter 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
    };

    // إضافة تعريف الأمان
    idx.AddSecurityDefinition("Bearer", securitySchema);

    // إضافة متطلبات الأمان
    idx.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<CustomRequestMiddleware>();
app.UseMiddleware<RequestTimingMiddleware>();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    // استدعاء الدالة التي تقوم بتهيئة البيانات
    SeedData.SeedRolesAsync(userManager, roleManager).Wait();
}
app.MapTaskEndpoints();


app.MapControllers();

app.Run();
