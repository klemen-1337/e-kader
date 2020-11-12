using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{
    public class DelovneUre
    {
        public int ID { get; set; }
        public DateTime Datum { get; set; }
        public DateTime UraZacetka { get; set; }
        public DateTime UraKonca { get; set; }
         public ICollection<Zaposlen> Zaposleni {get;set;}
    }
}