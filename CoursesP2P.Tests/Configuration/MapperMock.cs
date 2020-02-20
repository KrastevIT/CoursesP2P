using AutoMapper;
using CoursesP2P.App.Mapping;

namespace CoursesP2P.Tests.Configuration
{
    public static class MapperMock
    {
        public static IMapper AutoMapperMock()
        {
            return new MapperConfiguration(
                x => x.AddProfile(new AutoMapperProfile())).CreateMapper();
        }
    }
}
