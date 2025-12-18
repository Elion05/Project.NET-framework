// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MangaBook_Models;
using MangaBook_Models.NewFolder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Manga_Web.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<MangaUser> _userManager;
        private readonly SignInManager<MangaUser> _signInManager;
        private readonly MangaDbContext _context;

        public IndexModel(
            UserManager<MangaUser> userManager,
            SignInManager<MangaUser> signInManager,
            MangaDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "First Name")]
            public string Firstname { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string Lastname { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Language")]
            public string LanguageCode { get; set; }
        }

        private async Task LoadAsync(MangaUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            

            Username = userName;

            Input = new InputModel
            {
                // Corrected to use PascalCase: FirstName and LastName
                Firstname = user.FirstName,
                Lastname = user.LastName,
                PhoneNumber = phoneNumber,
                LanguageCode = user.LanguageCode
            };

            SelectList sl = new SelectList(Language.Languages.Where(l => l.Code != "- " && l.IsSystemLanguage),
                "Code", 
                "Name", 
                user.LanguageCode
            );

            ViewData["LanguageCodes"] = sl;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            if (Input.Firstname != user.FirstName)
            {
                user.FirstName = Input.Firstname;
            }

            if (Input.Lastname != user.LastName)
            {
                user.LastName = Input.Lastname;
            }

            await _userManager.UpdateAsync(user);

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            if (user.LanguageCode != Input.LanguageCode)
            {
                user.LanguageCode = Input.LanguageCode;
                _context.Update(user);
                _context.SaveChanges();
                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(user.LanguageCode)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

                await _signInManager.RefreshSignInAsync(user);
                StatusMessage = "Your profile has been updated";
                return RedirectToPage();
            }

            // Ensure a return value for all code paths
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
