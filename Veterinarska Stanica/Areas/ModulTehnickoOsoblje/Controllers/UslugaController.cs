using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VeterinarskaStanica.Areas.ModulTehnickoOsoblje.Models;
using VeterinarskaStanica.DAL;
using VeterinarskaStanica.Helper;
using VeterinarskaStanica.Models;

namespace VeterinarskaStanica.Areas.ModulTehnickoOsoblje.Controllers
{
    [Area("ModulTehnickoOsoblje")]
    [Autorizacija(KorisnickeUloge.Tehnicko_osoblje)]
    public class UslugaController : Controller
    {

        private readonly VeterinarskaStanicaContext db;

        public UslugaController(VeterinarskaStanicaContext context)
        {
            db = context;
        }

        [Autorizacija(KorisnickeUloge.Tehnicko_osoblje)]
        // GET: ModulTehnickoOsoblje/Usluge
        public ActionResult Index()
        {
            var usluge = db.Uslugee;
            return View(usluge.ToList());
        }
        public ActionResult Create()
        {
            Usluga model = new Usluga
            {
                listaUsluga = db.Uslugee.ToList()
            };
            return View(model);
        }

        // POST: ModulTehnickoOsoblje/Usluge/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create(Usluga vm)
        {
            Usluge p = new Usluge();
            if (ModelState.IsValid)
            {
                p.Id = vm.Id;
                p.Naziv = vm.Naziv;
                p.DatumUsluge = vm.DatumUsluge;
                p.IsDeleted = false;

                db.Uslugee.Add(p);

                db.SaveChanges();

                TempData["Message"] = "Uspješno dodavanje!";
                return RedirectToAction("Create");
            }
            return View(vm);
        }

        // GET:  ModulTehnickoOsoblje/Usluge/Edit/5
        public ActionResult Edit(int? id)
        {
            Usluga model = db.Uslugee.Where(x => x.Id == id).Select(z => new Usluga()
            {
                Id = z.Id,
                IsDeleted = z.IsDeleted,
                Naziv = z.Naziv,
                DatumUsluge = z.DatumUsluge,

            }).Single();
            return View(model);
        }

        // POST: ModulTehnickoOsoblje/Usluge/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Edit(Usluga vm)
        {

            Usluge a = new Usluge();
            if (ModelState.IsValid)
            {
                a = db.Uslugee.Find(vm.Id);
                a.Id = vm.Id;
                a.Naziv = vm.Naziv;
                a.DatumUsluge = vm.DatumUsluge;
                a.IsDeleted = false;

                db.SaveChanges();

                TempData["Message"] = "Uspješna izmjena!";
                return RedirectToAction("Edit");
            }
            return View(vm);
        }
        public ActionResult Delete()
        {
            Usluga model = new Usluga();
            model.listaUsluga = db.Uslugee.Where(x => x.IsDeleted == false).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Obrisi(int id)
        {
            if (db.Uslugee.SingleOrDefault(x => x.Id == id) != null)
            {
                db.Uslugee.Remove(db.Uslugee.FirstOrDefault(x => x.Id == id));

                db.SaveChanges();

                return RedirectToAction("Delete");
            }

            return View("Delete");
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