using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dataContext;

        public UserRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<User> Authenticate(string UserName, string Password)
        {
            return await dataContext.Users.FirstOrDefaultAsync(x => x.UserName == UserName && x.Password == Password);
            
        }
    }
}
