using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VeterinarskaStanica.Areas.ModulDoktor.Models;
using VeterinarskaStanica.DAL;
using VeterinarskaStanica.Helper;
using VeterinarskaStanica.Models;

namespace VeterinarskaStanica.Areas.ModulDoktor.Controllers
{
    [Area("ModulDoktor")]
    [Autorizacija(KorisnickeUloge.Doktor)]
    public class DijagnozaController : Controller
    {
        private readonly VeterinarskaStanicaContext db;

        public DijagnozaController(VeterinarskaStanicaContext context)
        {
            db = context;
        }
        [Autorizacija(KorisnickeUloge.Doktor)]
        // GET: ModulDoktor/Dijagnoze
        public ActionResult Index()
        {
            var dijagnoze = db.Dijagnoze;
            return View(dijagnoze.ToList());
        }
        public ActionResult Create()
        {
            Dijagnoze model = new Dijagnoze
            {

            };
            return View(model);
        }

        // POST: ModulDoktor/Dijagnoze/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create(Dijagnoze vm)
        {
            Dijagnoza p = new Dijagnoza();
            if (ModelState.IsValid)
            {
                p.Id = vm.Id;
                p.Naziv_dijagnoze = vm.Naziv_dijagnoze;
                p.Sifra_dijagnoze = vm.Sifra_dijagnoze;
                p.IsDeleted = false;

                db.Dijagnoze.Add(p);

                db.SaveChanges();

                TempData["Message"] = "Uspješno dodavanje!";
                return RedirectToAction("Create");
            }
            return View(vm);
        }

        // GET: ModulDoktor/Dijagnoze/Edit/5
        public ActionResult Edit(int? id)
        {
            Dijagnoze model = db.Dijagnoze.Where(x => x.Id == id).Select(z => new Dijagnoze()
            {
                Id = z.Id,
                IsDeleted = z.IsDeleted,
                Naziv_dijagnoze = z.Naziv_dijagnoze,
                Sifra_dijagnoze = z.Sifra_dijagnoze,

            }).Single();
            return View(model);
        }

        // POST: ModulDoktor/Dijagnoze/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Edit(Dijagnoze vm)
        {

            Dijagnoza a = new Dijagnoza();
            if (ModelState.IsValid)
            {
                a = db.Dijagnoze.Find(vm.Id);
                a.Id = vm.Id;
                a.Naziv_dijagnoze = vm.Naziv_dijagnoze;
                a.Sifra_dijagnoze = vm.Sifra_dijagnoze;
                a.IsDeleted = false;

                db.SaveChanges();

                TempData["Message"] = "Uspješna izmjena!";
                return RedirectToAction("Edit");
            }
            return View(vm);
        }
        public ActionResult Delete()
        {
            Dijagnoze model = new Dijagnoze();
            model.listaDijagnoza = db.Dijagnoze.Where(x => x.IsDeleted == false).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Obrisi(int id)
        {
            if (db.Dijagnoze.SingleOrDefault(x => x.Id == id) != null)
            {
                db.Dijagnoze.Remove(db.Dijagnoze.FirstOrDefault(x => x.Id == id));

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