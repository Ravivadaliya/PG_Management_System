using DatabaseHelperLibrary;
using Hangfire;
using Hangfire.SqlServer;
using PG_Management_System.Areas.PG_Person.Controllers;
using PG_Management_System.Areas.PG_Person.Data;
using PG_Management_System.Areas.PG_Person.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//// Add Hangfire services
//builder.Services.AddHangfire(config =>
//{
//    config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
//          .UseSimpleAssemblyNameTypeSerializer()
//          .UseRecommendedSerializerSettings()
//          .UseSqlServerStorage(builder.Configuration.GetConnectionString("myConnectionStrings"), new SqlServerStorageOptions
//          {
//              CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
//              SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
//              QueuePollInterval = TimeSpan.Zero,
//              UseRecommendedIsolationLevel = true,
//              DisableGlobalLocks = true
//          });
//});

//// Add Hangfire server
//builder.Services.AddHangfireServer();


builder.Services.AddSingleton<DatabaseHelper>(sp =>
    new DatabaseHelper(builder.Configuration.GetConnectionString("myConnectionStrings")));
builder.Services.AddHostedService<DatabaseService>(); // Register the lifecycle service

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(1800);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();

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
app.UseSession();
app.UseAuthorization();

#pragma warning disable ASP0014 // Suggest using top level route registrations
app.UseEndpoints(endpoints =>
{
    // Custom route to access action directly, hiding Area and Controller in the URL
    endpoints.MapControllerRoute(
        name: "Pg_hostel_route",
        pattern: "{action}/{id?}",  // Clean URL format
        defaults: new { controller = "PG_Hostel", area = "PG_Hostel" }  // Set the default controller and area
    );
    endpoints.MapControllerRoute(
        name: "Pg_Owner_route",
        pattern: "{action}/{id?}",
        defaults: new { controller = "PG_Owner", area = "PG_Owner" }
   );
    endpoints.MapControllerRoute(
      name: "Pg_owner_route",
      pattern: "{action}/{id?}",
      defaults: new { controller = "PG_Issues", area = "PG_Issues" }
    );
    endpoints.MapControllerRoute(
      name: "Pg_Staff_route",
      pattern: "{action}/{id?}",
      defaults: new { controller = "PG_Staff", area = "PG_Staff" }
    );
    endpoints.MapControllerRoute(
      name: "Pg_Person_route",
      pattern: "{action}/{id?}",
      defaults: new { controller = "PG_Person", area = "PG_Person" }
    );
    endpoints.MapControllerRoute(
      name: "Pg_Room_route",
      pattern: "{action}/{id?}",
      defaults: new { controller = "PG_Room", area = "PG_Room" }
    );
    endpoints.MapControllerRoute(
      name: "Pg_Bed_route",
      pattern: "{action}/{id?}",
      defaults: new { controller = "PG_Bed", area = "PG_Bed" }
    );
    endpoints.MapControllerRoute(
     name: "Pg_Announcements_route",
     pattern: "{action}/{id?}",
     defaults: new { controller = "PG_Announcements", area = "PG_Announcements" }
    );
    endpoints.MapControllerRoute(
     name: "Pg_Announcements_route",
     pattern: "{action}/{id?}",
     defaults: new { controller = "PG_Payments", area = "PG_Payments" }
    );
    endpoints.MapControllerRoute(
     name: "Pg_Login_route",
     pattern: "{action}/{id?}",
     defaults: new { controller = "Login", area = "Login" }
    );
    endpoints.MapControllerRoute(
    name: "Pg_User_route",
    pattern: "{action}/{id?}",
    defaults: new { controller = "User", area = "User" }
   );
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{area}/{controller}/{action}/{id?}");
});
#pragma warning restore ASP0014 // Suggest using top level route registrations

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Dashboard}/{id?}");


//using (var scope = app.Services.CreateScope())
//{
//    var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
//    recurringJobManager.AddOrUpdate<PG_PersonController>(
//        "generate-payment-requests",
//        service => service.GeneratePaymentRequests(),
//        "*/2 * * * *");
//}
app.Run();
