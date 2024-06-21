using HiHelloCard.Interfaces.Service;
using HiHelloCard.Model.Response;
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

        public IActionResult Details(string guid) 
        {
            var card = _userCardService.CardDetails(guid);
            if (card == null)
            {
                return NotFound();
            }
            return View(card);
        }
    }
}
