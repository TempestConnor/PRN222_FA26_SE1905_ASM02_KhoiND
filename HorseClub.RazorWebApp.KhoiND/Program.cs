using HorseClub.RazorWebApp.KhoiND.Hubs;
using HorseClub.Services.KhoiND;
using HorseClub.Services.KhoiND.Realtime;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddScoped<IHorseServiceKhoiND, HorseServiceKhoiND>();
builder.Services.AddScoped<IOwnerServiceKhoiND, OwnerServiceKhoiND>();
builder.Services.AddScoped<IStaffProfileServiceKhoiND, StaffProfileServiceKhoiND>();
builder.Services.AddScoped<ISupplierServiceKhoiND, SupplierServiceKhoiND>();
builder.Services.AddScoped<ISupplyCategoryServiceKhoiND, SupplyCategoryServiceKhoiND>();
builder.Services.AddScoped<ISupplyItemServiceKhoiND, SupplyItemServiceKhoiND>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();
app.MapHub<HorseClubHub>(HubContract.Path);

app.Run();
