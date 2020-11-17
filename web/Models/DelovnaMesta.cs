using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{

    public class DelovnaMesta
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DelovnaMestaID { get; set; }

        public string Oddelek { get; set;}

        public string Lokacija { get; set; }
        public string NazivDelovnegaMesta { get; set; }
        public ICollection<Zaposlitve> Zaposlitve {get;set;}

    }
}