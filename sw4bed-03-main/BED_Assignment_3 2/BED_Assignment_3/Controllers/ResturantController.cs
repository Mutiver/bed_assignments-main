using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;

namespace BED_Assignment_3.Controllers
{
    public class ResturantController : Controller
    {
        //public async Task<IActionResult> OnPostAsync(string returnUrl= null)
        //{
        //    returnUrl ??= Url.Content("~/");
        //    ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        //    if (ModelState.IsValid)
        //    {
        //        var user = CreateUser();
        //        await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
        //        await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
        //        var result = await _userManager.CreateAsync(user, Input.Password);
        //        if (result.Succeeded)
        //        {
        //            // Add a new claim for the user
        //            var nameClaim = newClaim("FullName", Input.FullName);
        //            await _userManager.AddClaimAsync(user, nameClaim);

        //        }
        //    }
        //}
    }
}
