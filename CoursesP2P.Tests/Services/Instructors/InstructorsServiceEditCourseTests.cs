using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Instructors;
using CoursesP2P.Tests.Configuration;
using CoursesP2P.ViewModels.Courses.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CoursesP2P.Tests.Services.Instructors
{
    public class InstructorsServiceEditCourseTests
    {
        private CoursesP2PDbContext db;
        private InstructorsService instructorsService;

        public InstructorsServiceEditCourseTests()
        {
            this.db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            this.instructorsService = new InstructorsService(db, null);
        }

        [Fact]
        public async Task EditCourseReturnCorrectly()
        {
            var course = new Course { Id = 1, Name = "Pesho" };
            await this.db.Courses.AddAsync(course);
            await this.db.SaveChangesAsync();

            var model = new CourseEditViewModel
            {
                Id = 1,
                Name = "newName"
            };

            await this.instructorsService.EditCourseAsync(model, null);

            var newCourse = this.db.Courses.FirstOrDefault(x => x.Id == 1);
            var isSetNewName = newCourse.Name == "newName";

            Assert.True(isSetNewName);
        }

        [Fact]
        public async Task EditCourseWithInvalidIdReturnException()
        {
            var course = new Course { Id = 1, Name = "Pesho" };
            await this.db.Courses.AddAsync(course);
            await this.db.SaveChangesAsync();

            var model = new CourseEditViewModel
            {
                Id = 2,
                Name = "newName"
            };

            await Assert.ThrowsAsync<ArgumentNullException>(() => this.instructorsService.EditCourseAsync(model, null));
        }
    }
}
