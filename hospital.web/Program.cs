using Hospital.Models;
using Hospital.Repositories;
using Hospital.Repositories.Implementation;
using Hospital.Repositories.Interface;
using Hospital.Services;
using Hospital.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();



builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));

    options.EnableSensitiveDataLogging();

    options.LogTo(Console.WriteLine);
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IDbInitializer, DbInitializer>();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddTransient<IDoctorService, DoctorService>();
builder.Services.AddTransient<IHospitalInfo, HospitalInfoService>();
builder.Services.AddTransient<IRoomService, RoomService>();
builder.Services.AddScoped<IDoctorNoteService,
                           DoctorNoteService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();
builder.Services.AddScoped<

IDoctorStatisticsService,

DoctorStatisticsService

>();
builder.Services.AddScoped<IInsuranceService,
                           InsuranceService>();

builder.Services.AddTransient<IApplicationUserService, ApplicationUserService>();

builder.Services.AddScoped<IDoctorDashboardService, DoctorDashboardService>();
builder.Services.AddScoped<IPatientDashboardService, PatientDashboardService>();

builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<ILabService, LabService>();
builder.Services.AddScoped<IBillService, BillService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IMedicalFileService,
                           MedicalFileService>();
builder.Services.AddScoped<IInsuranceService,
                           InsuranceService>();
builder.Services.AddScoped<IMedicineService, MedicineService>();
builder.Services.AddScoped<IPatientReportService, PatientReportService>();
builder.Services.AddScoped<ISupplierService,
                           SupplierService>();

builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapRazorPages();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();