using Microsoft.AspNetCore.Mvc;

namespace dotnet_demo.Repositories
{
    public class PostgresRepository : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
