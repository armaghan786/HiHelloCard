using HiHelloCard.Interfaces.Repository;
using HiHelloCard.Model.Domain;
using HiHelloCard.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiHelloCard.Repository
{
    public class UserCardFieldRepository : BaseRepository<Usercardfield>, IUserCardFieldRepository
    {
        public UserCardFieldRepository(HihelloContext dbContext) : base(dbContext)
        { }

        public void BulkDelete(List<Usercardfield> model)
        {
            _dbContext.Usercardfields.RemoveRange(model);
            _dbContext.SaveChanges();
        }
    }
}
