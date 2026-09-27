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
    public class TerapijeController : Controller
    {
        private readonly VeterinarskaStanicaContext db;

        public TerapijeController(VeterinarskaStanicaContext context)
        {
            db = context;
        }

        [Autorizacija(KorisnickeUloge.Tehnicko_osoblje)]
        // GET: ModulTehnickoOsoblje/Terapije
        public ActionResult Index()
        {
            var terapije = db.Terapije;
            return View(terapije.ToList());
        }
        public ActionResult Create()
        {
            Terapije model = new Terapije
            {
                listaTerapija = db.Terapije.ToList()
            };
            return View(model);
        }

        // POST: ModulTehnickoOsoblje/Terapije/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create(Terapije vm)
        {
            Terapija p = new Terapija();
            if (ModelState.IsValid)
            {
                p.Id = vm.Id;
                p.Vrsta = vm.Vrsta;
                p.DatumTerapije = vm.DatumTerapije;
                p.IsDeleted = false;

                db.Terapije.Add(p);

                db.SaveChanges();

                TempData["Message"] = "Uspješno dodavanje!";
                return RedirectToAction("Create");
            }
            return View(vm);
        }

        // GET:  ModulTehnickoOsoblje/Terapije/Edit/5
        public ActionResult Edit(int? id)
        {
            Terapije model = db.Terapije.Where(x => x.Id == id).Select(z => new Terapije()
            {
                Id = z.Id,
                IsDeleted = z.IsDeleted,
                Vrsta = z.Vrsta,
                DatumTerapije = z.DatumTerapije,

            }).Single();
            return View(model);
        }

        // POST: ModulTehnickoOsoblje/Terapije/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Edit(Terapije vm)
        {

            Terapija a = new Terapija();
            if (ModelState.IsValid)
            {
                a = db.Terapije.Find(vm.Id);
                a.Id = vm.Id;
                a.Vrsta = vm.Vrsta;
                a.DatumTerapije = vm.DatumTerapije;
                a.IsDeleted = false;

                db.SaveChanges();

                TempData["Message"] = "Uspješna izmjena!";
                return RedirectToAction("Edit");
            }
            return View(vm);
        }
        public ActionResult Delete()
        {
            Terapije model = new Terapije();
            model.listaTerapija = db.Terapije.Where(x => x.IsDeleted == false).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Obrisi(int id)
        {
            if (db.Terapije.SingleOrDefault(x => x.Id == id) != null)
            {
                db.Terapije.Remove(db.Terapije.FirstOrDefault(x => x.Id == id));

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