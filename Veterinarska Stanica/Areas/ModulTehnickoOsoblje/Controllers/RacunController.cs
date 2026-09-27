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
    public class RacunController : Controller
    {
        private readonly VeterinarskaStanicaContext db;

        public RacunController(VeterinarskaStanicaContext context)
        {
            db = context;
        }

        [Autorizacija(KorisnickeUloge.Tehnicko_osoblje)]
        // GET: ModulTehnickoOsoblje/Racun
        public ActionResult Index()
        {
            var racun = db.Racunii;
            return View(racun.ToList());
        }
        public ActionResult Create()
        {
            Racun model = new Racun
            {
                listaRacuna = db.Racunii.ToList()
            };
            return View(model);
        }

        // POST: ModulTehnickoOsoblje/Racun/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create(Racun vm)
        {
            Racuni p = new Racuni();
            if (ModelState.IsValid)
            {
                p.Id = vm.Id;
                p.Naziv = vm.Naziv;
                p.Datum = vm.Datum;
                p.IznosZaPlatiti = vm.IznosZaPlatiti;
                p.IsDeleted = false;

                db.Racunii.Add(p);

                db.SaveChanges();

                TempData["Message"] = "Uspješno dodavanje!";
                return RedirectToAction("Create");
            }
            return View(vm);
        }

        // GET:  ModulTehnickoOsoblje/Racun/Edit/5
        public ActionResult Edit(int? id)
        {
            Racun model = db.Racunii.Where(x => x.Id == id).Select(z => new Racun()
            {
                Id = z.Id,
                IsDeleted = z.IsDeleted,
                Naziv = z.Naziv,
                Datum = z.Datum,
                IznosZaPlatiti = z.IznosZaPlatiti,


            }).Single();
            return View(model);
        }

        // POST: ModulTehnickoOsoblje/Racun/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Edit(Racun vm)
        {

            Racuni a = new Racuni();
            if (ModelState.IsValid)
            {
                a = db.Racunii.Find(vm.Id);
                a.Id = vm.Id;
                a.Naziv = vm.Naziv;
                a.Datum = vm.Datum;
                a.IznosZaPlatiti = vm.IznosZaPlatiti;
                a.IsDeleted = false;

                db.SaveChanges();

                TempData["Message"] = "Uspješna izmjena!";
                return RedirectToAction("Edit");
            }
            return View(vm);
        }
        public ActionResult Delete()
        {
            Racun model = new Racun();
            model.listaRacuna = db.Racunii.Where(x => x.IsDeleted == false).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Obrisi(int id)
        {
            if (db.Racunii.SingleOrDefault(x => x.Id == id) != null)
            {
                db.Racunii.Remove(db.Racunii.FirstOrDefault(x => x.Id == id));

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