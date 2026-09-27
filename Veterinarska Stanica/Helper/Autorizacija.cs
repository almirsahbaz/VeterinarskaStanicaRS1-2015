using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VeterinarskaStanica.Models;
using VeterinasrkaStanica.Helper;

namespace VeterinarskaStanica.Helper
{
    public class Autorizacija : TypeFilterAttribute
    {
        public Autorizacija(params KorisnickeUloge[] dozvoljeneUloge) : base(typeof(AuthorizationFilter))
        {
            Arguments = new object[] { dozvoljeneUloge };
        }
    }

    public class AuthorizationFilter : IAuthorizationFilter
    {
        private readonly KorisnickeUloge[] _dozvoljeneUloge;

        public AuthorizationFilter(params KorisnickeUloge[] dozvoljeneUloge)
        {
            _dozvoljeneUloge = dozvoljeneUloge;
        }
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            KorisnickiNalog k = Autentifikacija.GetLogiraniKorisnik(filterContext.HttpContext);

            if (k == null)
            {
                filterContext.HttpContext.Response.Redirect("/ModulLogin/Login/Login");
                return;
            }

            //provjera
            foreach (KorisnickeUloge x in _dozvoljeneUloge)
            {
                if (x == KorisnickeUloge.Administrator && k.admin != false)
                    return;
                if (x == KorisnickeUloge.Doktor && k.doc != false)
                    return;
                if (x == KorisnickeUloge.Tehnicko_osoblje && k.teh_osob != false)
                    return;
            }
            //ako funkcija nije prekinuta pomoću "return", onda korisnik nema pravo pistupa pa će se vršiti redirekcija na "/Home/Index"
            filterContext.HttpContext.Response.Redirect("/");
        }
    }
    public enum KorisnickeUloge
    {
        Tehnicko_osoblje,
        Doktor,
        Administrator
    }
}
