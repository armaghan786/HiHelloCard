using HiHelloCard.Model.Domain;
using HiHelloCard.Repository.BaseRepository;
using HiHelloCard.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiHelloCard.Model.ViewModel;
using Microsoft.AspNetCore.Identity;
using HiHelloCard.Model.ViewModel.Common;
using HiHelloCard.Interfaces.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;

namespace HiHelloCard.Repository
{
    public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        public UserRepository(HihelloContext dbContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService, IConfiguration configuration) : base(dbContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<SignInResult> PasswordSignInAsync(UserModel signInModel)
        {
            IdentityError message = new IdentityError();
            try
            {
                return await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, signInModel.RememberMe, true);
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public async Task SendEmailConfirmationEmail(ApplicationUser User, string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string appTitle = _configuration.GetSection("Application:AppTitle").Value;
            string confirmationLink = _configuration.GetSection("Application:EmailConfirmation").Value;

            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { User.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{Link}}", string.Format(appDomain + confirmationLink, User.Id,token)),
                    new KeyValuePair<string, string>("{{AppTitle}}", appTitle)
                }
            };
            await _emailService.SendEmailForEmailConfirmation(options);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string uid, string token)
        {
            return await _userManager.ConfirmEmailAsync(await _userManager.FindByIdAsync(uid), token);
        }
    }
}
