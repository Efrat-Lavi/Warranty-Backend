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
    public class UserServices : IUserServices
    {
        private readonly IRepositoryManager _iRepository;
        private readonly IMapper _mapper;

        public UserServices(IRepositoryManager iRepository, IMapper mapper)
        {
            _iRepository = iRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = await _iRepository.userRepository.GetFull();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUserById(int id)
        {
            var user = await _iRepository.userRepository.GetById(id);
            return _mapper.Map<UserDto>(user);
        }
        public async Task<UserDto> GetUserByEmail(string email)
        {
            var user = await _iRepository.userRepository.GetUserByEmail(email);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> AddUser(UserDto userDto)
        {
            var userEntity = _mapper.Map<UserModel>(userDto);
            userEntity = await _iRepository.userRepository.Add(userEntity);
            if (userEntity != null)
            {
                await _iRepository.Save();
                return _mapper.Map<UserDto>(userEntity);
            }
            return null;
        }

        public async Task<UserDto> UpdateUser(int id, UserDto userDto)
        {
            var userEntity = _mapper.Map<UserModel>(userDto);
            userEntity =  await _iRepository.userRepository.Update(id, userEntity);
            if (userEntity != null)
            {
                await _iRepository.Save();
                return _mapper.Map<UserDto>(userEntity);
            }
            return null;
        }

        public async Task<bool> DeleteUser(int id)
        {
            bool succeed = await _iRepository.userRepository.Delete(id);
            if (succeed)
                await _iRepository.Save();
            return succeed;
        }
    }

}
