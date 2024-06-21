using HiHelloCard.Interfaces.Service;
using HiHelloCard.Model.Response;
using HiHelloCard.Model.ViewModel;
using HiHelloCard.Services.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HiHelloCard.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly IUserCardService _userCardService;
        ClientDataTableRequest _clientData;
        public CardController(IUserCardService userCardService)
        {
            _userCardService = userCardService;
            _clientData = new ClientDataTableRequest();
        }

        [Authorize]
        [HttpGet]
        public async Task<object> List(int PageNumber, int RowsOfPage)
        {
            var data = Constant.ReturnData(HttpContext);
            _clientData.start = PageNumber;
            _clientData.length = RowsOfPage;
            return await _userCardService.LoadData(_clientData, data.UserGUID);
        }

        [HttpPost]
        [Authorize]
        public async Task<object> AddEdit(UserCardModel cardmodel)
        {
            var data = Constant.ReturnData(HttpContext);
            return await _userCardService.AddEditCard(cardmodel, data.UserId, Request.Form.Files);
        }

        [HttpGet]
        [Authorize]
        public async Task<object> Detail(string guid)
        {
            return  _userCardService.CardDetails(guid).Result.Data;
        }
    }
}
