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
    public class UplateController : Controller
    {
        private readonly VeterinarskaStanicaContext db;

        public UplateController(VeterinarskaStanicaContext context)
        {
            db = context;
        }

        [Autorizacija(KorisnickeUloge.Tehnicko_osoblje)]
        // GET: ModulTehnickoOsoblje/Uplate
        public ActionResult Index()
        {
            var uplate = db.Uplate;
            return View(uplate.ToList());
        }
        public ActionResult Create()
        {
            Uplate model = new Uplate
            {
                listaRacuna = db.Racunii.ToList(),
                listaUplata = db.Uplate.ToList()
            };
            return View(model);
        }

        // POST: ModulTehnickoOsoblje/Uplate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create(Uplate vm)
        {
            Uplata p = new Uplata();
            if (ModelState.IsValid)
            {
                p.Id = vm.Id;
                p.Iznos = vm.Iznos;
                p.Datum = vm.Datum;
                p.RacuniId = vm.RacuniId;
                p.IsDeleted = false;

                db.Uplate.Add(p);

                db.SaveChanges();

                TempData["Message"] = "Uspješno dodavanje!";
                return RedirectToAction("Create");
            }
            vm.listaRacuna = db.Racunii.ToList();
            return View(vm);
        }

        // GET:  ModulTehnickoOsoblje/Uplate/Edit/5
        public ActionResult Edit(int? id)
        {
            Uplate model = db.Uplate.Where(x => x.Id == id).Select(z => new Uplate()
            {
                Id = z.Id,
                IsDeleted = z.IsDeleted,
                Iznos = z.Iznos,
                Datum = z.Datum,
                RacuniId = z.RacuniId,
                listaRacuna = db.Racunii.ToList(),

            }).Single();
            return View(model);
        }

        // POST: ModulTehnickoOsoblje/Uplate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Edit(Uplate vm)
        {

            Uplata a = new Uplata();
            if (ModelState.IsValid)
            {
                a = db.Uplate.Find(vm.Id);
                a.Id = vm.Id;
                a.Iznos = vm.Iznos;
                a.Datum = vm.Datum;
                a.RacuniId = vm.RacuniId;
                a.IsDeleted = false;

                db.SaveChanges();

                TempData["Message"] = "Uspješna izmjena!";
                return RedirectToAction("Edit");
            }
            vm.listaRacuna = db.Racunii.ToList();
            return View(vm);
        }
        public ActionResult Delete()
        {
            Uplate model = new Uplate();
            model.listaUplata = db.Uplate.Where(x => x.IsDeleted == false).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Obrisi(int id)
        {
            if (db.Uplate.SingleOrDefault(x => x.Id == id) != null)
            {
                db.Uplate.Remove(db.Uplate.FirstOrDefault(x => x.Id == id));

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