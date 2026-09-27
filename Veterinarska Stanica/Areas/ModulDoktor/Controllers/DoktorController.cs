using Microsoft.AspNetCore.Mvc;

namespace VeterinarskaStanica.Areas.ModulDoktor.Controllers
{
    [Area("ModulDoktor")]
    public class DoktorController : Controller
    {
        // GET: ModulDoktor/Doktor
        public ActionResult Home()
        {
            return View();
        }
    }
}