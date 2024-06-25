using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiHelloCard.Model.ViewModel
{
    public class ApplicationUser : IdentityUser
    {
        public string? Guid { get; set; }
        public bool? IsActive { get; set; } = false;
        public bool? IsArchive { get; set; } = false;
    }
}
