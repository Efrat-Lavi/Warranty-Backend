using Microsoft.EntityFrameworkCore;
using Warranty.Core.Models;

namespace Warranty.Data
{
    public class DataContext: DbContext
    {
        public DbSet<UserModel> users { get; set; }
        public DbSet<WarrantyModel> warranties { get; set; }
        public DbSet<CompanyModel> comoanies { get; set; }
        public DbSet<RecordModel> records { get; set; }
        public DbSet<PermissionModel> permissions { get; set; }
        public DbSet<RoleModel> roles { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}
