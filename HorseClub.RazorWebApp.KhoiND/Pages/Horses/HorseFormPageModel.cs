using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using HorseClub.Entities.KhoiND.Enums;
using HorseClub.Entities.KhoiND.Models;
using HorseClub.RazorWebApp.KhoiND.Hubs;
using HorseClub.Services.KhoiND;
using HorseClub.Services.KhoiND.Dtos;
using HorseClub.Services.KhoiND.Realtime;

namespace HorseClub.RazorWebApp.KhoiND.Pages.Horses
{
    // Shared by Create and Edit: the bound form model, dropdown lists and hub access.
    public abstract class HorseFormPageModel : PageModel
    {
        protected readonly IHorseServiceKhoiND _service;
        protected readonly IOwnerServiceKhoiND _ownerService;
        protected readonly IHubContext<HorseClubHub> _hub;

        protected HorseFormPageModel(IHorseServiceKhoiND service, IOwnerServiceKhoiND ownerService, IHubContext<HorseClubHub> hub)
        {
            _service = service;
            _ownerService = ownerService;
            _hub = hub;
        }

        [BindProperty]
        public Horse Horse { get; set; } = default!;

        // Navigations are never posted.
        protected void RemoveNavigationErrors()
        {
            ModelState.Remove($"{nameof(Horse)}.{nameof(Horse.Owner)}");
            ModelState.Remove($"{nameof(Horse)}.{nameof(Horse.Sire)}");
            ModelState.Remove($"{nameof(Horse)}.{nameof(Horse.Dam)}");
        }

        // Re-read after saving so the DTO carries the owner name.
        protected async Task NotifyChangedAsync(string action)
        {
            var saved = await _service.GetByIdAsync(Horse.Id) ?? Horse;
            await _hub.Clients.All.SendAsync(HubContract.ItemChanged, action, saved.ToDto());
        }

        protected async Task LoadSelectListsAsync()
        {
            ViewData["OwnerId"] = new SelectList(
                await _ownerService.GetAllAsync(), nameof(Owner.Id), nameof(Owner.FullName), Horse.OwnerId);

            // A horse can't be its own parent; sires are male, dams are mares.
            var others = (await _service.GetAllAsync()).Where(h => h.Id != Horse.Id).ToList();
            ViewData["SireId"] = new SelectList(
                others.Where(h => h.Sex != (byte)HorseSex.Mare), nameof(Horse.Id), nameof(Horse.Name), Horse.SireId);
            ViewData["DamId"] = new SelectList(
                others.Where(h => h.Sex == (byte)HorseSex.Mare), nameof(Horse.Id), nameof(Horse.Name), Horse.DamId);

            ViewData["Sex"] = EnumSelectList<HorseSex>(Horse.Sex);
            ViewData["Status"] = EnumSelectList<HorseStatus>(Horse.Status);
        }

        private static SelectList EnumSelectList<TEnum>(byte selected) where TEnum : struct, Enum
        {
            var options = Enum.GetValues<TEnum>()
                .Select(e => new { Value = Convert.ToByte(e), Text = e.ToString() });
            return new SelectList(options, "Value", "Text", selected);
        }
    }
}
