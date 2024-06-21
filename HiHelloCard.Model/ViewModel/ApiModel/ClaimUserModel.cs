using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiHelloCard.Model.ViewModel.ApiModel
{
    public class ClaimUserModel
    {
        public int UserId { get; set; }
        public string UserGUID { get; set; }
        public string UserEmail { get; set; }
    }
}
