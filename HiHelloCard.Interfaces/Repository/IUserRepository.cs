using HiHelloCard.Model.Domain;
using HiHelloCard.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiHelloCard.Interfaces.Repository
{
    public interface IUserRepository : IBaseRepository<ApplicationUser>
    {
    }
}
