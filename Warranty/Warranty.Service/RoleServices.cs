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
    public class RoleServices : IRoleServices
    {
        private readonly IRepositoryManager _iRepository;
        private readonly IMapper _mapper;

        public RoleServices(IRepositoryManager iRepository, IMapper mapper)
        {
            _iRepository = iRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleDto>> GetAllRoles()
        {
            var roles = await _iRepository.roleRepository.GetFull();
            return _mapper.Map<IEnumerable<RoleDto>>(roles);
        }

        public async Task<RoleDto> GetRoleById(int id)
        {
            var role = await _iRepository.roleRepository.GetById(id);
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<RoleDto> AddRole(RoleDto roleDto)
        {
            var roleEntity = _mapper.Map<RoleModel>(roleDto);
            roleEntity = await _iRepository.roleRepository.Add(roleEntity);
            if (roleEntity != null)
            {
                await _iRepository.Save();
                return _mapper.Map<RoleDto>(roleEntity);
            }
            return null;
        }

        public async Task<RoleDto> UpdateRole(int id, RoleDto roleDto)
        {
            var roleEntity = _mapper.Map<RoleModel>(roleDto);
            roleEntity =  await _iRepository.roleRepository.Update(id, roleEntity);
            if (roleEntity != null)
            {
                await _iRepository.Save();
                return _mapper.Map<RoleDto>(roleEntity);
            }
            return null;
        }

        public async Task<bool> DeleteRole(int id)
        {
            bool succeed = await _iRepository.roleRepository.Delete(id);
            if (succeed)
                await _iRepository.Save();
            return succeed;
        }
    }

}
