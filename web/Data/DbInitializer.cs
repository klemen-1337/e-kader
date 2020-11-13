using web.Data;
using web.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;


namespace web.Data
{
    public static class DbInitializer
    {
        public static void Initialize(EkadriContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Zaposleni.Any())
            {
                return;   // DB has been seeded
            }

            var zaposleni = new Zaposlen[]
            {
                new Zaposlen{Ime="Miha",Priimek="Žnidar",Naslov="Tvoja mt planina", Telefon=420420420, DatumRojstva=DateTime.Parse("2019-09-01"), DatumZaposlitve=DateTime.Parse("2019-09-01"), Spol="Ž"},
                new Zaposlen{Ime="Klemen",Priimek="Štefe",Naslov="Tvoja mt planina", Telefon=420420420, DatumRojstva=DateTime.Parse("2019-09-01"), DatumZaposlitve=DateTime.Parse("2019-09-01"), Spol="Ž"},
                new Zaposlen{Ime="Marjan",Priimek="Kovač",Naslov="Tvoja mt planina", Telefon=420420420, DatumRojstva=DateTime.Parse("2019-09-01"), DatumZaposlitve=DateTime.Parse("2019-09-01"), Spol="Ž"},
                new Zaposlen{Ime="Janez",Priimek="Pegam",Naslov="Tvoja mt planina", Telefon=420420420, DatumRojstva=DateTime.Parse("2019-09-01"), DatumZaposlitve=DateTime.Parse("2019-09-01"), Spol="Ž"},
                new Zaposlen{Ime="Florjan",Priimek="Gasilec",Naslov="Tvoja mt planina", Telefon=420420420, DatumRojstva=DateTime.Parse("2019-09-01"), DatumZaposlitve=DateTime.Parse("2019-09-01"), Spol="Ž"},
                new Zaposlen{Ime="Cene",Priimek="Novak",Naslov="Tvoja mt planina", Telefon=420420420, DatumRojstva=DateTime.Parse("2019-09-01"), DatumZaposlitve=DateTime.Parse("2019-09-01"), Spol="Ž"},
                new Zaposlen{Ime="Mirjan",Priimek="Salam",Naslov="Tvoja mt planina", Telefon=420420420, DatumRojstva=DateTime.Parse("2019-09-01"), DatumZaposlitve=DateTime.Parse("2019-09-01"), Spol="Ž"},
                
                
            };

            foreach (Zaposlen zaposlen in zaposleni)
            {
                context.Zaposleni.Add(zaposlen);
            }

            var hasher = new PasswordHasher<Uporabniki>();
            Zaposlen zapAdmin = new Zaposlen{Ime="admin",Priimek="admin",Naslov="Tvoja mt planina", Telefon=420420420, DatumRojstva=DateTime.Parse("2019-09-01"), DatumZaposlitve=DateTime.Parse("2019-09-01"), Spol="Ž"};
            String pass = hasher.HashPassword(null, "Admin123!");
            context.Zaposleni.Add(zapAdmin);
            context.Users.Add(new Uporabniki{ Id="a0e274cd-b2ea-462f-aa63-ff77f34c0cc1",UserName = "admin@gmail.com",NormalizedUserName="ADMIN@GMAIL.COM",NormalizedEmail="ADMIN@GMAIL.COM",Email = "admin@gmail.com",PasswordHash=pass ,Zaposlen=zapAdmin});
            
            context.SaveChanges();
        }
    }
}