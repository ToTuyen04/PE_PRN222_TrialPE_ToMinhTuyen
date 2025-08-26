using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Repository.Models;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameManagement.Pages.Games
{
    public class CreateModel : PageModel
    {
        private readonly GameService _service;
        private readonly IHubContext<SignalHub> _hubContext;

        public CreateModel(GameService service, IHubContext<SignalHub> hubContext)
        {
            _service = service;
            _hubContext = hubContext;
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
                ViewData["CategoryId"] = new SelectList(await _service.GetGameCategoriesAsync(), "CategoryId", "CategoryName");
                ViewData["DeveloperId"] = new SelectList(await _service.GetDevelopersAsync(), "DeveloperId", "DeveloperName");
                return Page();
            }

            await _service.CreateGameAsync(Game);
            await _hubContext.Clients.All.SendAsync("LoadData");

            return RedirectToPage("./Index");
        }
    }
}
