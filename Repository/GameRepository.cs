using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class GameRepository
    {
        private readonly GameHubContext _context;
        public GameRepository(GameHubContext context)
        {
            _context = context;
        }

        public async Task<List<Developer>> GetDevelopersAsync()
        {
            return await _context.Developers.ToListAsync();
        }

        public async Task<List<GameCategory>> GetCategoriesAsync()
        {
            return await _context.GameCategories.ToListAsync();
        }

        public async Task<List<Game>> SearchAsync(string search1, string search2)
        {
            var query = _context.Games.OrderByDescending(g => g.GameId).Include(g => g.Category).AsQueryable();
            if(!string.IsNullOrEmpty(search1) && !string.IsNullOrEmpty(search2))
            {
                query = query.Where(g => g.Price.ToString().ToLower().Contains(search1.ToLower()) && g.Category.CategoryName.ToLower().Contains(search2.ToLower()));
            } else if(!string.IsNullOrEmpty(search2))
            {
                query = query.Where(g => g.Category.CategoryName.ToLower().Contains(search2.ToLower()));
            } else if(!string.IsNullOrEmpty(search1))
            {
                query = query.Where(g => g.Price.ToString().ToLower().Contains(search1.ToLower()));
            }
            return await query.ToListAsync();

        }
        public async Task RemoveGameAsync(Game g)
        {
            _context.Games.Remove(g);
            await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateGameAsync(Game g)
        {
            _context.Attach(g).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
        public async Task<int> AddGameAsync(Game g)
        {
            await _context.Games.AddAsync(g);
            return await _context.SaveChangesAsync();
        }

        public async Task<Game?> GetGameAsync(int id)
        {
            return await _context.Games
                .Include(g => g.Category)
                .Include(g => g.Developer)
                .FirstOrDefaultAsync(g => g.GameId == id);
        }
        public async Task<List<Game>> GetGamesAsync()
        {
            return await _context.Games.OrderByDescending(g => g.GameId)
                .Include(g => g.Category)
                .Include(g => g.Developer)
                .ToListAsync();
        }
    }
}
