using hospital;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<dental_hospitalContext>(options =>
{
    options.UseNpgsql("Host=localhost;Port=5432;Database=dental_hospital;Username=postgres;Password=51543001");
});
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "dentalhoes",
    pattern: "{controller=DentalHoes}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "doctors",
    pattern: "{controller=Doctors}/{action=Doctors}/{id?}");
app.MapControllerRoute(
    name: "diagnoses",
    pattern: "{controller=Diagnoses}/{action=Diagnoses}/{id?}");
app.MapControllerRoute(
    name: "patients",
    pattern: "{controller=Patients}/{action=Patients}/{id?}");
app.MapControllerRoute(
    name: "plan",
    pattern: "{controller=Plan}/{action=Plan}/{id?}");
app.MapControllerRoute(
    name: "visits",
    pattern: "{controller=Visits}/{action=Visits}/{id?}");
app.Run();
