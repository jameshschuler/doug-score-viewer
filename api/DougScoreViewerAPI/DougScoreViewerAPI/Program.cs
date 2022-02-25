using DougScoreViewerAPI.Entities;
using DougScoreViewerAPI.Middlewares;
using DougScoreViewerAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Environment.IsDevelopment() ? builder.Configuration.GetConnectionString("DougScoreDatabase") : 
    Environment.GetEnvironmentVariable("POSTGRESQLCONNSTR_DougScoreDatabase")!;

// Add services to the container.
builder.Services.AddDbContext<MyContext>(options =>
    options.UseNpgsql(connectionString));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddHealthChecks()
    .AddDbContextCheck<MyContext>();

builder.Services.AddScoped<IDougScoreService, DougScoreService>();
builder.Services.AddScoped<IDataService, DataService>();

builder.Services.AddCors();
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
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.MapControllers();

app.UseCors(policy => policy
    .SetIsOriginAllowed(origin => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseAuthorization();

app.Run();