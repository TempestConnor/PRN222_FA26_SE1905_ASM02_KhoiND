using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HorseClub.Entities.KhoiND.Models;
using HorseClub.Services.KhoiND;

namespace HorseClub.RazorWebApp.KhoiND.Pages.Horses
{
    public class IndexModel : PageModel
    {
        private readonly IHorseServiceKhoiND _service;

        public IndexModel(IHorseServiceKhoiND service)
        {
            _service = service;
        }

        // GET: /Horses?name=..&breed=..&ownerName=..
        [BindProperty(SupportsGet = true)]
        public string? Name { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Breed { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? OwnerName { get; set; }

        public List<Horse> Horses { get; set; } = new();

        public async Task OnGetAsync()
        {
            Horses = await _service.GetAllAsync(Name, Breed, OwnerName);
        }
    }
}
