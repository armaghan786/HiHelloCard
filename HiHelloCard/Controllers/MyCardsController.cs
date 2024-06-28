using HiHelloCard.Interfaces.Service;
using HiHelloCard.Model.Response;
using HiHelloCard.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HiHelloCard.Controllers
{
    public class MyCardsController : Controller
    {
        private readonly IUserCardService _userCardService;
        public MyCardsController(IUserCardService userCardService)
        {
            _userCardService = userCardService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(string guid) 
        {
            UserCardModel model = new UserCardModel();
            if (!string.IsNullOrEmpty(guid))
                model = (UserCardModel)_userCardService.CardDetails(guid).Result.Data;
            return View(model);
        }
    }
}
