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
    public class GradoviController : Controller
    {
        private readonly VeterinarskaStanicaContext db;

        public GradoviController(VeterinarskaStanicaContext context)
        {
            db = context;
        }


        // GET: ModulAdmin/Gradovi/Create
        public ActionResult Create()
        {
            PodaciAddVM model = new PodaciAddVM();
            return View("Create", model);
        }

        // POST: ModulAdmin/Gradovi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(PodaciAddVM vm)
        {

            Gradovi gradovi = new Gradovi();
            if (ModelState.IsValid)
            {
                gradovi.Naziv = vm.Naziv;

                db.Gradovii.Add(gradovi);
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
