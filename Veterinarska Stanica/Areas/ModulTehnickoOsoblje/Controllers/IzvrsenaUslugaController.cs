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
    public class IzvrsenaUslugaController : Controller
    {
        private readonly VeterinarskaStanicaContext db;

        public IzvrsenaUslugaController(VeterinarskaStanicaContext context)
        {
            db = context;
        }

        [Autorizacija(KorisnickeUloge.Tehnicko_osoblje)]
        // GET: ModulTehnickoOsoblje/IzvrsenaUsluga
        public ActionResult Index()
        {
            var izv_usluga = db.IzvrseneUslugee;
            return View(izv_usluga.ToList());
        }
        public ActionResult Create()
        {
            IzvrsenaUsluga model = new IzvrsenaUsluga
            {
                listaRacuna = db.Racunii.ToList(),
                listaUsluga = db.Uslugee.ToList(),
                listaIzvrsenihUsluga = db.IzvrseneUslugee.ToList()
            };
            return View(model);
        }

        // POST: ModulTehnickoOsoblje/IzvrsenaUsluga/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create(IzvrsenaUsluga vm)
        {
            IzvrseneUsluge p = new IzvrseneUsluge();
            if (ModelState.IsValid)
            {
                p.Id = vm.Id;
                p.RacuniId = vm.RacuniId;
                p.UslugeId = vm.UslugeId;
                p.IsDeleted = false;

                db.IzvrseneUslugee.Add(p);

                db.SaveChanges();

                TempData["Message"] = "Uspješno dodavanje!";
                return RedirectToAction("Create");
            }
            vm.listaRacuna = db.Racunii.ToList();
            vm.listaUsluga = db.Uslugee.ToList();
            return View(vm);
        }

        // GET:  ModulTehnickoOsoblje/IzvrsenaUsluga/Edit/5
        public ActionResult Edit(int? id)
        {
            IzvrsenaUsluga model = db.IzvrseneUslugee.Where(x => x.Id == id).Select(z => new IzvrsenaUsluga()
            {
                Id = z.Id,
                IsDeleted = z.IsDeleted,
                RacuniId = z.RacuniId,
                UslugeId = z.UslugeId,
                listaRacuna = db.Racunii.ToList(),
                listaUsluga = db.Uslugee.ToList(),

            }).Single();
            return View(model);
        }

        // POST: ModulTehnickoOsoblje/IzvrsenaUsluga/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Edit(IzvrsenaUsluga vm)
        {

            IzvrseneUsluge a = new IzvrseneUsluge();
            if (ModelState.IsValid)
            {
                a = db.IzvrseneUslugee.Find(vm.Id);
                a.Id = vm.Id;
                a.RacuniId = vm.RacuniId;
                a.UslugeId = vm.UslugeId;
                a.IsDeleted = false;

                db.SaveChanges();

                TempData["Message"] = "Uspješna izmjena!";
                return RedirectToAction("Edit");
            }
            vm.listaRacuna = db.Racunii.ToList();
            vm.listaUsluga = db.Uslugee.ToList();
            return View(vm);
        }
        public ActionResult Delete()
        {
            IzvrsenaUsluga model = new IzvrsenaUsluga();
            model.listaIzvrsenihUsluga = db.IzvrseneUslugee.Where(x => x.IsDeleted == false).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Obrisi(int id)
        {
            if (db.IzvrseneUslugee.SingleOrDefault(x => x.Id == id) != null)
            {
                db.IzvrseneUslugee.Remove(db.IzvrseneUslugee.FirstOrDefault(x => x.Id == id));

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