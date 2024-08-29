using CourseManagement.Application.Interfaces;
using CourseManagement.Application.Services;
using CourseManagement.Domain.Repositories;
using CourseManagement.Infrastructure.DBContext;
using CourseManagement.Infrastructure.Membership;
using CourseManagement.Infrastructure.Repository;
using CourseManagement.Web;
using CourseManagement.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json.Serialization;

var Configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json").Build();

Log.Logger = new LoggerConfiguration()
                  .ReadFrom
                  .Configuration(Configuration)
                  .CreateBootstrapLogger();

try
{
    Log.Information("Application Starting...");

    var builder = WebApplication.CreateBuilder(args);
    
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");


    builder.Host.UseSerilog((context,LoggerConfig)=>
        LoggerConfig.MinimumLevel.Debug()
        .ReadFrom.Configuration(context.Configuration)
    );
    // Add services to the container.
    builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
    builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
    builder.Services.AddControllersWithViews();
    builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
    builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<ICourseManagementService, CourseManagementService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<ICourseOutLineManagementService, CourseOutLineManagementService>();
    builder.Services.AddScoped<CourseVM, CourseVM>();
    builder.Services.AddScoped<ICourseEnrollmentService, CourseEnrollmentService>();
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();

}
catch(Exception ex)
{
    Log.Fatal(ex, "Failed to start application.");
}
finally
{
    Log.CloseAndFlush();
}