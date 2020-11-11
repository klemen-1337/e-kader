using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{

    public class DelovnaMesta
    {
        public int DelovnoMestoID { get; set; }

        public string Oddelek { get; set;}

        public string Lokacija { get; set; }
        public Zaposlen Zaposlen { get; set; }
        public string NazivDelovnegaMesta { get; set; }
        public ICollection<Zaposlen> Zaposleni {get;set;}

    }
}