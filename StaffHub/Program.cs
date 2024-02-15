using Microsoft.EntityFrameworkCore;
using StaffHub.Entities;
using StaffHub.ServiceContracts;
using StaffHub.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddDbContext<EmployeesDbContext>(
    options => { options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")); });

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseStaticFiles();
app.UseRouting();
app.MapControllers(); 

app.Run();
