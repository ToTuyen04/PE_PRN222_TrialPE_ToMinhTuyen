using Repository;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AccountService
    {
        private readonly AccountRepository _accountRepo;
        public AccountService(AccountRepository repository)
        {
            _accountRepo = repository;
        }

        public async Task<User?> AuthenticateAsync(string email, string password)
        {
            return await _accountRepo.Authenticate(email, password);
        }
    }
}
