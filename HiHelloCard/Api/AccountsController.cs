using HiHelloCard.Interfaces.Service;
using HiHelloCard.Model.ViewModel;
using HiHelloCard.Model.ViewModel.ApiModel;
using HiHelloCard.Services.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace HiHelloCard.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUserService _accountService;
        private readonly AppSettings _appSettings;
        public AccountsController(IUserService accountService, IOptions<AppSettings> appSettings)
        {
            _accountService = accountService;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        public async Task<object> SignUp([FromBody] UserModel user)
        {
            return await _accountService.SignUp(user);
        }
        [HttpGet]
        public async Task<object> Signin(UserModel credentials)
        {

            return _accountService.AppLogin(credentials, _appSettings).Result.Data;

        }

        [HttpGet("confirm-email")]
        public async Task<object> ConfirmEmail(string uid, string token)
        {
            if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(token))
            {
                token = token.Replace(' ', '+');
                return await _accountService.ConfirmEmailAsync(uid, token);
            }
            else
                return new object();
        }
    }
}
