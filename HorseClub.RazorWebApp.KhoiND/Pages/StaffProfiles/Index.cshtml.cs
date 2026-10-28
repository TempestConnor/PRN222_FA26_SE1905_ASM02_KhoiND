using Microsoft.AspNetCore.Mvc.RazorPages;
using HorseClub.Entities.KhoiND.Models;
using HorseClub.Services.KhoiND;

namespace HorseClub.RazorWebApp.KhoiND.Pages.StaffProfiles
{
    // Read-only: lists rows, no create/edit/delete.
    public class IndexModel : PageModel
    {
        private readonly IStaffProfileServiceKhoiND _service;

        public IndexModel(IStaffProfileServiceKhoiND service)
        {
            _service = service;
        }

        public List<StaffProfile> Items { get; set; } = new();

        public async Task OnGetAsync()
        {
            Items = await _service.GetAllAsync();
        }
    }
}
