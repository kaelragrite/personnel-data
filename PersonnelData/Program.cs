using System.Resources;
using Microsoft.EntityFrameworkCore;
using PersonnelData.Config.ActionFilters;
using PersonnelData.Config.Middlewares;
using PersonnelData.Data;
using PersonnelData.Data.DdSeed;

var builder = WebApplication.CreateBuilder(args);

// Add Services
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPhoneRepository, PhoneRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddLocalization(/*options => options.ResourcesPath = "Resources"*/);

builder.Services
    .AddControllers(options => { options.Filters.Add(new ValidateModelAttribute()); })
    .AddDataAnnotationsLocalization(options => 
    {
        options.DataAnnotationLocalizerProvider = (_, factory) => factory.Create(typeof(PersonnelData.Resources.SharedResources));
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); });

builder.Services.AddSingleton<ResourceManager>(_ => new ResourceManager(typeof(PersonnelData.Resources.SharedResources)));

var app = builder.Build();

// Seed Data
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<ApplicationDbContext>();
SeedData.Initialize(context); // Seed the data
scope.Dispose();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline
var supportedCultures = new[] { "en-us", "ka-ge", "en", "ka" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("ka")
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<AcceptLanguageMiddleware>();
app.MapControllers();

app.Run();