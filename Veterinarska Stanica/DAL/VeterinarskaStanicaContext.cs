using Microsoft.EntityFrameworkCore;
using VeterinarskaStanica.Models;

namespace VeterinarskaStanica.DAL
{
    public class VeterinarskaStanicaContext : DbContext
    {
        public VeterinarskaStanicaContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Administrator> Administratori { get; set; }

        public DbSet<DefinsanaTerapija> DefinsanaTerapije { get; set; }

        public DbSet<Dijagnoza> Dijagnoze { get; set; }

        public DbSet<Doktor> Doktori { get; set; }

        public DbSet<Gradovi> Gradovii { get; set; }

        public DbSet<IzvrseneUsluge> IzvrseneUslugee { get; set; }

        public DbSet<KorisnickiNalog> KorisnickiNalozi { get; set; }

        public DbSet<KupljeniLijekovi> KupljeniLijekovii { get; set; }

        public DbSet<Lijekovi> Lijekovii { get; set; }

        public DbSet<Odjeli> Odjelii { get; set; }

        public DbSet<Pacijent> Pacijenti { get; set; }

        public DbSet<Pregledi> Pregledii { get; set; }

        public DbSet<Racuni> Racunii { get; set; }

        public DbSet<StrucneSpreme> StrucneSpremee { get; set; }

        public DbSet<Tehnicko_osoblje> Tehnicka_osoblja { get; set; }

        public DbSet<Terapija> Terapije { get; set; }

        public DbSet<Uplata> Uplate { get; set; }

        public DbSet<Usluge> Uslugee { get; set; }

        public DbSet<UspostavljenaDijagnoza> UspostavljenaDijagnoze { get; set; }

        public DbSet<Vlasnik> Vlasnici { get; set; }

        public DbSet<Vrsta> Vrste { get; set; }

        public DbSet<Zvanje> Zvanja { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<KorisnickiNalog>().HasOptional(x => x.Doktor).WithRequired(x => x.KorisnickiNalog);
            //modelBuilder.Entity<KorisnickiNalog>().HasOptional(x => x.Administrator).WithRequired(x => x.KorisnickiNalog);
            //modelBuilder.Entity<KorisnickiNalog>().HasOptional(x => x.Tehnicko_osoblje).WithRequired(x => x.KorisnickiNalog);

        }

        public DbSet<VeterinarskaStanica.Models.Uloge> Uloges { get; set; }
    }
}
