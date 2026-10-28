using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using HorseClub.RazorWebApp.KhoiND.Hubs;
using HorseClub.Services.KhoiND;

namespace HorseClub.RazorWebApp.KhoiND.Pages.Horses
{
    public class EditModel : HorseFormPageModel
    {
        public EditModel(IHorseServiceKhoiND service, IOwnerServiceKhoiND ownerService, IHubContext<HorseClubHub> hub)
            : base(service, ownerService, hub)
        {
        }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null) return NotFound();
            var item = await _service.GetByIdAsync(id.Value);
            if (item == null) return NotFound();
            Horse = item;
            await LoadSelectListsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (id != Horse.Id) return NotFound();

            // Soft-delete flags are only changed by the Delete page.
            Horse.IsDeleted = false;
            Horse.DeletedAt = null;

            RemoveNavigationErrors();
            if (ModelState.IsValid)
            {
                try
                {
                    // Don't let EF touch the related rows.
                    Horse.Owner = null;
                    Horse.Sire = null;
                    Horse.Dam = null;
                    await _service.UpdateAsync(Horse);
                    await NotifyChangedAsync("updated");
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
