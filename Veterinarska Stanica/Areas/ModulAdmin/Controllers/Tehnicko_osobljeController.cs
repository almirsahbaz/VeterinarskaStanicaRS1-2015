using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using VeterinarskaStanica.Areas.ModulAdmin.Models;
using VeterinarskaStanica.DAL;
using VeterinarskaStanica.Helper;
using VeterinarskaStanica.Models;

namespace VeterinasrkaStanica.Areas.ModulAdmin.Controllers
{
    [Area("ModulAdmin")]
    [Autorizacija(KorisnickeUloge.Administrator)]
    public class Tehnicko_osobljeController : Controller
    {
        private readonly VeterinarskaStanicaContext db;

        public Tehnicko_osobljeController(VeterinarskaStanicaContext context)
        {
            db = context;
        }

        // GET: ModulAdmin/Tehnicko_osoblje
        public ActionResult Index()
        {
            var tehnicka_osoblja = db.Tehnicka_osoblja.Include(t => t.KorisnickiNalog);
            return View(tehnicka_osoblja.ToList());
        }

        // GET: ModulAdmin/Tehnicko_osoblje/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Tehnicko_osoblje tehnicko_osoblje = db.Tehnicka_osoblja.Find(id);
            if (tehnicko_osoblje == null)
            {
                return NotFound();
            }
            return View(tehnicko_osoblje);
        }

        public ActionResult Create()
        {


            TehnicarDodajVM model = new TehnicarDodajVM()
            {
                listaNaloga = db.KorisnickiNalozi.Where(x => x.teh_osob == true && x.IsDeleted == false && x.Aktivan.Value == false).ToList(),
                listaOdjela = db.Odjelii.ToList(),
                listaGradova = db.Gradovii.ToList()
            };



            return View("Create", model);
        }

        // POST: ModulAdmin/Administrator/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TehnicarDodajVM vm)
        {
            Tehnicko_osoblje a = new Tehnicko_osoblje();
            KorisnickiNalog k = db.KorisnickiNalozi.Find(vm.KorisnickiNalogId);
            if (ModelState.IsValid)
            {


                a.Id = vm.KorisnickiNalogId;
                a.Ime = vm.Ime;
                a.Prezime = vm.Prezime;
                a.OdjeliId = vm.OdjeliId;
                a.GradoviId = vm.GradoviId;
                a.KorisnickiNalogId = vm.KorisnickiNalogId;
                a.Vrsta_posla = vm.Vrsta_posla;
                a.IsDeleted = false;
                k.Aktivan = true;


                db.Tehnicka_osoblja.Add(a);
                db.SaveChanges();



                TempData["Message"] = "Uspjesno dodavanje!";
                return RedirectToAction("Create");

            }

            vm.listaNaloga = db.KorisnickiNalozi.Where(x => x.teh_osob == true && x.IsDeleted == false && x.Aktivan.Value == false).ToList();
            vm.listaOdjela = db.Odjelii.ToList();
            vm.listaGradova = db.Gradovii.ToList();
            TempData["Message"] = "Nije moguće unije postojećeg korisnika !";
            return View(vm);
        }

        // GET: ModulAdmin/Tehnicko_osoblje/Edit/5
        public ActionResult Edit(int? id)
        {
            TehnicarDodajVM model = db.Tehnicka_osoblja.Where(x => x.Id == id).Select(z => new TehnicarDodajVM()
            {
                KorisnickiNalogId = z.KorisnickiNalogId,
                Id = z.Id,
                GradoviId = z.GradoviId.Value,
                IsDeleted = z.IsDeleted,
                Vrsta_posla = z.Vrsta_posla,
                Ime = z.Ime,
                Prezime = z.Prezime,
                OdjeliId = z.OdjeliId,
                listaNaloga = db.KorisnickiNalozi.Where(x => x.admin == true && x.IsDeleted == false && x.Aktivan.Value == false).ToList(),
                listaOdjela = db.Odjelii.ToList(),
                listaGradova = db.Gradovii.ToList(),

            }).Single();
            return View(model);
        }

        // POST: ModulAdmin/Tehnicko_osoblje/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TehnicarDodajVM vm)
        {
            Tehnicko_osoblje a;
            if (ModelState.IsValid)
            {
                a = db.Tehnicka_osoblja.Find(vm.Id);
                a.Id = vm.KorisnickiNalogId;
                a.Ime = vm.Ime;
                a.Prezime = vm.Prezime;
                a.OdjeliId = vm.OdjeliId;
                a.GradoviId = vm.GradoviId;
                a.Vrsta_posla = vm.Vrsta_posla;
                a.KorisnickiNalogId = vm.KorisnickiNalogId;
                a.IsDeleted = vm.IsDeleted;


                db.SaveChanges();



                TempData["Message"] = "Uspjesno izmjena!";
                return RedirectToAction("Edit");
            }
            vm.listaNaloga = db.KorisnickiNalozi.Where(x => x.admin == true && x.IsDeleted == false).ToList();
            vm.listaOdjela = db.Odjelii.ToList();
            vm.listaGradova = db.Gradovii.ToList();
            return View(vm);
        }

        // POST: ModulKorisnickiNalog/KorisnickiNalogs/Delete/5
        public ActionResult Delete()
        {
            DeleteVM model = new DeleteVM();
            model.listaTehnicara = db.Tehnicka_osoblja.Where(x => x.IsDeleted == false).ToList();

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Obrisi(DeleteVM vm)
        {
            Tehnicko_osoblje nalog;


            nalog = db.Tehnicka_osoblja.Find(vm.Tehnicko_osobljeId);

            db.Tehnicka_osoblja.Remove(nalog);

            db.SaveChanges();
            vm.listaTehnicara = db.Tehnicka_osoblja.ToList();
            //return RedirectToAction("Home","Administrator",new {area="ModulAdmin"});
            TempData["Message"] = "Uspjesno izmjena!";
            return RedirectToAction("Delete");

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
