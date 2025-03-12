using AutoMapper;
using Warranty.API.PostModels;
using Warranty.Core.DTOs;

namespace Warranty.API
{
    public class MappingPostProfile:Profile
    {
        public MappingPostProfile()
        {
            CreateMap<CompanyPostModel, CompanyDto>();
            CreateMap<PermissionPostModel, PermissionDto>();
            CreateMap<RecordPostModel, RecordDto>();
            CreateMap<RolePostModel, RoleDto>();
            CreateMap<UserPostModel, UserDto>();
            CreateMap<WarrantyPostModel, WarrantyDto>();

            CreateMap<UserPostModel, RegisterDto>();

        }
    }
}
