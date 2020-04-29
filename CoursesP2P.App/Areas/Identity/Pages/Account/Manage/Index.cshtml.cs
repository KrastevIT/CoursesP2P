using Courses.P2P.Common;
using CoursesP2P.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CoursesP2P.App.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        public string City { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = ErrorMessages.Required)]
            [MinLength(2, ErrorMessage = ErrorMessages.FirstNameLength)]
            [MaxLength(50, ErrorMessage = ErrorMessages.FirstNameLength)]
            public string FirstName { get; set; }

            [Required(ErrorMessage = ErrorMessages.Required)]
            [MinLength(2, ErrorMessage = ErrorMessages.LastNameLength)]
            [MaxLength(50, ErrorMessage = ErrorMessages.LastNameLength)]
            public string LastName { get; set; }

            [Required]
            [DataType(DataType.Date)]
            public DateTime Birthday { get; set; }

            [Required(ErrorMessage = ErrorMessages.Required)]
            [MinLength(2, ErrorMessage = ErrorMessages.CityLength)]
            [MaxLength(50, ErrorMessage = ErrorMessages.CityLength)]
            public string City { get; set; }
        }

        private void LoadAsync(User user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Birthday = user.Birthday;
            City = user.City;

            Input = new InputModel
            {
                FirstName = FirstName,
                LastName = LastName,
                Birthday = Birthday,
                City = City
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                LoadAsync(user);
                return Page();
            }

            user.FirstName = Input.FirstName;
            user.LastName = Input.LastName;
            user.Birthday = Input.Birthday;
            user.City = Input.City;

            await userManager.UpdateAsync(user);

            await signInManager.RefreshSignInAsync(user);
            StatusMessage = string.Format(SuccessfulMessages.SuccessfulUpdateProfile);
            return RedirectToPage();
        }
    }
}
