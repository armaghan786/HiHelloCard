using HiHelloCard.Model.Domain;
using HiHelloCard.Model.ViewModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiHelloCard.Interfaces.Repository
{
    public interface IUserRepository : IBaseRepository<ApplicationUser>
    {
        Task<SignInResult> PasswordSignInAsync(UserModel signInModel);
        Task SendEmailConfirmationEmail(ApplicationUser User, string token);
        Task<IdentityResult> ConfirmEmailAsync(string uid, string token);
    }
}
