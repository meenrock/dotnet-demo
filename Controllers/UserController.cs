using dotnet_demo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_demo.Controllers
{
    public class UserController : Controller
    {
        private readonly PostgresDBContext _context;

        public UserController(PostgresDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
