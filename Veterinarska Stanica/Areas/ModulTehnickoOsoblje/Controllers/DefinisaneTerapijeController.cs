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
    public class DefinisaneTerapijeController : Controller
    {
        private readonly VeterinarskaStanicaContext db;

        public DefinisaneTerapijeController(VeterinarskaStanicaContext context)
        {
            db = context;
        }

        [Autorizacija(KorisnickeUloge.Tehnicko_osoblje)]
        // GET: ModulTehnickoOsoblje/DefinisaneTerapije
        public ActionResult Index()
        {
            var def_terapije = db.DefinsanaTerapije;
            return View(def_terapije.ToList());
        }
        public ActionResult Create()
        {
            DefinisaneTerapije model = new DefinisaneTerapije
            {
                listaTerapija = db.Terapije.ToList(),
                listaPregleda = db.Pregledii.ToList(),
                listaDefinisanihTerapija = db.DefinsanaTerapije.ToList()
            };
            return View(model);
        }

        // POST: ModulTehnickoOsoblje/DefinisaneTerapije/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create(DefinisaneTerapije vm)
        {
            DefinsanaTerapija p = new DefinsanaTerapija();
            if (ModelState.IsValid)
            {
                p.Id = vm.Id;
                p.Doziranje = vm.Doziranje;
                p.PreglediId = vm.PreglediId;
                p.TerapijaId = vm.TerapijaId;
                p.IsDeleted = false;

                db.DefinsanaTerapije.Add(p);

                db.SaveChanges();

                TempData["Message"] = "Uspješno dodavanje!";
                return RedirectToAction("Create");
            }
            vm.listaTerapija = db.Terapije.ToList();
            vm.listaPregleda = db.Pregledii.ToList();
            return View(vm);
        }

        // GET: ModulTehnickoOsoblje/DefinisaneTerapije/Edit/5
        public ActionResult Edit(int? id)
        {
            DefinisaneTerapije model = db.DefinsanaTerapije.Where(x => x.Id == id).Select(z => new DefinisaneTerapije()
            {
                Id = z.Id,
                IsDeleted = z.IsDeleted,
                Doziranje = z.Doziranje,
                PreglediId = z.PreglediId,
                TerapijaId = z.TerapijaId,
                listaTerapija = db.Terapije.ToList(),
                listaPregleda = db.Pregledii.ToList(),

            }).Single();
            return View(model);
        }

        // POST: ModulTehnickoOsoblje/DefinisaneTerapije/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Edit(DefinisaneTerapije vm)
        {

            DefinsanaTerapija a = new DefinsanaTerapija();
            if (ModelState.IsValid)
            {
                a = db.DefinsanaTerapije.Find(vm.Id);
                a.Id = vm.Id;
                a.Doziranje = vm.Doziranje;
                a.PreglediId = vm.PreglediId;
                a.TerapijaId = vm.TerapijaId;
                a.IsDeleted = false;

                db.SaveChanges();

                TempData["Message"] = "Uspješna izmjena!";
                return RedirectToAction("Edit");
            }
            vm.listaTerapija = db.Terapije.ToList();
            vm.listaPregleda = db.Pregledii.ToList();
            return View(vm);
        }
        public ActionResult Delete()
        {
            DefinisaneTerapije model = new DefinisaneTerapije();
            model.listaDefinisanihTerapija = db.DefinsanaTerapije.Where(x => x.IsDeleted == false).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Obrisi(int id)
        {
            if (db.DefinsanaTerapije.SingleOrDefault(x => x.Id == id) != null)
            {
                db.DefinsanaTerapije.Remove(db.DefinsanaTerapije.FirstOrDefault(x => x.Id == id));

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