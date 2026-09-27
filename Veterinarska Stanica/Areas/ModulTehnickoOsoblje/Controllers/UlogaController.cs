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
    public class UlogaController : Controller
    {
        private readonly VeterinarskaStanicaContext db;

        public UlogaController(VeterinarskaStanicaContext context)
        {
            db = context;
        }

        [Autorizacija(KorisnickeUloge.Tehnicko_osoblje)]
        // GET: ModulTehnickoOsoblje/Uloga
        public ActionResult Index()
        {
            var uloga = db.Uloges;
            return View(uloga.ToList());
        }
        public ActionResult Create()
        {
            Uloga model = new Uloga
            {
                listaUloga = db.Uloges.ToList()
            };
            return View(model);
        }

        // POST: ModulTehnickoOsoblje/Uloga/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create(Uloga vm)
        {
            Uloge p = new Uloge();
            if (ModelState.IsValid)
            {
                p.Id = vm.Id;
                p.NazivUloge = vm.NazivUloge;
                p.IsDeleted = false;

                db.Uloges.Add(p);

                db.SaveChanges();

                TempData["Message"] = "Uspješno dodavanje!";
                return RedirectToAction("Create");
            }
            return View(vm);
        }

        // GET:  ModulTehnickoOsoblje/Uloga/Edit/5
        public ActionResult Edit(int? id)
        {
            Uloga model = db.Uloges.Where(x => x.Id == id).Select(z => new Uloga()
            {
                Id = z.Id,
                IsDeleted = z.IsDeleted,
                NazivUloge = z.NazivUloge,

            }).Single();
            return View(model);
        }

        // POST: ModulTehnickoOsoblje/Uloga/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Edit(Uloga vm)
        {

            Uloge a = new Uloge();
            if (ModelState.IsValid)
            {
                a = db.Uloges.Find(vm.Id);
                a.Id = vm.Id;
                a.NazivUloge = vm.NazivUloge;
                a.IsDeleted = false;

                db.SaveChanges();

                TempData["Message"] = "Uspješna izmjena!";
                return RedirectToAction("Edit");
            }
            return View(vm);
        }
        public ActionResult Delete()
        {
            Uloga model = new Uloga();
            model.listaUloga = db.Uloges.Where(x => x.IsDeleted == false).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Obrisi(int id)
        {
            if (db.Uloges.SingleOrDefault(x => x.Id == id) != null)
            {
                db.Uloges.Remove(db.Uloges.FirstOrDefault(x => x.Id == id));

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