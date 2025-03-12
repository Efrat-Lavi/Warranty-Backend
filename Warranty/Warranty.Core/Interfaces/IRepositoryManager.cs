using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warranty.Core.Interfaces.Repositories;

namespace Warranty.Core.Interfaces
{
    public interface IRepositoryManager
    {
        ICompanyRepository companyRepository { get; }
         IPermissionRepository permissionRepository { get; }
         IRecordRepository recordRepository { get;  }
         IRoleRepository roleRepository { get; }
         IUserRepository userRepository { get; }
         IWarrantyRepository warrantyRepository { get; }
        Task Save();
    }
}
