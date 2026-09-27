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
    public class UspostavljenaDijagnozaController : Controller
    {

        private readonly VeterinarskaStanicaContext db;

        public UspostavljenaDijagnozaController(VeterinarskaStanicaContext context)
        {
            db = context;
        }

        [Autorizacija(KorisnickeUloge.Doktor)]
        // GET: ModulDoktor/UspostavljenaDijagnoza
        public ActionResult Index()
        {
            var uspostavljenadijagnoza = db.UspostavljenaDijagnoze;
            return View(uspostavljenadijagnoza.ToList());
        }
        public ActionResult Create()
        {
            Uspostavljena model = new Uspostavljena
            {
                listaDijagnoza = db.Dijagnoze.ToList(),
                listaPregleda = db.Pregledii.ToList(),
                listaUspostavljenih = db.UspostavljenaDijagnoze.ToList()
            };
            return View(model);
        }

        // POST: ModulDoktor/UspostavljenaDijagnoza/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create(Uspostavljena vm)
        {
            UspostavljenaDijagnoza p = new UspostavljenaDijagnoza();
            if (ModelState.IsValid)
            {
                p.Id = vm.Id;
                p.Intezitet = vm.Intezitet;
                p.Komentar = vm.Komentar;
                p.DijagnozaId = vm.DijagnozaId;
                p.PreglediId = vm.PreglediId;
                p.IsDeleted = false;

                db.UspostavljenaDijagnoze.Add(p);

                db.SaveChanges();

                TempData["Message"] = "Uspješno dodavanje!";
                return RedirectToAction("Create");
            }
            vm.listaDijagnoza = db.Dijagnoze.ToList();
            vm.listaPregleda = db.Pregledii.ToList();
            return View(vm);
        }

        // GET: ModulDoktor/UspostavljenaDijagnoza/Edit/5
        public ActionResult Edit(int? id)
        {
            Uspostavljena model = db.UspostavljenaDijagnoze.Where(x => x.Id == id).Select(z => new Uspostavljena()
            {
                Id = z.Id,
                IsDeleted = z.IsDeleted,
                Intezitet = z.Intezitet,
                Komentar = z.Komentar,
                DijagnozaId = z.DijagnozaId,
                PreglediId = z.PreglediId,
                listaDijagnoza = db.Dijagnoze.ToList(),
                listaPregleda = db.Pregledii.ToList(),

            }).Single();
            return View(model);
        }

        // POST: ModulDoktor/UspostavljenaDijagnoza/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Edit(Uspostavljena vm)
        {

            UspostavljenaDijagnoza a = new UspostavljenaDijagnoza();
            if (ModelState.IsValid)
            {
                a = db.UspostavljenaDijagnoze.Find(vm.Id);
                a.Id = vm.Id;
                a.Intezitet = vm.Intezitet;
                a.Komentar = vm.Komentar;
                a.DijagnozaId = vm.DijagnozaId;
                a.PreglediId = vm.PreglediId;
                a.IsDeleted = false;

                db.SaveChanges();

                TempData["Message"] = "Uspješna izmjena!";
                return RedirectToAction("Edit");
            }
            vm.listaDijagnoza = db.Dijagnoze.ToList();
            vm.listaPregleda = db.Pregledii.ToList();
            return View(vm);
        }
        public ActionResult Delete()
        {
            Uspostavljena model = new Uspostavljena();
            model.listaUspostavljenih = db.UspostavljenaDijagnoze.Where(x => x.IsDeleted == false).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Obrisi(int id)
        {
            if (db.UspostavljenaDijagnoze.SingleOrDefault(x => x.Id == id) != null)
            {
                db.UspostavljenaDijagnoze.Remove(db.UspostavljenaDijagnoze.FirstOrDefault(x => x.Id == id));
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