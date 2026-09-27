using Microsoft.AspNetCore.Http;
using Veterinarska_Stanica.Helper;
using VeterinarskaStanica.Models;

namespace VeterinasrkaStanica.Helper
{
    public class Autentifikacija
    {
        public const string LogiraniKorisnik = "logirani_korisnik";

        public static void PokreniNovuSesiju(KorisnickiNalog korisnik, HttpContext context)
        {
            context.Session.Set(LogiraniKorisnik, korisnik);
        }

        public static KorisnickiNalog GetLogiraniKorisnik(HttpContext context)
        {
            return context.Session.Get<KorisnickiNalog>(LogiraniKorisnik);
        }
    }
}