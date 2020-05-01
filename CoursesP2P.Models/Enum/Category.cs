using System.ComponentModel.DataAnnotations;

namespace CoursesP2P.Models.Enum
{
    public enum Category
    {
        Програмиране = 0,

        Маркетинг = 1,

        Бизнес = 2,

        [Display(Name ="ИТ и Софтуер")]
        ИТ_и_Софтуер = 3,

        [Display(Name = "Личностно Развитие")]
        Личностно_Развитие = 4,

        Дизайн = 5,

        [Display(Name = "Начин на живот")]
        Начин_на_живот = 6,

        Фотография = 7,

        [Display(Name = "Здраве и фитнес")]
        Здраве_и_фитнес = 8,

        Музика = 9,

        Езици = 10
    }
}
