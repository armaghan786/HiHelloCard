using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HiHelloCard.Model.ViewModel
{
    public class UserModel
    {
        public string? Id { get; set; }
        public string? Guid { get; set; }
        public string? RefreshToken { get; set; }


        [Required(ErrorMessage = "Email field is required."), EmailAddress(ErrorMessage = "Email is not a valid e-mail address.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password field is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
        public string? DeviceToken { get; set; }
        public IList<AuthenticationScheme>? ExternalLogins { get; set; }
    }
}
