using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warranty.Core.DTOs;
using Warranty.Core.Models;

namespace Warranty.Core
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CompanyDto,CompanyModel>().ReverseMap();
            CreateMap<PermissionDto, PermissionModel>().ReverseMap();
            CreateMap<RoleDto, RoleModel>().ReverseMap();
            CreateMap<RecordDto, RecordModel>().ReverseMap();
            CreateMap<UserDto, UserModel>().ReverseMap();
            CreateMap<WarrantyDto, WarrantyModel>().ReverseMap();
        }
    }
}
