using Microsoft.AspNetCore.Mvc.RazorPages;
using HorseClub.Entities.KhoiND.Models;
using HorseClub.Services.KhoiND;

namespace HorseClub.RazorWebApp.KhoiND.Pages.Owners
{
    // Read-only: lists rows, no create/edit/delete.
    public class IndexModel : PageModel
    {
        private readonly IOwnerServiceKhoiND _service;

        public IndexModel(IOwnerServiceKhoiND service)
        {
            _service = service;
        }

        public List<Owner> Items { get; set; } = new();

        public async Task OnGetAsync()
        {
            Items = await _service.GetAllAsync();
        }
    }
}
