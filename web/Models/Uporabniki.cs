using web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace web.Models
{
    public class Uporabniki : IdentityUser
    {
        public virtual Zaposlen Zaposlen {get; set;}
    }

    

}

