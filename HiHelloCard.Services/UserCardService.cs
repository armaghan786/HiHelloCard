using AutoMapper;
using HiHelloCard.Interfaces.Repository;
using HiHelloCard.Interfaces.Service;
using HiHelloCard.Model.Domain;
using HiHelloCard.Model.Response;
using HiHelloCard.Model.ViewModel;
using HiHelloCard.Services.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiHelloCard.Services
{
    public class UserCardService : BaseService<UserCardModel, Usercard>, IUserCardService
    {
        private readonly IUserCardRepository _userCardRepository;
        private readonly IUserCardFieldRepository _userCardFieldRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        public UserCardService(IUserCardRepository userCardRepository, IUserCardFieldRepository userCardFieldRepository, IHostingEnvironment hostingEnvironment, IMapper mapper) : base(userCardRepository, mapper)
        {
            _userCardRepository = userCardRepository;
            _userCardFieldRepository = userCardFieldRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<BaseListResponse> LoadData(ClientDataTableRequest model, string userGuid)
        {
            try
            {
                var dtmodel = Constant.TransformIntoModel(model);
                var orderBy = $"{dtmodel.SortColumn} {dtmodel.SortColumnDir}";
                var data = _userCardRepository.GetCardslist(userGuid, orderBy).Select(y => new UserCardModel
                {
                    Id = y.Id,
                    Guid = y.Guid,
                    Name = y.Name,
                    DesignId = y.DesignId,
                    ProfilePhoto = y.ProfilePhoto
                });
                var dataList = data.Skip(dtmodel.Skip).Take(dtmodel.PageSize).ToList();
                int recordsTotal = data.Count();
                BaseListResponse listResponse = new BaseListResponse
                {
                    Status = "success",
                    Message = "",
                    CurrentPage = (model.start / model.length),
                    Next = (model.start / model.length) < ((int)Math.Ceiling(recordsTotal / (double)model.length)) ? "Yes" : "No",
                    Pervious = model.start > 1 ? "Yes" : "No",
                    TotalRecords = recordsTotal,
                    Data = Mapper.Map<IEnumerable<UserCardModel>>(dataList)
                };
                return listResponse;
            }
            catch (Exception e)
            { throw; }
        }

        public async Task<BaseResponse> AddEditCard(UserCardModel model, int userId, IFormFileCollection files)
        {
            try
            {
                string logofolder = "Image/Logo";
                string Profilefolder = "Image/Profile";
                string badgefolder = "Image/Badge";
                if (string.IsNullOrEmpty(model.Guid))
                {
                    var card = new Usercard();
                    card.Guid = Guid.NewGuid().ToString();
                    card.Name = model.Name;
                    card.Prefix = model.Prefix;
                    card.FirstName = model.FirstName;
                    card.MiddleName = model.MiddleName;
                    card.LastName = model.LastName;
                    card.Suffix = model.Suffix;
                    card.Accreditations = model.Accreditations;
                    card.PreferredName = model.PreferredName;
                    card.MaidenName = model.MaidenName;
                    card.Pronouns = model.Pronouns;
                    if (model.UserProfile != null)
                        card.ProfilePhoto = Constant.UploadImage(Profilefolder, model.UserProfile, _hostingEnvironment);
                    card.DesignId = model.DesignId;
                    card.UserId = userId;
                    card.Color = model.Color;
                    if (model.UserLogo != null)
                        card.Logo = Constant.UploadImage(logofolder, model.UserLogo, _hostingEnvironment);
                    card.AffiliateTitle = model.AffiliateTitle;
                    card.Department = model.Department;
                    card.Company = model.Company;
                    card.Headline = model.Headline;
                    card.IsArchive = false;
                    card.CreatedDateTime = DateTime.UtcNow;

                    if (files != null && files.Any())
                        card.Usercardbadges = files.Where(x => x.Name != "UserProfile" && x.Name != "UserLogo").Select(x => new Usercardbadge
                        {
                            BadgePath = Constant.UploadImage(badgefolder, x, _hostingEnvironment),
                            Card = card
                        }).ToList();

                    if (model.CardFields.Any())
                        card.Usercardfields = model.CardFields.Select(x => new Usercardfield
                        {
                            CardFieldId = x.CardFieldId,
                            Card = card,
                            Link = x.Link,
                            Description = x.Description
                        }).ToList();

                    await _userCardRepository.Add(card);
                    return Constant.Response("success", new object(), Constant.CreatedMessage);
                }
                else
                {
                    var getdata = _userCardRepository.GetByGuid(model.Guid);
                    getdata.Name = model.Name;
                    getdata.Prefix = model.Prefix;
                    getdata.FirstName = model.FirstName;
                    getdata.MiddleName = model.MiddleName;
                    getdata.LastName = model.LastName;
                    getdata.Suffix = model.Suffix;
                    getdata.Accreditations = model.Accreditations;
                    getdata.PreferredName = model.PreferredName;
                    getdata.MaidenName = model.MaidenName;
                    getdata.Pronouns = model.Pronouns;
                    if (model.UserProfile != null)
                        getdata.ProfilePhoto = Constant.UploadImage(Profilefolder, model.UserProfile, _hostingEnvironment);
                    getdata.DesignId = model.DesignId;
                    getdata.UserId = userId;
                    getdata.Color = model.Color;
                    if (model.UserLogo != null)
                        getdata.Logo = Constant.UploadImage(logofolder, model.UserLogo, _hostingEnvironment);
                    getdata.AffiliateTitle = model.AffiliateTitle;
                    getdata.Department = model.Department;
                    getdata.Company = model.Company;
                    getdata.Headline = model.Headline;
                    getdata.UpdatedDateTime = DateTime.UtcNow;

                    if (files != null && files.Any())
                        getdata.Usercardbadges = files.Where(x => x.Name != "UserProfile" && x.Name != "UserLogo").Select(x => new Usercardbadge
                        {
                            BadgePath = Constant.UploadImage(badgefolder, x, _hostingEnvironment),
                        }).ToList();

                    var prelist = getdata.Usercardfields.ToList();
                    if (model.CardFields.Any())
                    {
                        if (prelist.Any())
                            _userCardFieldRepository.BulkDelete(prelist);
                        getdata.Usercardfields = model.CardFields.Select(x => new Usercardfield
                        {
                            CardFieldId = x.CardFieldId,
                            Link = x.Link,
                            Description = x.Description
                        }).ToList();
                    }
                    else
                    {
                        if (prelist.Any())
                            _userCardFieldRepository.BulkDelete(prelist);
                    }
                    await _userCardRepository.Update(getdata);
                    return Constant.Response("success", new object(), Constant.UpdatedMessage);
                }
            }
            catch (Exception e)
            {
                return Constant.Response("error", new object(), e.Message);
            }
        }

        public async Task<BaseResponse> CardDetails(string guid)
        {
            try
            {
                if (!string.IsNullOrEmpty(guid))
                {
                    var getData = _userCardRepository.GetByGuid(guid);
                    if (getData != null)
                    {
                        var model = new UserCardModel();
                        model.Id = getData.Id;
                        model.Guid = getData.Guid;
                        model.Name = getData.Name;
                        model.Prefix = getData.Prefix;
                        model.FirstName = getData.FirstName;
                        model.MiddleName = getData.MiddleName;
                        model.LastName = getData.LastName;
                        model.Suffix = getData.Suffix;
                        model.Accreditations = getData.Accreditations;
                        model.PreferredName = getData.PreferredName;
                        model.MaidenName = getData.MaidenName;
                        model.Pronouns = getData.Pronouns;
                        model.ProfilePhoto = getData.ProfilePhoto != null ? "Image/Profile/" + getData.ProfilePhoto : "asset/media/avatars/blank.png";
                        model.DesignId = getData.DesignId;
                        model.UserId = getData.UserId;
                        model.Color = getData.Color;
                        model.Logo = getData.Logo != null ? "Image/Logo/" + getData.Logo : "asset/media/avatars/blank.png";
                        model.AffiliateTitle = getData.AffiliateTitle;
                        model.Department = getData.Department;
                        model.Company = getData.Company;
                        model.Headline = getData.Headline;
                        model.IsArchive = getData.IsArchive;
                        model.CreatedDateTime = getData.CreatedDateTime;
                        model.UpdatedDateTime = getData.UpdatedDateTime;
                        model.CardBadges = getData.Usercardbadges.Select(x => new UserCardBadgeModel { Id = x.Id, BadgePath = "/Image/Badge/" + x.BadgePath, CardId = x.CardId }).ToList();
                        model.CardFields = getData.Usercardfields.Select(x => new UserCardFieldModel
                        {
                            Id = x.Id,
                            CardFieldId = x.CardFieldId,
                            CardId = x.CardId,
                            Link = x.Link,
                            Description = x.Description,
                        }).ToList();
                        return Constant.Response("success", model, "");
                    }
                }
                return Constant.Response("error", new object(), Constant.NotFoundMessage);
            }
            catch (Exception ex)
            {
                return Constant.Response("error", new object(), ex.Message);
            }
        }
    }
}
