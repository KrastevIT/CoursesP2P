﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using CoursesP2P.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using CoursesP2P.Services.ReCaptcha;
using Courses.P2P.Common;

namespace CoursesP2P.App.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;
        private readonly IReCAPTCHAService reCAPTCHAService;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IReCAPTCHAService reCAPTCHAService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
            this.reCAPTCHAService = reCAPTCHAService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = ErrorMessages.RequiredFirsName)]
            [MinLength(2, ErrorMessage = ErrorMessages.FirstNameLength)]
            [MaxLength(50)]
            public string FirstName { get; set; }

            [Required(ErrorMessage = ErrorMessages.RequiredLastName)]
            [MinLength(2, ErrorMessage = ErrorMessages.LastNameLength)]
            [MaxLength(50)]
            public string LastName { get; set; }

            [Required(ErrorMessage = ErrorMessages.RequiredBirthday)]
            [DataType(DataType.Date)]
            public DateTime Birthday { get; set; }

            [Required(ErrorMessage = ErrorMessages.RequiredCity)]
            [MinLength(2, ErrorMessage = ErrorMessages.CityLength)]
            [MaxLength(50)]
            public string City { get; set; }

            [Required(ErrorMessage = ErrorMessages.RequiredEmail)]
            [EmailAddress]
            public string Email { get; set; }

            [Required(ErrorMessage = ErrorMessages.RequiredPassword)]
            [MinLength(6, ErrorMessage = ErrorMessages.PasswordLength)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = ErrorMessages.PasswordNotMatch)]
            public string ConfirmPassword { get; set; }

            [Required]
            public string Token { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            var ReCaptcha = this.reCAPTCHAService.VerifyAsync(Input.Token);
            if (!ReCaptcha.Result.Success && ReCaptcha.Result.Score <= 0.5)
            {
                ModelState.AddModelError(string.Empty, "Your are Not Humman!");
                return Page();
            }

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    Birthday = Input.Birthday,
                    City = Input.City,
                    UserName = Input.Email,
                    Email = Input.Email
                };

                var result = await userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    logger.LogInformation("User created a new account with password.");

                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await emailSender.SendEmailAsync(Input.Email, "Потвърдете регистация си!",
                        $"Натистене <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>тук</a> за да потвърдите регистрацията си..");

                    if (userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    }
                    else
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
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
