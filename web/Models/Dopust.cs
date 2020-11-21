using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{
    public class Dopust
    {
        public int ID { get; set; }
        public DateTime Datum { get; set; }
        public DateTime UraZacetka { get; set; }
        public DateTime UraKonca { get; set; }
       public Uporabniki? Uporabnik {get;set;}
         
    }
}