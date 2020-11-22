using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace web.Models
{

    public class Zaposlitve
    {
        public int ID { get; set; }
        public int ZaposlenID { get; set; }
        public int DelovnaMestaID { get; set; }
        public DateTime DatumZaposlitve { get; set; }
        public Zaposlen Zaposlen { get; set; }
        public DelovnaMesta DelovnaMesta { get; set; }

    }
}