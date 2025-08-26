using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameManagement.Pages.Games
{
    public class DeleteModel : PageModel
    {
        private readonly GameService _service;
        private readonly IHubContext<SignalHub> _hubContext;

        public DeleteModel(GameService service, IHubContext<SignalHub> hubContext)
        {
            _service = service;
            _hubContext = hubContext;
        }

        [BindProperty]
        public Game Game { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _service.GetGameAsync(id.Value);

            if (game == null)
            {
                return NotFound();
            }
            else
            {
                Game = game;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _service.GetGameAsync(id.Value);
            if (game != null)
            {
                Game = game;
                await _service.DeleteAsync(Game);
                await _hubContext.Clients.All.SendAsync("LoadData");
            }

            return RedirectToPage("./Index");
        }
    }
}
