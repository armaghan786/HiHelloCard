using AutoMapper;
using HiHelloCard.Model.Domain;
using HiHelloCard.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiHelloCard.Mapper
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<User, UserModel>();

            CreateMap<UserModel, User>()
                .ForMember(g => g.Guid, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(g => g.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(g => g.IsArchive, opt => opt.MapFrom(src => false))
                .ForMember(g => g.CreatedDateTime, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(g => g.UpdatedDateTime, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
