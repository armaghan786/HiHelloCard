using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HiHelloCard.Model.Domain;
using HiHelloCard.Model.ViewModel;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HiHelloCard.Mapper
{
    public class UserCardMapperProfile : Profile
    {
        public UserCardMapperProfile()
        {
            CreateMap<Usercard, UserCardModel>();
            CreateMap<UserCardModel, Usercard>()
                .ForMember(g => g.Guid, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(g => g.CreatedDateTime, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(g => g.UpdatedDateTime, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
