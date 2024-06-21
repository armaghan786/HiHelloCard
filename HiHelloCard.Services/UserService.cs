using AutoMapper;
using HiHelloCard.Interfaces.Repository;
using HiHelloCard.Interfaces.Service;
using HiHelloCard.Model.Domain;
using HiHelloCard.Model.Response;
using HiHelloCard.Model.ViewModel;
using HiHelloCard.Model.ViewModel.ApiModel;
using HiHelloCard.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiHelloCard.Services
{
    public class UserService : BaseService<UserModel, User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository , IMapper mapper) : base(userRepository, mapper)
        {
            _userRepository = userRepository;
        }


        public async Task<BaseResponse> SignUp(UserModel sModel)
        {
            try
            {
                var exUser = _userRepository.FirstOrDefault(x => x.Email == sModel.Email);
                if (exUser != null)
                    return Constant.Response(Constant.error, new object(), "User already registered.");

                var data = new User();
                data.Guid = Guid.NewGuid().ToString();
                data.Email = sModel.Email;
                data.Password = Constant.Encrypt(sModel.Password);
                data.IsActive = true;
                data.IsArchive = false;
                data.CreatedDateTime = DateTime.UtcNow;
                await _userRepository.Add(data);
                return Constant.Response(Constant.success, new object(), "Signup successfull.");

            }
            catch (Exception ex)
            {
                return Constant.Response(Constant.error, new object(), "Something went wrong.");
            }
        }

        public async Task<BaseResponse> AppLogin(UserModel login, AppSettings _appSetting)
        {
            try
            {
                var user = _userRepository.FirstOrDefault(x => x.Email.ToLower().Contains(login.Email) && !x.IsArchive.Value && x.IsActive.Value);
                if (user != null)
                {
                    var password = Constant.Decrypt(user.Password);
                    if (login.Password == password)
                    {
                        var data = new UserAppModel();
                        data.GUID = user.Guid;
                        data.Email = login.Email;
                        data.Id = user.Id;
                        login.Id = user.Id;
                        login.Guid = user.Guid;
                        data.OauthToken =  Constant.token(login, _appSetting);
                        return Constant.Response(Constant.success, data, "Login successfull.");
                    }
                    return Constant.Response(Constant.error, new object(), "Invalid credentioals.");
                }
                return Constant.Response(Constant.error, new object(), "Invalid credentioals.");
            }
            catch (Exception ex)
            {
                return Constant.Response(Constant.error, new object(), "Invalid credentioals.");
            }
        }
    }
}
