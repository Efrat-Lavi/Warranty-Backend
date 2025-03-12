using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warranty.Core.DTOs;
using Warranty.Core.Interfaces;
using Warranty.Core.Interfaces.Services;
using Warranty.Core.Models;

namespace Warranty.Service
{
    //public class PermissionServices:IPermissionServices
    //{
    //    private readonly IRepositoryManager _iRepository;

    //    public PermissionServices(IRepositoryManager iRepository)
    //    {
    //        _iRepository = iRepository;
    //    }

    //    public IEnumerable<PermissionModel> GetAllPermissions()
    //    {
    //        return _iRepository.permissionRepository.GetFull();
    //    }

    //    public PermissionModel GetPermissionById(int id)
    //    {
    //        return _iRepository.permissionRepository.GetById(id);
    //    }

    //    public PermissionModel AddPermission(PermissionModel permission)
    //    {
    //        var newPermission = _iRepository.permissionRepository.Add(permission);
    //        if (newPermission != null)
    //        {
    //            _iRepository.save();
    //            return newPermission;
    //        }
    //        return null;
    //    }

    //    public PermissionModel UpdatePermission(int id, PermissionModel permission)
    //    {
    //        var updatedPermission = _iRepository.permissionRepository.Update(id, permission);
    //        if (updatedPermission != null)
    //        {
    //            _iRepository.save();
    //            return updatedPermission;
    //        }
    //        return null;
    //    }

    //    public bool DeletePermission(int id)
    //    {
    //        bool succeed = _iRepository.permissionRepository.Delete(id);
    //        if (succeed)
    //            _iRepository.save();
    //        return succeed;
    //    }
    //}
    public class PermissionServices : IPermissionServices
    {
        private readonly IRepositoryManager _iRepository;
        private readonly IMapper _mapper;

        public PermissionServices(IRepositoryManager iRepository, IMapper mapper)
        {
            _iRepository = iRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PermissionDto>> GetAllPermissions()
        {
            var permissions = await _iRepository.permissionRepository.GetFull();
            return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
        }

        public async Task<PermissionDto> GetPermissionById(int id)
        {
            var permission = _iRepository.permissionRepository.GetById(id);
            return _mapper.Map<PermissionDto>(permission);
        }

        public async Task<PermissionDto> AddPermission(PermissionDto permissionDto)
        {
            var permissionEntity = _mapper.Map<PermissionModel>(permissionDto);
            permissionEntity = await _iRepository.permissionRepository.Add(permissionEntity);
            if (permissionEntity != null)
            {
                await _iRepository.Save();
                return _mapper.Map<PermissionDto>(permissionEntity);
            }
            return null;
        }

        public async Task<PermissionDto> UpdatePermission(int id, PermissionDto permissionDto)
        {
            var permissionEntity = _mapper.Map<PermissionModel>(permissionDto);
            permissionEntity = await _iRepository.permissionRepository.Update(id, permissionEntity);
            if (permissionEntity != null)
            {
                await _iRepository.Save();
                return _mapper.Map<PermissionDto>(permissionEntity);
            }
            return null;
        }

        public async Task<bool> DeletePermission(int id)
        {
            bool succeed =  await _iRepository.permissionRepository.Delete(id);
            if (succeed)
                await  _iRepository.Save();
            return succeed;
        }
    }

}
