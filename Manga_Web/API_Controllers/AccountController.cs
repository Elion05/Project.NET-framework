using Microsoft.AspNetCore.Identity;
using MangaBook_Models;
using Microsoft.AspNetCore.Mvc;


namespace Manga_Web.API_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        SignInManager<MangaUser> _signInManager;

        public AccountController(SignInManager<MangaUser> signInManager)
        {
            _signInManager = signInManager;
        }

        //POST: api/Account/Login
        [HttpPost]
        [Route("/api/Login")]

        public async Task<IActionResult> LogIn([FromBody] LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                // Haal de user op uit de database
                var user = await _signInManager.UserManager.FindByNameAsync(model.Username);
                if (user != null)
                {
                    return Ok(user); // Stuur het user-object terug
                }
                return NotFound();
            }
            return Unauthorized();
        }

        public class LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
