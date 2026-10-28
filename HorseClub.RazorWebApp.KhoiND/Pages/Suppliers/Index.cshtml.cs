using Microsoft.AspNetCore.Mvc.RazorPages;
using HorseClub.Entities.KhoiND.Models;
using HorseClub.Services.KhoiND;

namespace HorseClub.RazorWebApp.KhoiND.Pages.Suppliers
{
    // Read-only: lists rows, no create/edit/delete.
    public class IndexModel : PageModel
    {
        private readonly ISupplierServiceKhoiND _service;

        public IndexModel(ISupplierServiceKhoiND service)
        {
            _service = service;
        }

        public List<Supplier> Items { get; set; } = new();

        public async Task OnGetAsync()
        {
            Items = await _service.GetAllAsync();
        }
    }
}
