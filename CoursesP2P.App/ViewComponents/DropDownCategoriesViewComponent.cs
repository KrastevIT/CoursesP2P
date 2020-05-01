using CoursesP2P.Models.Enum;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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
                    case "Програмиране": categories.Add(category, "fas fa-laptop-code"); break;
                    case "Маркетинг": categories.Add(category, "fas fa-bullhorn"); break;
                    case "Бизнес": categories.Add(category, "far fa-chart-bar"); break;
                    case "ИТ_и_Софтуер": categories.Add(category.Replace("_", " "), "fas fa-desktop"); break;
                    case "Личностно_Развитие": categories.Add(category.Replace("_", " "), "fas fa-book-reader"); break;
                    case "Дизайн": categories.Add(category, "fas fa-pencil-alt"); break;
                    case "Начин_на_живот": categories.Add(category.Replace("_", " "), "fas fa-dove"); break;
                    case "Фотография": categories.Add(category, "fas fa-camera-retro"); break;
                    case "Здраве_и_фитнес": categories.Add(category.Replace("_", " "), "fas fa-heartbeat"); break;
                    case "Музика": categories.Add(category, "fab fa-itunes-note"); break;
                    case "Езици": categories.Add(category, "fas fa-language"); break;
                }
            }

            return View(categories);
        }
    }
}
