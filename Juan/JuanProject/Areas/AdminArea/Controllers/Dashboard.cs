using Microsoft.AspNetCore.Mvc;

namespace JuanProject.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class Dashboard : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
