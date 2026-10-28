using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using HorseClub.Entities.KhoiND.Models;
using HorseClub.RazorWebApp.KhoiND.Hubs;
using HorseClub.Services.KhoiND;
using HorseClub.Services.KhoiND.Dtos;
using HorseClub.Services.KhoiND.Realtime;

namespace HorseClub.RazorWebApp.KhoiND.Pages.Horses
{
    public class DeleteModel : PageModel
    {
        private readonly IHorseServiceKhoiND _service;
        private readonly IHubContext<HorseClubHub> _hub;

        public DeleteModel(IHorseServiceKhoiND service, IHubContext<HorseClubHub> hub)
        {
            _service = service;
            _hub = hub;
        }

        public Horse Horse { get; set; } = default!;

        public string? Error { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null) return NotFound();
            var item = await _service.GetByIdAsync(id.Value);
            if (item == null) return NotFound();
            Horse = item;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return RedirectToPage("Index");

            try
            {
                var dto = item.ToDto();
                await _service.DeleteAsync(id);
                await _hub.Clients.All.SendAsync(HubContract.ItemChanged, "deleted", dto);
                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {
                Error = ex.GetBaseException().Message;
                Horse = item;
                return Page();
            }
        }
    }
}
