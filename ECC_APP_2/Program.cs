using ECC_APP_2.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Register logging
        builder.Services.AddLogging(logging =>
        {
            logging.AddConsole(); // Enable console logging
            logging.AddDebug();   // Enable debug logging
        });

        // Register HttpClient services
        builder.Services.AddHttpClient<MentorService>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7187/"); // Your API base URL
        });

        builder.Services.AddHttpClient<StudentService>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7187/");
        });

        builder.Services.AddHttpClient<FundingGuideService>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7187/");
        });

        builder.Services.AddHttpClient<BusinessProposalService>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7187/");
        });

        builder.Services.AddHttpClient<AdminService>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7187/");
        });

        // Configure CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });

        // Configure session management
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors("AllowAll"); // Apply CORS policy
        app.UseSession(); // Must be before UseAuthorization
        app.UseAuthorization();

        // Set up routing
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
