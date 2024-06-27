using AutoMapper;
using HiHelloCard.Interfaces.Repository;
using HiHelloCard.Interfaces.Service;
using HiHelloCard.Model.Domain;
using HiHelloCard.Model.Response;
using HiHelloCard.Model.ViewModel;
using HiHelloCard.Model.ViewModel.ApiModel;
using HiHelloCard.Services.Common;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QRCoder.PayloadGenerator;

namespace HiHelloCard.Services
{
    public class UserService : BaseService<UserModel, ApplicationUser>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService(IUserRepository userRepository, UserManager<ApplicationUser> userManager, IMapper mapper) : base(userRepository, mapper)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }


        public async Task<BaseResponse> SignUp(UserModel sModel)
        {
            try
            {
                var aspuser = _userManager.Users.FirstOrDefault(x => x.Email == sModel.Email);
                if (aspuser != null)
                    return Constant.Response(Constant.error, new object(), "User already registered.");

                try
                {
                    var user = new ApplicationUser()
                    {
                        Email = sModel.Email,
                        IsActive = false,
                        IsArchive = false,
                        UserName = sModel.Email,
                        Guid = Guid.NewGuid().ToString()
                    };
                    var resp = await _userManager.CreateAsync(user, sModel.Password);
                    if(resp.Succeeded)
                    {
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        if(!string.IsNullOrEmpty(token))
                        {
                            await _userRepository.SendEmailConfirmationEmail(user, token);
                        }
                    }
                }
                catch (Exception e)
                {
                    IdentityError message = new IdentityError();
                    message.Description = e.Message;
                }
                return Constant.Response(Constant.success, new object(), "Signup successfull.");

            }
            catch (Exception ex)
            {
                return Constant.Response(Constant.error, new object(), "Something went wrong.");
            }
        }

        public async Task<BaseResponse> ConfirmEmailAsync(string uid, string token)
        {
            var resp = await _userRepository.ConfirmEmailAsync(uid, token);
            if (resp.Succeeded)
                return Constant.Response(Constant.success, new object(), "");

            return Constant.Response("", new object(), resp.Errors.Any() ? resp.Errors.FirstOrDefault().Description : "");
        }

        public async Task<BaseResponse> AppLogin(UserModel login, AppSettings _appSetting)
        {
            try
            {
                var resp = await _userRepository.PasswordSignInAsync(login);
                if (resp.Succeeded)
                {
                    var user = _userManager.Users.FirstOrDefault(x => x.Email == login.Email);
                        var data = new UserAppModel();
                        data.GUID = user.Guid;
                        data.Email = login.Email;
                        data.Id = user.Id;
                        login.Id = user.Id;
                        login.Guid = user.Guid;
                        data.OauthToken = Constant.token(login, _appSetting);
                        return Constant.Response(Constant.success, data, "Login successfull.");
                }
                if (resp.IsNotAllowed)
                    return Constant.Response(Constant.notallowed, new object(), "Not allowed to login");
                else if (resp.IsLockedOut)
                    return Constant.Response(Constant.error, new object(), "Account blocked. Try after some time.");

                return Constant.Response(Constant.error, new object(), "Invalid credentioals.");
            }
            catch (Exception ex)
            {
                return Constant.Response(Constant.error, new object(), "Invalid credentioals.");
            }
        }
    }
}
