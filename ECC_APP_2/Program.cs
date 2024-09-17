using ECC_APP_2.Models;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);



        // Register HttpClient and StudentService
        builder.Services.AddHttpClient<StudentService>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7187/"); // Correct base URL for your API
        });

        builder.Services.AddHttpClient<FundingGuideService>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7187/"); // Your API base URL
        });

        builder.Services.AddHttpClient<BusinessProposalService>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7187/"); // Your API base URL
        });

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
        app.UseAuthorization();
        app.UseSession(); // Add this line to use sessions

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
