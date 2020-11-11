using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{

    public class Izobrazevanje
    {
        public string Naziv  { get; set; }
        public DateTime Datum { get; set;}
        public string Cena { get; set; }
        public bool Redno { get; set; }
        public bool Specializirano { get; set; }
        public ICollection<DelovnaMesta> DelovnaMesta {get;set;}

    }
}