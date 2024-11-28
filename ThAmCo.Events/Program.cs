using ThAmCo.Events.Data;
using ThAmCo.Events.Services;

var builder = WebApplication.CreateBuilder(args);

//Register the HTTP Client and the EventTypeService for dependency injection
builder.Services.AddHttpClient<EventTypeService>();

//Register the HTTP Client and the ReservationService for dependency injection
builder.Services.AddHttpClient<VenueService>();

// Add services to the container.
builder.Services.AddRazorPages();

//Register the database context
builder.Services.AddDbContext<EventsDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
