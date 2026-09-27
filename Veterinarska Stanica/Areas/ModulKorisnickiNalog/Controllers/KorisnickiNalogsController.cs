using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using VeterinarskaStanica.Areas.ModulKorisnickiNalog.Models;
using VeterinarskaStanica.Areas.ModulTehnickoOsoblje.Controllers;
using VeterinarskaStanica.DAL;
using VeterinarskaStanica.Helper;
using VeterinarskaStanica.Models;
using VeterinasrkaStanica.Helper;

namespace VeterinarskaStanica.Areas.ModulKorisnickiNalog.Controllers
{
    [Area("ModulKorisnickiNalog")]
    [Autorizacija(KorisnickeUloge.Administrator, KorisnickeUloge.Doktor, KorisnickeUloge.Tehnicko_osoblje)]
    public class KorisnickiNalogsController : Controller
    {
        private readonly VeterinarskaStanicaContext db;

        public KorisnickiNalogsController(VeterinarskaStanicaContext context)
        {
            db = context;
        }


        // GET: ModulKorisnickiNalog/KorisnickiNalogs
        public ActionResult Index()
        {
            var korisnickiNalozi = db.KorisnickiNalozi.Include(k => k.Administrator).Include(k => k.Doktor).Include(k => k.Tehnicko_osoblje).Where(z => z.IsDeleted == false);
            return View(korisnickiNalozi.ToList());
        }


        public ActionResult Create()
        {
            NaloziEditVM model = new NaloziEditVM();
            return View("Create", model);
        }


        [HttpPost]
        public ActionResult Create(NaloziEditVM model)
        {
            KorisnickiNalog nalog;
            if (ModelState.IsValid)
            {
                nalog = new KorisnickiNalog();
                db.KorisnickiNalozi.Add(nalog);
                nalog.Username = model.Username;
                nalog.Password = model.Password;
                nalog.IsDeleted = model.IsDeleted;
                nalog.admin = model.admin;
                nalog.doc = model.doc;
                nalog.teh_osob = model.teh_osob;
                nalog.IsDeleted = false;
                nalog.Aktivan = false;
                db.SaveChanges();
                TempData["Message"] = "Uspjesno dodavanje!";
                return RedirectToAction("Create");
            }



            return View(model);
        }

        // GET: ModulKorisnickiNalog/KorisnickiNalogs/Edit/5
        public ActionResult Edit(int? id)
        {
            NaloziEditVM model = db.KorisnickiNalozi.Where(x => x.Id == id).Select(z => new NaloziEditVM()
            {


                IsDeleted = z.IsDeleted,
                Username = z.Username,
                Password = z.Password,
                admin = z.admin,
                doc = z.doc,
                teh_osob = z.teh_osob

            }).Single();
            return View(model);

        }

        // POST: ModulKorisnickiNalog/KorisnickiNalogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Edit(NaloziEditVM vm)
        {
            KorisnickiNalog k;
            if (ModelState.IsValid)
            {
                k = db.KorisnickiNalozi.Find(vm.Id);
                k.Username = vm.Username;
                k.Password = vm.Password;
                k.admin = vm.admin;
                k.doc = vm.doc;
                k.teh_osob = vm.teh_osob;

                db.SaveChanges();

                TempData["Message"] = "Uspjesno izmjena!";
                return RedirectToAction("Edit", "KorisnickiNalogs");
            }
            return View(vm);
        }

        // GET: ModulKorisnickiNalog/KorisnickiNalogs/Delete/5
        public ActionResult Delete()
        {
            NalogDeleteVM model = new NalogDeleteVM();
            model.listaNaloga = db.KorisnickiNalozi.Where(x => x.IsDeleted == false).ToList();

            return View(model);
        }

        // POST: ModulKorisnickiNalog/KorisnickiNalogs/Delete/5


        public ActionResult Obrisi(NalogDeleteVM vm)
        {
            KorisnickiNalog nalog = db.KorisnickiNalozi.Find(vm.KorisnickiNalogId);
            Tehnicko_osoblje tehosob = db.Tehnicka_osoblja.Find(vm.KorisnickiNalogId);
            Administrator admin = db.Administratori.Find(vm.KorisnickiNalogId);
            Doktor doc = db.Doktori.Find(vm.KorisnickiNalogId);
            if (tehosob != null)
            {
                db.Tehnicka_osoblja.Remove(tehosob);
            }
            else if (admin != null)
            {
                db.Administratori.Remove(admin);
            }
            else
               if (doc != null)
            {
                db.Doktori.Remove(doc);
            }
            nalog.IsDeleted = true;
            nalog.Aktivan = false;

            db.SaveChanges();
            vm.listaNaloga = db.KorisnickiNalozi.ToList();
            //return RedirectToAction("Home","Administrator",new {area="ModulAdmin"});
            TempData["Message"] = "Uspjesno izmjena!";
            return RedirectToAction("Delete", "KorisnickiNalogs");

        }


        // GET: ModulKorisnickiNalog/KorisnickiNalogs/Delete/5
        public ActionResult EditNeaktivne()
        {

            NalogDeleteVM model = new NalogDeleteVM();
            model.listaNaloga = db.KorisnickiNalozi.Where(x => x.IsDeleted == true).ToList();

            return View(model);
        }

        // POST: ModulKorisnickiNalog/KorisnickiNalogs/Delete/5


        public ActionResult Aktiviraj(NalogDeleteVM vm)
        {
            KorisnickiNalog nalog;


            nalog = db.KorisnickiNalozi.Find(vm.KorisnickiNalogId);


            nalog.IsDeleted = false;
            db.SaveChanges();
            vm.listaNaloga = db.KorisnickiNalozi.ToList();
            return RedirectToAction("EditNeaktivne", "KorisnickiNalogs");
        }

        public ActionResult Izmjena()
        {
            ProfilIzmjenaVM model = new ProfilIzmjenaVM();


            return View(model);
        }

        public ActionResult Snimi(ProfilIzmjenaVM vm)
        {
            int id = Autentifikacija.GetLogiraniKorisnik(HttpContext).Id;
            KorisnickiNalog k = db.KorisnickiNalozi.Find(id);
            if (vm.StariPassword == k.Password)
            {
                k.Password = vm.NoviPassword;
                db.SaveChanges();

                TempData["Message"] = "Uspješna izmjena lozinke!";
                return RedirectToAction("Izmjena");
            }
            TempData["Message"] = "Unijeli ste pogrešnu lozinku";
            return RedirectToAction("Izmjena");


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
