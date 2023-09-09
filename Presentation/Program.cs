using Business.Services.Abstract.Admin;
using Business.Services.Concrete.Admin;
using Business.Utilities.File;
using Common.Entities;
using DataAccess;
using DataAccess.Contexts;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("DataAccess")));
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredUniqueChars = 0;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

builder.Services.AddScoped<ISliderRepository, SliderRepository>();
builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddSingleton<IFileService, FileService>();
builder.Services.AddScoped<IOurVisionRepository, OurVisionRepository>();
builder.Services.AddScoped<IOurVisionService, OurVisionService>();
builder.Services.AddScoped<IOurVisionGoalRepository, OurVisionGoaRepository>();
builder.Services.AddScoped<IOurVisionGoalService, OurVisionGoalService>();
builder.Services.AddScoped<IWhatWeDoRepository, WhatWeDoRepository>();
builder.Services.AddScoped<IWhatWeDoService, WhatWeDoService>();
builder.Services.AddScoped<IAboutUsImagesRepository, AboutUsImagesRepository>();
builder.Services.AddScoped<IAboutUsImagesService, AboutUsImagesService>();
builder.Services.AddScoped<IAboutUsViewComponentRepository, AboutUsViewComponentRepository>();
builder.Services.AddScoped<IAboutUsViewComponentService, AboutUsViewComponentService>();



builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var ourVisionRepository = scope.ServiceProvider.GetService<IOurVisionRepository>();
    var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();
    await DbInitializer.SeedAsync(ourVisionRepository, unitOfWork);
}
    


app.MapControllerRoute(
		  name: "areas",
		  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
		);
app.MapDefaultControllerRoute();
app.UseStaticFiles();
app.Run();



