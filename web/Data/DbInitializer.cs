using web.Data;
using web.Models;
using System;
using System.Linq;

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
                new Zaposlen{Ime="Miha",Priimek="Žnidar",Naslov="Tvoja mt planina", Telefon=420420420, DatumRojstva=DateTime.Parse("2019-09-01"), DatumZaposlitve=DateTime.Parse("2019-09-01"), Spol="Ž"},
                new Zaposlen{Ime="Miha",Priimek="Žnidar",Naslov="Tvoja mt planina", Telefon=420420420, DatumRojstva=DateTime.Parse("2019-09-01"), DatumZaposlitve=DateTime.Parse("2019-09-01"), Spol="Ž"},
                new Zaposlen{Ime="Miha",Priimek="Žnidar",Naslov="Tvoja mt planina", Telefon=420420420, DatumRojstva=DateTime.Parse("2019-09-01"), DatumZaposlitve=DateTime.Parse("2019-09-01"), Spol="Ž"},
                new Zaposlen{Ime="Miha",Priimek="Žnidar",Naslov="Tvoja mt planina", Telefon=420420420, DatumRojstva=DateTime.Parse("2019-09-01"), DatumZaposlitve=DateTime.Parse("2019-09-01"), Spol="Ž"},
                new Zaposlen{Ime="Miha",Priimek="Žnidar",Naslov="Tvoja mt planina", Telefon=420420420, DatumRojstva=DateTime.Parse("2019-09-01"), DatumZaposlitve=DateTime.Parse("2019-09-01"), Spol="Ž"},
                new Zaposlen{Ime="Miha",Priimek="Žnidar",Naslov="Tvoja mt planina", Telefon=420420420, DatumRojstva=DateTime.Parse("2019-09-01"), DatumZaposlitve=DateTime.Parse("2019-09-01"), Spol="Ž"},
            };

            foreach (Zaposlen zaposlen in zaposleni)
            {
                context.Zaposleni.Add(zaposlen);
            }

            context.SaveChanges();
        }
    }
}