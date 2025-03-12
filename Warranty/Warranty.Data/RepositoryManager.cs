using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warranty.Core.Interfaces;
using Warranty.Core.Interfaces.Repositories;

namespace Warranty.Data
{
    public class RepositoryManager : IRepositoryManager
    {
        DataContext _dataContext;
        public ICompanyRepository companyRepository { get; }
        public IPermissionRepository permissionRepository { get; }
        public IRecordRepository recordRepository { get; }
        public IRoleRepository roleRepository { get; }
        public IUserRepository userRepository { get; }
        public IWarrantyRepository warrantyRepository { get; }
        public RepositoryManager(DataContext dataContext, ICompanyRepository company, IPermissionRepository permission, IRecordRepository record, IRoleRepository role, IUserRepository user, IWarrantyRepository warranty)
        {
            companyRepository = company;
            permissionRepository = permission;
            recordRepository = record;
            roleRepository = role;
            userRepository = user;
            warrantyRepository = warranty;
            _dataContext = dataContext;
        }

        public async Task Save()
        {
            await _dataContext.SaveChangesAsync();
        }
    }
}
