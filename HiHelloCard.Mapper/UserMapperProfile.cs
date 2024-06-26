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
            CreateMap<UserModel, ApplicationUser>().ReverseMap();
        }
    }
}
