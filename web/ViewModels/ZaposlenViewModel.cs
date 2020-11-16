using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;  
using System.ComponentModel.DataAnnotations;  

namespace web.Models
{
    public class ZaposlenViewModel
    {
        public int ID { get; set; }
        public string Ime { get; set; }
        public string Priimek { get; set; }
        public string Naslov { get; set; }
        public int Telefon { get; set; }
        public DateTime DatumRojstva { get; set; }
        public DateTime DatumZaposlitve { get; set; }
        public string Spol { get; set; }
        public IFormFile Slika { get; set; }
        
    }
}