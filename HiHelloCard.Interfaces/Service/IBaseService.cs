using HiHelloCard.Model.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HiHelloCard.Interfaces.Service
{
    public interface IBaseService<TModel> where TModel : class
    {
        Task<BaseResponse> SoftDelete(int id);
        Task<BaseResponse> Get(int id);
        Task<BaseResponse> GetByGuid(string guid);
        Task<IEnumerable<TModel>> GetAll();
        Task<BaseResponse> Upsert(TModel request);
        Task<BaseListResponse> LoadData(ClientDataTableRequest request);
    }
}
