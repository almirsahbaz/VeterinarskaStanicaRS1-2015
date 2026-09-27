using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using VeterinarskaStanica.Areas.ModulAdmin.Models;
using VeterinarskaStanica.DAL;
using VeterinarskaStanica.Helper;
using VeterinarskaStanica.Models;

namespace VeterinasrkaStanica.Areas.ModulAdmin.Controllers
{
    [Area("ModulAdmin")]
    [Autorizacija(KorisnickeUloge.Administrator)]
    public class DoktorController : Controller
    {
        private readonly VeterinarskaStanicaContext db;

        public DoktorController(VeterinarskaStanicaContext context)
        {
            db = context;
        }

        // GET: ModulAdmin/Doktor
        public IActionResult Index()
        {
            var doktori = db.Doktori.Include(d => d.KorisnickiNalog);
            return View(doktori.ToList());
        }

        // GET: ModulAdmin/Doktor/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Doktor doktor = db.Doktori.Find(id);
            if (doktor == null)
            {
                return HttpNotFound();
            }
            return View(doktor);
        }

        private IActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        // GET: ModulAdmin/Doktor/Create
        // GET: ModulAdmin/Administrator/Create
        public IActionResult Create()
        {
            DoktorDodajVM model = new DoktorDodajVM
            {
                listaNaloga = db.KorisnickiNalozi.Where(x => x.doc == true && x.IsDeleted == false && x.Aktivan.Value == false).ToList(),
                listaZavnja = db.Zvanja.ToList(),
                listaGradova = db.Gradovii.ToList()
            };



            return View(model);
        }

        // POST: ModulAdmin/Administrator/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public IActionResult Create(DoktorDodajVM vm)
        {
            KorisnickiNalog k = db.KorisnickiNalozi.Find(vm.KorisnickiNalogId);
            Doktor a = new Doktor();
            if (ModelState.IsValid)
            {
                a.Id = vm.KorisnickiNalogId;
                a.Ime = vm.Ime;
                a.Prezime = vm.Prezime;
                a.ZvanjeId = vm.ZvanjeId;
                a.GradoviId = vm.GradoviId;
                a.KorisnickiNalogId = vm.KorisnickiNalogId;
                a.Pozicija = vm.Pozicija;
                a.IsDeleted = false;
                k.Aktivan = true;
                db.Doktori.Add(a);
                db.SaveChanges();


                TempData["Message"] = "Uspjesno dodavanje!";
                return RedirectToAction("Create");
            }
            vm.listaNaloga = db.KorisnickiNalozi.Where(x => x.doc == true && x.IsDeleted == false && x.Aktivan.Value == false).ToList();
            vm.listaZavnja = db.Zvanja.ToList();
            vm.listaGradova = db.Gradovii.ToList();
            return View(vm);
        }
        // GET: ModulAdmin/Doktor/Edit/5
        public ActionResult Edit(int? id)
        {
            DoktorDodajVM model = db.Doktori.Where(x => x.Id == id).Select(z => new DoktorDodajVM
            {
                KorisnickiNalogId = z.KorisnickiNalogId,
                Id = z.Id,
                GradoviId = z.GradoviId.Value,
                IsDeleted = z.IsDeleted,
                ZvanjeId = z.ZvanjeId.Value,
                Ime = z.Ime,
                Prezime = z.Prezime,
                Pozicija = z.Pozicija,
                listaNaloga = db.KorisnickiNalozi.Where(x => x.admin == true && x.IsDeleted == false).ToList(),
                listaZavnja = db.Zvanja.ToList(),
                listaGradova = db.Gradovii.ToList(),

            }).Single();
            return View(model);
        }

        // POST: ModulAdmin/Doktor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(DoktorDodajVM vm)
        {
            Doktor a;
            if (ModelState.IsValid)
            {
                a = db.Doktori.Find(vm.Id);

                a.Ime = vm.Ime;
                a.Prezime = vm.Prezime;
                a.ZvanjeId = vm.ZvanjeId;
                a.GradoviId = vm.GradoviId;
                a.KorisnickiNalogId = vm.KorisnickiNalogId;
                a.Pozicija = vm.Pozicija;
                a.IsDeleted = vm.IsDeleted;


                db.SaveChanges();



                TempData["Message"] = "Uspjesno izmjena!";
                return RedirectToAction("Edit");
            }
            vm.listaNaloga = db.KorisnickiNalozi.Where(x => x.admin == true && x.IsDeleted == false).ToList();
            vm.listaZavnja = db.Zvanja.ToList();
            vm.listaGradova = db.Gradovii.ToList();
            return View(vm);
        }

        // POST: ModulKorisnickiNalog/KorisnickiNalogs/Delete/5


        public ActionResult Delete()
        {
            DeleteVM model = new DeleteVM();
            model.listaDoktora = db.Doktori.Where(x => x.IsDeleted == false).ToList();

            return View(model);
        }

        public ActionResult Obrisi(DeleteVM vm)
        {
            Doktor nalog;


            nalog = db.Doktori.Find(vm.DoktorId);

            db.Doktori.Remove(nalog);

            db.SaveChanges();
            vm.listaDoktora = db.Doktori.ToList();
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
