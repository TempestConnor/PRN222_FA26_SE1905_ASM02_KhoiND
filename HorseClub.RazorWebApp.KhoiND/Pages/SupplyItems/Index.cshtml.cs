using Microsoft.AspNetCore.Mvc.RazorPages;
using HorseClub.Entities.KhoiND.Models;
using HorseClub.Services.KhoiND;

namespace HorseClub.RazorWebApp.KhoiND.Pages.SupplyItems
{
    // Read-only: lists rows, no create/edit/delete.
    public class IndexModel : PageModel
    {
        private readonly ISupplyItemServiceKhoiND _service;

        public IndexModel(ISupplyItemServiceKhoiND service)
        {
            _service = service;
        }

        public List<SupplyItem> Items { get; set; } = new();

        public async Task OnGetAsync()
        {
            Items = await _service.GetAllAsync();
        }
    }
}
