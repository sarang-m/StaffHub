using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StaffHub.Entities;
using StaffHub.Entities.IdentityEntities;
using StaffHub.ServiceContracts;
using StaffHub.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddDbContext<EmployeesDbContext>(
    options => { options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")); });
//Enable identity
builder.Services.AddIdentity<ApplicationUser,ApplicationRole>(options =>
{
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = true;
})
    .AddEntityFrameworkStores<EmployeesDbContext>()
    .AddDefaultTokenProviders()
    .AddUserStore<UserStore<ApplicationUser,ApplicationRole,EmployeesDbContext, Guid>>()
    .AddRoleStore<RoleStore<ApplicationRole,EmployeesDbContext, Guid>>();
builder.Services.AddAuthorization(Options =>
{ Options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build(); });
builder.Services.ConfigureApplicationCookie(Options => Options.LoginPath = "/account/login");

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers(); 

app.Run();
