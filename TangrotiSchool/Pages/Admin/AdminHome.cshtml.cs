using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TangrotiSchool.Pages.Admin
{
    public class AdminHomeModel : PageModel
    {
        [Authorize]
        public void OnGet()
        {
            try
            {

            }
            catch(Exception ex)
            {
                Console.Write(ex);
            }
        }

        public async Task<IActionResult> OnPostLogOff()
        {
            try
            {
                var authenticationManager = Request.HttpContext;

                await authenticationManager.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return this.RedirectToPage("/Index");
        }
    }
}