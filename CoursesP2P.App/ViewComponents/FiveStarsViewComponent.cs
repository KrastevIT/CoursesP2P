using CoursesP2P.Data;
using CoursesP2P.ViewModels.FiveStars;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoursesP2P.App.ViewComponents
{
    public class FiveStarsViewComponent : ViewComponent
    {
        private const string STAR = "fa fa-star";
        private const string HALF_STAR = "fa fa-star-half-alt";
        private const string COLOR_STAR = "color:orange";

        private readonly CoursesP2PDbContext db;

        public FiveStarsViewComponent(CoursesP2PDbContext db)
        {
            this.db = db;
        }

        public IViewComponentResult Invoke(int courseId)
        {
            var stars = new List<FiveStarsViewModel>();

            var avrRatings = this.db.Ratings
                .Where(x => x.CourseId == courseId).Select(x => x.Vote)
                .ToList();

            var avr = avrRatings.Count > 0 ? avrRatings.Average() : 0;
            var avrRound = Math.Round(avr);

            for (int i = 1; i <= 5; i++)
            {
                var model = new FiveStarsViewModel();

                if (avr >= i)
                {
                    model.StarIcon = STAR;
                    model.Color = COLOR_STAR;
                }
                else if (avrRound > avr && i == avrRound)
                {
                    model.StarIcon = HALF_STAR;
                    model.Color = COLOR_STAR;
                }
                else
                {
                    model.StarIcon = STAR;
                }

                stars.Add(model);
            }


            return View(stars);
        }
    }
}
