using AutoMapper;
using HiHelloCard.Interfaces.Repository;
using HiHelloCard.Interfaces.Service;
using HiHelloCard.Model.Response;
using HiHelloCard.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiHelloCard.Services
{
    public abstract class BaseService<TModel, TEntity> : IBaseService<TModel>
         where TModel : class //Model
         where TEntity : class //Entity
    {
        protected readonly IMapper Mapper;
        protected readonly IBaseRepository<TEntity> Repository;
        public BaseService(IBaseRepository<TEntity> repository, IMapper mapper)
        {
            this.Repository = repository;
            this.Mapper = mapper;
        }

        public virtual async Task<BaseResponse> Delete(int id)
        {
            await Repository.Delete(id);
            return Constant.Response("success", new object(), "Data removed successfully");
        }
        public virtual async Task<BaseResponse> SoftDelete(int id)
        {

            //var entity = Mapper.Map<TEntity>(model);
            //await Repository.Update(entity);

            return Constant.Response("success", new object(), "Data removed successfully");
        }
        public virtual async Task<BaseResponse> Get(int id)
        {
            var result = await Repository.GetById(id);
            if (result == null)
            {
                return null;
            }
            var model = this.Mapper.Map<TModel>(result);
            return Constant.Response("success", model, "");
        }

        public virtual async Task<BaseResponse> GetByGuid(string guid)
        {
            var result = await Repository.GetByGUID(guid);
            if (result == null)
            {
                return null;
            }
            var model = this.Mapper.Map<TModel>(result);
            return Constant.Response("success", model, "");
        }

        public virtual async Task<IEnumerable<TModel>> GetAll()
        {
            IEnumerable<TEntity> entities = await Repository.List();
            IEnumerable<TModel> models = Mapper.Map<IEnumerable<TModel>>(entities);
            return models;
        }

        public virtual async Task<BaseResponse> Upsert(TModel model)
        {
            try
            {
                int? id = (int)model.GetType().GetProperty("Id").GetValue(model);
                string guid = (string)model.GetType().GetProperty("Guid").GetValue(model);
                if (id == 0)
                {
                    var entity = Mapper.Map<TEntity>(model);
                    await Repository.Add(entity);
                }
                else
                {
                    var entity = Mapper.Map<TEntity>(model);
                    await Repository.Update(entity);
                }
                return Constant.Response("success", new object(), id == 0 ? "Data added successfully." : "Data updated successfully.");

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        Task<BaseListResponse> IBaseService<TModel>.LoadData(ClientDataTableRequest request)
        {
            return null;
        }



    }
}
