using HiHelloCard.Interfaces.Service;
using HiHelloCard.Model.ViewModel.ApiModel;
using HiHelloCard.Model.ViewModel.Common;
using HiHelloCard.Services.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static QRCoder.PayloadGenerator;

namespace HiHelloCard.Controllers
{
    public class AccountsController : Controller
    {

        private readonly IUserService _accountService;
        public AccountsController(IUserService accountService)
        {
            _accountService = accountService;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string uid, string token)
        {
            if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(token))
            {
                token = token.Replace(' ', '+');
                var result =  await _accountService.ConfirmEmailAsync(uid, token);
                if (result.Status == Constant.success)
                    ViewBag.EmailVerified = true;
            }
            return View();
        }
    }
}
