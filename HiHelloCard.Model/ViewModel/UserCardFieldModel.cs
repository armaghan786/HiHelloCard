using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiHelloCard.Model.ViewModel
{
    public class UserCardFieldModel
    {
        public int Id { get; set; }
        public int? CardFieldId { get; set; }
        public int? CardId { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
    }
}
