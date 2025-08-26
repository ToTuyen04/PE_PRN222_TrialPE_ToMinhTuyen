using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace GameManagement.Pages.Games
{
    public class IndexModel : PageModel
    {
        private readonly Repository.Models.GameHubContext _context;

        public IndexModel(Repository.Models.GameHubContext context)
        {
            _context = context;
        }

        public IList<Game> Game { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Game = await _context.Games
                .Include(g => g.Category)
                .Include(g => g.Developer).ToListAsync();
        }
    }
}
