using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class EditModel : PageModel
    {
        private readonly GameService _service;
        private readonly IHubContext<SignalHub> _hubContext;

        public EditModel(GameService service, IHubContext<SignalHub> hubContext)
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
            Game = game;
           ViewData["CategoryId"] = new SelectList(await _service.GetGameCategoriesAsync(), "CategoryId", "CategoryName");
           ViewData["DeveloperId"] = new SelectList(await _service.GetDevelopersAsync(), "DeveloperId", "DeveloperName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["CategoryId"] = new SelectList(await _service.GetGameCategoriesAsync(), "CategoryId", "CategoryName");
                ViewData["DeveloperId"] = new SelectList(await _service.GetDevelopersAsync(), "DeveloperId", "DeveloperName");
                return Page();
            }

            //_context.Attach(Game).State = EntityState.Modified;

            try
            {
                await _service.UpdateGameAsync(Game);
                await _hubContext.Clients.All.SendAsync("LoadData");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _service.GetGameAsync(Game.GameId) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        //private bool GameExists(int id)
        //{
        //    return _context.Games.Any(e => e.GameId == id);
        //}
    }
}
