using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Areas.Identity.Data;
using ThAmCo.Events.Data;
using ThAmCo.Events.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("IdentityContextConnection") 
                        ?? throw new InvalidOperationException("Connection string 'IdentityContextConnection' not found.");

// Register the HTTP Client and the EventTypeService for dependency injection
builder.Services.AddHttpClient<EventTypeService>();

// Register the HTTP Client and the ReservationService for dependency injection
builder.Services.AddHttpClient<AvailabilityService>();

// Register the HTTP Client and the VenueService for dependency injection
builder.Services.AddHttpClient<VenueService>();

// Add services to the container.
builder.Services.AddRazorPages();

// Register the database context
builder.Services.AddDbContext<EventsDbContext>();

// Register the Identity database context
builder.Services.AddDbContext<IdentityDbContext>(options =>
    options.UseSqlite(connectionString));

// Register the Identity services
builder.Services.AddDefaultIdentity<EventUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IdentityDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

// Create roles
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<EventUser>>();
    await CreateRoles(roleManager, userManager);
}

//Create Manager and Team Leader roles
async Task CreateRoles(RoleManager<IdentityRole> roleManager, UserManager<EventUser> userManager)
{
    string[] roleNames = { "Manager", "Team Leader" };
    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    // Create users and assign them to roles
    var users = new[]
    {
        new { Email = "manager@thamco.co.uk", Password = "Manager123!", Role = "Manager" },
        new { Email = "teamleader@thamco.co.uk", Password = "TeamLeader123!", Role = "Team Leader" }
    };

    foreach (var userInfo in users)
    {
        var user = await userManager.FindByEmailAsync(userInfo.Email);
        if (user == null)
        {
            user = new EventUser { UserName = userInfo.Email, Email = userInfo.Email };
            var result = await userManager.CreateAsync(user, userInfo.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, userInfo.Role);
            }
        }
    }
}

app.Run();

