﻿using HiHelloCard.Model.Domain;
using HiHelloCard.Repository.BaseRepository;
using HiHelloCard.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiHelloCard.Model.ViewModel;

namespace HiHelloCard.Repository
{
    public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(HihelloContext dbContext) : base(dbContext)
        {
        }
    }
}
