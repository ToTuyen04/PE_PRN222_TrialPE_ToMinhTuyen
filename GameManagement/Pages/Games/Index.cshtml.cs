using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Service;

namespace GameManagement.Pages.Games
{
    public class IndexModel : PageModel
    {
        private readonly GameService _service;
        [BindProperty(SupportsGet = true)]
		public string Search1 { get; set; } = string.Empty;
        [BindProperty(SupportsGet = true)]
        public string Search2 { get; set; } = string.Empty;
        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 3;
        public int TotalPage { get; set; }

        public IndexModel(GameService service)
        {
            _service = service;
        }

        public IList<Game> Game { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (!string.IsNullOrEmpty(Search1) || !string.IsNullOrEmpty(Search2))
            {
                Game = await _service.SearchAsync(Search1, Search2);
            }
            else
            {
                Game = await _service.GetGamesAsync();
            }
            Paginate(Game, PageNumber);
        }

        private void Paginate(IList<Game> source, int pageNum)
        {
            Game = source.Skip((pageNum - 1) * PageSize).Take(PageSize).ToList();
            TotalPage = (int)Math.Ceiling((double)source.Count / PageSize);

        }
    }
}
