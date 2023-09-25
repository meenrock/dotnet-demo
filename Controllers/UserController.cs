using Microsoft.AspNetCore.Mvc;

namespace dotnet_demo.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
