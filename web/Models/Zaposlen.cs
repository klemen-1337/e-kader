using System;
using System.Collections.Generic;

namespace web.Models
{
    public class Zaposlen
    {
        public int ID { get; set; }
        public string Ime { get; set; }
        public string Priimek { get; set; }
        public string Naslov { get; set; }
        public int Telefon { get; set; }
        public DateTime DatumRojstva { get; set; }
        public DateTime DatumZaposlitve { get; set; }
        public string Spol { get; set; }
        public string PhotoPath { get; set; }
        public ICollection<Zaposlitve> Zaposlitve { get; set; }
        public bool Kadrovanje { get; set;}
        
    }
}