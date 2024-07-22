using JobHunt.Application.Services;
using JobHunt.Domain.Entities;
using JobHunt.Application.Interfaces;
using JobHunt.Infrastructure.Repositories;
using JobHunt.Domain.Helper;
using JobHunt.Infrastructure.Interfaces;
using Microsoft.OpenApi.Models;
using JobHunt.Application.HostServices;
using JobHunt.Application.Interfaces.IHostHelper;
using JobHunt.Application.Services.HostHelper;
using Serilog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day).CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = "Bearer API Security Scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
        {
            Reference=new OpenApiReference
            {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer"
            },
            Scheme="auth1",
            Name="Bearer",
            In=ParameterLocation.Header,
        },
        new List<string>()
        }
    });
});



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});

//DATABASE CONNECTION
builder.Services.AddDbContext<DefaultdbContext>();

builder.Services.AddHostedService<DailyEmailSender>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//SERVICES
builder.Services.AddScoped<IServiceBundle, ServiceBundle>();
builder.Services.AddSingleton<IEmailSender, EmailSender>();
builder.Services.AddSingleton<IHostHelper, HostHelper>();

//REPOSITORY
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//MAPPER CONNECTION
builder.Services.AddAutoMapper(typeof(MappingConfig));

var app = builder.Build();

LogHelper.SetLogger(app.Logger);
ConfigurationHelper.Configure(app.Configuration);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//MIDDLEWARE EXCEPTION HANDLER
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
