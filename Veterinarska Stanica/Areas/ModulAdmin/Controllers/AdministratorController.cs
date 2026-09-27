using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using VeterinarskaStanica.Areas.ModulAdmin.Models;
using VeterinarskaStanica.Areas.ModulTehnickoOsoblje.Controllers;
using VeterinarskaStanica.DAL;
using VeterinarskaStanica.Helper;
using VeterinarskaStanica.Models;


namespace VeterinasrkaStanica.Areas.ModulAdmin.Controllers
{
    [Area("ModulAdmin")]
    [Autorizacija(KorisnickeUloge.Administrator)]
    public class AdministratorController : Controller
    {
        private readonly VeterinarskaStanicaContext db;

        public AdministratorController(VeterinarskaStanicaContext context)
        {
            this.db = context;
        }

        [Autorizacija(KorisnickeUloge.Administrator)]

        public ActionResult Home()
        {
            return View();
        }
        // GET: ModulAdmin/Administrator
        public ActionResult Index()
        {
            var administratori = db.Administratori.Include(a => a.KorisnickiNalog);
            return View(administratori.ToList());
        }

        // GET: ModulAdmin/Administrator/Create
        public ActionResult Create()
        {

            AdminDodajVM model = new AdminDodajVM
            {
                listaNaloga = db.KorisnickiNalozi.Where(x => x.admin == true && x.IsDeleted == false && x.Aktivan.Value == false).ToList(),
                listaSpreme = db.StrucneSpremee.ToList(),
                listaGradova = db.Gradovii.ToList()
            };



            return View(model);
        }

        // POST: ModulAdmin/Administrator/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create(AdminDodajVM vm)
        {
            KorisnickiNalog k = db.KorisnickiNalozi.Find(vm.KorisnickiNalogId);
            Administrator a = new Administrator();
            if (ModelState.IsValid)
            {
                a.Id = vm.KorisnickiNalogId;
                a.Ime = vm.Ime;
                a.Prezime = vm.Prezime;
                a.StrucneSpremeId = vm.StrucneSpremeId;
                a.GradoviId = vm.GradoviId;
                a.KorisnickiNalogId = vm.KorisnickiNalogId;
                a.IsDeleted = false;
                k.Aktivan = true;


                db.Administratori.Add(a);

                db.SaveChanges();



                TempData["Message"] = "Uspjesno dodavanje!";
                return RedirectToAction("Create");
            }
            vm.listaNaloga = db.KorisnickiNalozi.Where(x => x.admin == true && x.IsDeleted == false && x.Aktivan.Value == false).ToList();
            vm.listaSpreme = db.StrucneSpremee.ToList();
            vm.listaGradova = db.Gradovii.ToList();
            return View(vm);
        }

        // GET: ModulAdmin/Administrator/Edit/5
        public ActionResult Edit(int? id)
        {
            AdminDodajVM model = db.Administratori.Where(x => x.Id == id).Select(z => new AdminDodajVM()
            {
                KorisnickiNalogId = z.KorisnickiNalogId,
                Id = z.Id,
                StrucneSpremeId = z.StrucneSpremeId.Value,
                GradoviId = z.GradoviId.Value,
                IsDeleted = z.IsDeleted,
                Ime = z.Ime,
                Prezime = z.Prezime,
                listaNaloga = db.KorisnickiNalozi.Where(x => x.admin == true && x.IsDeleted == false).ToList(),
                listaSpreme = db.StrucneSpremee.ToList(),
                listaGradova = db.Gradovii.ToList(),

            }).Single();
            return View(model);
        }

        // POST: ModulAdmin/Administrator/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Edit(AdminDodajVM vm)
        {

            Administrator a;
            if (ModelState.IsValid)
            {
                a = db.Administratori.Find(vm.Id);
                a.Id = vm.KorisnickiNalogId;
                a.Ime = vm.Ime;
                a.Prezime = vm.Prezime;
                a.StrucneSpremeId = vm.StrucneSpremeId;
                a.GradoviId = vm.GradoviId;
                a.KorisnickiNalogId = vm.KorisnickiNalogId;
                a.IsDeleted = vm.IsDeleted;


                db.SaveChanges();



                TempData["Message"] = "Uspjesno izmjena!";
                return RedirectToAction("Edit");
            }
            vm.listaNaloga = db.KorisnickiNalozi.Where(x => x.admin == true && x.IsDeleted == false).ToList();
            vm.listaSpreme = db.StrucneSpremee.ToList();
            vm.listaGradova = db.Gradovii.ToList();
            return View(vm);
        }

        // POST: ModulKorisnickiNalog/KorisnickiNalogs/Delete/5
        public ActionResult Delete()
        {
            DeleteVM model = new DeleteVM();
            model.listaAdministratora = db.Administratori.Where(x => x.IsDeleted == false).ToList();

            return View(model);
        }

        public ActionResult Obrisi(DeleteVM vm)
        {
            KorisnickiNalog k = db.KorisnickiNalozi.Find(vm.AdministratorId);
            Administrator nalog;


            nalog = db.Administratori.Find(vm.AdministratorId);
            k.Aktivan = false;
            db.Administratori.Remove(nalog);

            db.SaveChanges();
            vm.listaAdministratora = db.Administratori.ToList();
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
