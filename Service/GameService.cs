using Repository;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class GameService
    {
        private readonly GameRepository _repository;
        public GameService(GameRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<Developer>> GetDevelopersAsync()
        {
            return await _repository.GetDevelopersAsync();
        }

        public async Task<List<Game>> SearchAsync(string search1, string search2)
        {
            return await _repository.SearchAsync(search1, search2);
        }
        public async Task DeleteAsync(Game g)
        {
            await _repository.RemoveGameAsync(g);
        }
        public async Task<int> UpdateGameAsync(Game g)
        {
            return await _repository.UpdateGameAsync(g);
        }
        public async Task<int> CreateGameAsync(Game g)
        {
            return await _repository.AddGameAsync(g);
        }
        public async Task<List<Game>> GetGamesAsync()
        {
            return await _repository.GetGamesAsync();
        }
        public async Task<List<GameCategory>> GetGameCategoriesAsync()
        {
            return await _repository.GetCategoriesAsync();
        }
        public async Task<Game?> GetGameAsync(int id)
        {
            return await _repository.GetGameAsync(id);
        }
    }
}
