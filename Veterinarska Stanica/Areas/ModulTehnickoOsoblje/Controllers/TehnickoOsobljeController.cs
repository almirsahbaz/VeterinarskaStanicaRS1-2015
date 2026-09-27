using Microsoft.AspNetCore.Mvc;

namespace VeterinarskaStanica.Areas.ModulTehnickoOsoblje.Controllers
{
    [Area("ModulTehnickoOsoblje")]
    public class TehnickoOsobljeController : Controller
    {
        // GET: ModulTehnickoOsoblje/TehnickoOsoblje
        public ActionResult Home()
        {
            return View();
        }
    }
}