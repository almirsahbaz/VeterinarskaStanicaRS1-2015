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
    public class KupljeniLijekController : Controller
    {
        private readonly VeterinarskaStanicaContext db;

        public KupljeniLijekController(VeterinarskaStanicaContext context)
        {
            db = context;
        }

        [Autorizacija(KorisnickeUloge.Tehnicko_osoblje)]
        // GET: ModulTehnickoOsoblje/KupljeniLijek
        public ActionResult Index()
        {
            var kup_lijek = db.KupljeniLijekovii;
            return View(kup_lijek.ToList());
        }
        public ActionResult Create()
        {
            KupljeniLijek model = new KupljeniLijek
            {
                listaRacuna = db.Racunii.ToList(),
                listaLijekova = db.Lijekovii.ToList(),
                listaKupljenihLijekova = db.KupljeniLijekovii.ToList()
            };
            return View(model);
        }

        // POST: ModulTehnickoOsoblje/KupljeniLijek/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create(KupljeniLijek vm)
        {
            KupljeniLijekovi p = new KupljeniLijekovi();
            if (ModelState.IsValid)
            {
                p.Id = vm.Id;
                p.Kolicina = vm.Kolicina;
                p.LijekoviId = vm.LijekoviId;
                p.RacuniId = vm.RacuniId;
                p.IsDeleted = false;

                db.KupljeniLijekovii.Add(p);

                db.SaveChanges();

                TempData["Message"] = "Uspješno dodavanje!";
                return RedirectToAction("Create");
            }
            vm.listaRacuna = db.Racunii.ToList();
            vm.listaLijekova = db.Lijekovii.ToList();
            return View(vm);
        }

        // GET:  ModulTehnickoOsoblje/KupljeniLijek/Edit/5
        public ActionResult Edit(int? id)
        {
            KupljeniLijek model = db.KupljeniLijekovii.Where(x => x.Id == id).Select(z => new KupljeniLijek()
            {
                Id = z.Id,
                IsDeleted = z.IsDeleted,
                Kolicina = z.Kolicina,
                RacuniId = z.RacuniId,
                LijekoviId = z.LijekoviId,
                listaRacuna = db.Racunii.ToList(),
                listaLijekova = db.Lijekovii.ToList(),

            }).Single();
            return View(model);
        }

        // POST: ModulTehnickoOsoblje/KupljeniLijek/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Edit(KupljeniLijek vm)
        {

            KupljeniLijekovi a = new KupljeniLijekovi();
            if (ModelState.IsValid)
            {
                a = db.KupljeniLijekovii.Find(vm.Id);
                a.Id = vm.Id;
                a.Kolicina = vm.Kolicina;
                a.RacuniId = vm.RacuniId;
                a.LijekoviId = vm.LijekoviId;
                a.IsDeleted = false;

                db.SaveChanges();

                TempData["Message"] = "Uspješna izmjena!";
                return RedirectToAction("Edit");
            }
            vm.listaRacuna = db.Racunii.ToList();
            vm.listaLijekova = db.Lijekovii.ToList();
            return View(vm);
        }
        public ActionResult Delete()
        {
            KupljeniLijek model = new KupljeniLijek();
            model.listaKupljenihLijekova = db.KupljeniLijekovii.Where(x => x.IsDeleted == false).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Obrisi(int id)
        {
            if (db.KupljeniLijekovii.SingleOrDefault(x => x.Id == id) != null)
            {
                db.KupljeniLijekovii.Remove(db.KupljeniLijekovii.FirstOrDefault(x => x.Id == id));

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