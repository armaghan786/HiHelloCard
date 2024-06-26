using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiHelloCard.Model.ViewModel
{
    public class UserModel
    {
        public int Id { get; set; }
        public string? Guid { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? RefreshToken { get; set; }
    }
}
