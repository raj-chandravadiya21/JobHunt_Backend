using JobHunt.Application.Services;
using JobHunt.Domain.Entities;
using JobHunt.Application.Interfaces;
using JobHunt.Infrastructure.Repositories;
using JobHunt.Domain.Helper;
using JobHunt.Infrastructure.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//DATABASE CONNECTION
builder.Services.AddDbContext<DefaultdbContext>();

//SERVICES
builder.Services.AddScoped<IServiceBundle, ServiceBundle>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

//REPOSITORY
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//MAPPER CONNECTION
builder.Services.AddAutoMapper(typeof(MappingConfig));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//MIDDLEWARE EXCEPTION HANDLER
app.UseMiddleware<ExceptionHandlerMiddleware>();


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
