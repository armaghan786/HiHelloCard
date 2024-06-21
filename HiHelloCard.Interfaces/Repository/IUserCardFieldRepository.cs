﻿using HiHelloCard.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiHelloCard.Interfaces.Repository
{
    public interface IUserCardFieldRepository : IBaseRepository<Usercardfield>
    {
        void BulkDelete(List<Usercardfield> model);
    }
}
