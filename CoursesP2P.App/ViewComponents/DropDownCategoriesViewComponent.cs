using CoursesP2P.Models.Enum;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesP2P.App.ViewComponents
{
    public class DropDownCategoriesViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var categories = new Dictionary<string, string>();

            foreach (var category in Enum.GetNames(typeof(Category)))
            {
                switch (category)
                {
                    case "Development": categories.Add(category, "fas fa-laptop-code"); break;
                    case "Marketing": categories.Add(category, "fas fa-bullhorn"); break;

                }
            }

            return View(categories);
        }
    }
}
