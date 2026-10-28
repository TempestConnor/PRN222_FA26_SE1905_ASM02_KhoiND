using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using HorseClub.Entities.KhoiND.Models;
using HorseClub.RazorWebApp.KhoiND.Hubs;
using HorseClub.Services.KhoiND;

namespace HorseClub.RazorWebApp.KhoiND.Pages.Horses
{
    public class CreateModel : HorseFormPageModel
    {
        public CreateModel(IHorseServiceKhoiND service, IOwnerServiceKhoiND ownerService, IHubContext<HorseClubHub> hub)
            : base(service, ownerService, hub)
        {
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Horse = new Horse { BirthDate = DateOnly.FromDateTime(DateTime.Today) };
            await LoadSelectListsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Server-managed columns: SQL Server fills Id/CreatedAt, the service sets the rest.
            Horse.Id = Guid.Empty;
            Horse.CreatedAt = default;
            Horse.UpdatedAt = null;

            RemoveNavigationErrors();
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.CreateAsync(Horse);
                    await NotifyChangedAsync("created");
                    return RedirectToPage("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.GetBaseException().Message);
                }
            }
            await LoadSelectListsAsync();
            return Page();
        }
    }
}
