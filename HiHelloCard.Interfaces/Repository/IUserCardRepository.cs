using HiHelloCard.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiHelloCard.Interfaces.Repository
{
    public interface IUserCardRepository : IBaseRepository<Usercard>
    {
        IEnumerable<Usercard> GetCardslist(string userGuid, string d);
        Usercard GetByGuid(string guid);
    }
}
