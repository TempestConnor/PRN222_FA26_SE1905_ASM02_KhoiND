using Microsoft.AspNetCore.Mvc.RazorPages;
using HorseClub.Entities.KhoiND.Models;
using HorseClub.Services.KhoiND;

namespace HorseClub.RazorWebApp.KhoiND.Pages.SupplyCategories
{
    // Read-only: lists rows, no create/edit/delete.
    public class IndexModel : PageModel
    {
        private readonly ISupplyCategoryServiceKhoiND _service;

        public IndexModel(ISupplyCategoryServiceKhoiND service)
        {
            _service = service;
        }

        public List<SupplyCategory> Items { get; set; } = new();

        public async Task OnGetAsync()
        {
            Items = await _service.GetAllAsync();
        }
    }
}
