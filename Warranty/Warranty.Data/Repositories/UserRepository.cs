using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warranty.Core.Interfaces.Repositories;
using Warranty.Core.Models;

namespace Warranty.Data.Repositories
{
    public class UserRepository : Repository<UserModel>, IUserRepository
    {
        public UserRepository(DataContext dataContext) : base(dataContext) { }
        public async Task<List<UserModel>> GetFull()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<UserModel> GetUserByEmail(string email)
        {


            return await _dbSet.Include(u => u.Role)  // כולל את ה-Role של המשתמש
                         .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
