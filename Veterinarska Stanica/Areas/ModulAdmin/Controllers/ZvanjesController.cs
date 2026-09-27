using Microsoft.AspNetCore.Mvc;
using VeterinarskaStanica.Areas.ModulAdmin.Models;
using VeterinarskaStanica.Areas.ModulTehnickoOsoblje.Controllers;
using VeterinarskaStanica.DAL;
using VeterinarskaStanica.Helper;
using VeterinarskaStanica.Models;

namespace VeterinasrkaStanica.Areas.ModulAdmin.Controllers
{
    [Area("ModulAdmin")]
    [Autorizacija(KorisnickeUloge.Administrator)]
    public class ZvanjesController : Controller
    {
        private readonly VeterinarskaStanicaContext db;

        public ZvanjesController(VeterinarskaStanicaContext context)
        {
            db = context;
        }

        // GET: ModulAdmin/Zvanjes/Create
        public ActionResult Create()
        {
            PodaciAddVM model = new PodaciAddVM();
            return View("Create", model);
        }

        // POST: ModulAdmin/Zvanjes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PodaciAddVM vm)
        {

            Zvanje zvanje = new Zvanje();
            if (ModelState.IsValid)
            {
                zvanje.Naziv = vm.Naziv;
                db.Zvanja.Add(zvanje);
                db.SaveChanges();
                TempData["Message"] = "Uspjesno dodavanje!";
                return RedirectToAction("Create");
            }
            return View(vm);
        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
