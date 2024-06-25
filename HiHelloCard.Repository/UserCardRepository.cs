using HiHelloCard.Interfaces.Repository;
using HiHelloCard.Model.Domain;
using HiHelloCard.Repository.BaseRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace HiHelloCard.Repository
{
    public class UserCardRepository : BaseRepository<Usercard>, IUserCardRepository
    {
        public UserCardRepository(HihelloContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Usercard> GetCardslist(string userguid, string d)
        {
            //return _dbContext.Usercards.Where(x => x.User.Guid == userguid && !x.IsArchive.Value).OrderBy(d);
            return null;
        }

        public Usercard GetByGuid(string guid)
        {
            return null;
            //return _dbContext.Usercards.Include(cf => cf.Usercardfields).Include(cb => cb.Usercardbadges).FirstOrDefault(x => x.Guid == guid);
        }
    }
}
