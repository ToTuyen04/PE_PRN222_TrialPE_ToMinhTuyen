using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepository
    {
        private readonly GameHubContext _context;
        public AccountRepository(GameHubContext context)
        {
            _context = context;
        }

        public async Task<User?> Authenticate(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == password);
        }
    }
}
