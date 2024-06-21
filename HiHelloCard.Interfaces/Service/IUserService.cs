using HiHelloCard.Model.Response;
using HiHelloCard.Model.ViewModel;
using HiHelloCard.Model.ViewModel.ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiHelloCard.Interfaces.Service
{
    public interface IUserService : IBaseService<UserModel>
    {
        Task<BaseResponse> SignUp(UserModel sModel);
        Task<BaseResponse> AppLogin(UserModel login, AppSettings _appSetting);
    }
}
