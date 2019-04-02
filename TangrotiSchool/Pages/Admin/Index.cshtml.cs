using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TangrotiSchool.Models.DB;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace TangrotiSchool.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly SchoolTangrotiContext databaseManager;

        /// <summary>
        /// Default constructor intialization
        /// </summary>
        /// <param name="databaseMangerContext">DataBase Manager context</param>
        public IndexModel(SchoolTangrotiContext databaseMangerContext)
        {
            try
            {

                this.databaseManager = databaseMangerContext;
            }
            catch(Exception ex)
            {
                Console.Write(ex);
            }
        }

        [BindProperty]
        public LoginViewModel LoginModel { get; set; }

        public IActionResult OnGet()
        {
            try
            {
                if(this.User.Identity.IsAuthenticated)
                {
                    return this.RedirectToPage("/Admin/AdminHome");
                }

            }
            catch(Exception ex)
            {
                Console.Write(ex);
            }

            return this.Page();
        }

        #region On Post Login Method

        public async Task<IActionResult> OnPostLogin()
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var loginInfo =await this.databaseManager.LoginByUsernamePasswords(this.LoginModel.Username, this.LoginModel.Password);

                    if(loginInfo !=null && loginInfo.Count ()>0)
                    {
                        var logindetails = loginInfo.First();

                        await this.SignInUser(logindetails.Username, false);
                        return this.RedirectToPage("/Admin/AdminHome");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid UserName or Password");
                    }
                }
            }
            catch(Exception ex)
            {
                Console.Write(ex);
            }

            return this.Page();
        }

        private async Task SignInUser(string userName , bool isPresistent)
        {
            var claims = new List<Claim>();
           try
            {
                claims.Add(new Claim(ClaimTypes.Name, userName));
                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimPrinciple = new ClaimsPrincipal(claimIdentity);
                var authenticationManager = Request.HttpContext;

                await authenticationManager.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrinciple, new AuthenticationProperties() { IsPersistent = isPresistent });


            }
            catch(Exception ex)
            {

            }
        }

        #endregion
    }
}