using HiHelloCard.Model.Response;
using HiHelloCard.Model.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiHelloCard.Interfaces.Service
{
    public interface IUserCardService : IBaseService<UserCardModel>
    {
        Task<BaseListResponse> LoadData(ClientDataTableRequest model, string userGuid);
        Task<BaseResponse> AddEditCard(UserCardModel model, string userId, IFormFileCollection files);
        Task<BaseResponse> CardDetails(string guid);
    }
}
