using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Models;
using Service;

namespace GameManagement.Pages.Games
{
    public class CreateModel : PageModel
    {
        private readonly GameService _service;

        public CreateModel(GameService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGet()
        {
        ViewData["CategoryId"] = new SelectList(await _service.GetGameCategoriesAsync(), "CategoryId", "CategoryName");
        ViewData["DeveloperId"] = new SelectList(await _service.GetDevelopersAsync(), "DeveloperId", "DeveloperName");
            return Page();
        }

        [BindProperty]
        public Game Game { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _service.CreateGameAsync(Game);

            return RedirectToPage("./Index");
        }
    }
}
