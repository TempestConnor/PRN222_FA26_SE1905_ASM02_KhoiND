using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HorseClub.RazorWebApp.KhoiND.Pages;

// The site root just forwards to the horse list.
public class IndexModel : PageModel
{
    public IActionResult OnGet() => RedirectToPage("/Horses/Index");
}
