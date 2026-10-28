using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HorseClub.Entities.KhoiND.Models;
using HorseClub.Services.KhoiND;

namespace HorseClub.RazorWebApp.KhoiND.Pages.Horses
{
    public class DetailsModel : PageModel
    {
        private readonly IHorseServiceKhoiND _service;

        public DetailsModel(IHorseServiceKhoiND service)
        {
            _service = service;
        }

        public Horse Horse { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null) return NotFound();
            var item = await _service.GetByIdAsync(id.Value);
            if (item == null) return NotFound();
            Horse = item;
            return Page();
        }
    }
}
