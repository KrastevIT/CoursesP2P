using AutoMapper;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Instructors;
using CoursesP2P.Tests.Configuration;
using CoursesP2P.ViewModels.Courses.ViewModels;
using System;
using System.Linq;
using Xunit;

namespace CoursesP2P.Tests.Services.Instructors
{
    public class InstructorsServiceEditCourseTests
    {
        private CoursesP2PDbContext db;
        private IMapper mapper;
        private InstructorsService instructorsService;

        public InstructorsServiceEditCourseTests()
        {
            this.db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            this.mapper = MapperMock.AutoMapperMock();
            this.instructorsService = new InstructorsService(db, mapper);
        }

        [Fact]
        public void EditCourseReturnCorrectly()
        {
            var course = new Course { Id = 1, Name = "Pesho" };
            this.db.Courses.Add(course);
            this.db.SaveChanges();

            var model = new CourseEditViewModel
            {
                Id = 1,
                Name = "newName"
            };

            this.instructorsService.EditCourse(model);

            var newCourse = this.db.Courses.FirstOrDefault(x => x.Id == 1);
            var isSetNewName = newCourse.Name == "newName";

            Assert.True(isSetNewName);
        }

        [Fact]
        public void EditCourseWithInvalidIdReturnException()
        {
            var course = new Course { Id = 1, Name = "Pesho" };
            this.db.Courses.Add(course);
            this.db.SaveChanges();

            var model = new CourseEditViewModel
            {
                Id = 2,
                Name = "newName"
            };

            Assert.Throws<ArgumentNullException>(() => this.instructorsService.EditCourse(model));
        }
    }
}
