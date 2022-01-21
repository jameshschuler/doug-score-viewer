using AutoMapper;
using DougScoreViewerAPI.Entities;
using DougScoreViewerAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Environment.IsDevelopment() ? builder.Configuration.GetConnectionString("DougScoreDatabase") : 
    Environment.GetEnvironmentVariable("POSTGRESQLCONNSTR_DougScoreDatabase")!;

// Add services to the container.
builder.Services.AddDbContext<MyContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddHealthChecks()
    .AddDbContextCheck<MyContext>();

builder.Services.AddScoped<IDougScoreService, DougScoreService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();

app.MapHealthChecks("/health");
 
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();