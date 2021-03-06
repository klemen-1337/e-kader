using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using web.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<Uporabniki> _signInManager;
        private readonly UserManager<Uporabniki> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment webHostEnvironment;

        public RegisterModel(
            UserManager<Uporabniki> userManager,
            SignInManager<Uporabniki> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender, 
            IWebHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            webHostEnvironment = hostEnvironment;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            ///Dodajanje zaposlenega

             [Required]
            [Display(Name = "Ime")]
            public string Ime {get; set;}

            [Required]
            [Display(Name = "Priimek")]
            public string Priimek {get; set;}

            [Required]
            [Display(Name = "Naslov")]
            public string Naslov {get; set;}

            [Required]
            [Display(Name = "Telefon")]
            public int Telefon {get; set;}

            [Required]
            [Display(Name = "DatumRojstva")]
            [DataType(DataType.Date)]
            public DateTime DatumRojstva {get; set;}

            [Required]
            [Display(Name = "Spol")]
            public String Spol {get; set;}
            
            [Required]
            [Display(Name = "Slika")]
            public IFormFile Slika {get; set;}

            [Display(Name = "Kadrovska")]
            public Boolean Kadrovska {get; set;}

        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        private string UploadedFile(ZaposlenViewModel model)  
        {  
            string uniqueFileName = null;  
  
            if (model.Slika != null)  
            {  
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images");  
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Slika.FileName;  
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);  
                using (var fileStream = new FileStream(filePath, FileMode.Create))  
                {  
                    model.Slika.CopyTo(fileStream);  
                }  
            }  
            return uniqueFileName;  
        }    


        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var zapView = new ZaposlenViewModel{Ime=Input.Ime,Priimek=Input.Priimek,Naslov=Input.Naslov, Telefon=Input.Telefon, DatumRojstva=Input.DatumRojstva, Spol=Input.Spol, Slika=Input.Slika};
                string uniqueFileName = UploadedFile(zapView);  
                var zapTmp = new Zaposlen{Ime=Input.Ime,Priimek=Input.Priimek,Naslov=Input.Naslov, Telefon=Input.Telefon, DatumRojstva=Input.DatumRojstva, Spol=Input.Spol, PhotoPath = uniqueFileName,Kadrovanje=Input.Kadrovska};
                var user = new Uporabniki { UserName = Input.Email, Email = Input.Email, Zaposlen=zapTmp };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if(Input.Kadrovska){
                    var roleResult = await _userManager.AddToRoleAsync(user,"Manager");
                }else{
                    var roleResult = await _userManager.AddToRoleAsync(user,"Worker");
                }
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
