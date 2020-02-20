using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Models.Enum;
using CoursesP2P.Services.Courses;
using CoursesP2P.Tests.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CoursesP2P.Tests
{
    public class CoursesServiceTests
    {
        [Fact]
        public void GetAllCoursesReturnAllCourses()
        {
            var db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            var mapper = MapperMock.AutoMapperMock();
            var userManager = UserManagerMock.UserManagerMockTest();

            var coursesService = new CoursesService(db, mapper, userManager);

            var course = new List<Course>
            {
                new Course
                {
                    Id = 1
                },
                new Course
                {
                    Id = 2
                }
            };

            db.Courses.AddRange(course);
            db.SaveChanges();

            var getCourses = coursesService.GetAllCourses().ToList();

            Assert.Equal(2, getCourses.Count);
        }

        [Fact]
        public void GetAllCoursesReturnAllCoursesWithAllLectures()
        {
            var db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            var mapper = MapperMock.AutoMapperMock();
            var userManager = UserManagerMock.UserManagerMockTest();

            var coursesService = new CoursesService(db, mapper, userManager);

            var lectures = new List<Lecture>
            {
                new Lecture
                {
                    Id = 1
                },
                new Lecture
                {
                    Id= 2
                },
                new Lecture
                {
                    Id= 3
                }
            };

            var course = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Lectures = lectures
                },
                new Course
                {
                    Id = 2
                }
            };

            db.Courses.AddRange(course);
            db.SaveChanges();

            var getCourses = coursesService.GetAllCourses()
                .Select(x => x.Lectures.Count).Sum();

            Assert.Equal(3, getCourses);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void GetCourseByIdRetunrCourseById(int id, int expected)
        {
            var db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            var mapper = MapperMock.AutoMapperMock();
            var userManager = UserManagerMock.UserManagerMockTest();

            var coursesService = new CoursesService(db, mapper, userManager);

            var courses = new List<Course>
            {
                new Course
                {
                    Id = 1
                },

                new Course
                {
                    Id = 2
                }
            };

            db.Courses.AddRange(courses);
            db.SaveChanges();

            var getCourse = coursesService.GetCourseById(id);

            Assert.Equal(expected, getCourse.Id);
        }

        [Theory]
        [InlineData(3)]
        public void GetCourseByIdWithNonExistentCourseIdReturnException(int id)
        {
            var db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            var mapper = MapperMock.AutoMapperMock();
            var userManager = UserManagerMock.UserManagerMockTest();

            var coursesService = new CoursesService(db, mapper, userManager);

            var courses = new List<Course>
            {
                new Course
                {
                    Id = 1
                },

                new Course
                {
                    Id = 2
                }
            };

            db.Courses.AddRange(courses);
            db.SaveChanges();

            Assert.Throws<ArgumentNullException>(() =>
            {
                coursesService.GetCourseById(id);
            });
        }

        [Theory]
        [InlineData("Development", 2)]
        [InlineData("Marketing", 1)]
        public void GetCoursesByCategoryReturnCourseByCategory(string categoryName, int expected)
        {
            var db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            var mapper = MapperMock.AutoMapperMock();
            var userManager = UserManagerMock.UserManagerMockTest();

            var coursesService = new CoursesService(db, mapper, userManager);

            var categoryDevelopment = (Category)Enum.Parse(typeof(Category), "Development");
            var categoryMarkiting = (Category)Enum.Parse(typeof(Category), "Marketing");

            var courses = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Category = categoryDevelopment
                },
                new Course
                {
                    Id = 2,
                    Category = categoryDevelopment
                },
                new Course
                {
                    Id = 3,
                    Category = categoryMarkiting
                },
            };

            db.Courses.AddRange(courses);
            db.SaveChanges();

            var getCourses = coursesService.GetCoursesByCategory(categoryName);

            Assert.Equal(expected, getCourses.Count());
        }

        [Theory]
        [InlineData("InvalidCategory")]
        public void GetCoursesByCategoryWithNonExistentCategoryReturnException(string categoryName)
        {
            var db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            var mapper = MapperMock.AutoMapperMock();
            var userManager = UserManagerMock.UserManagerMockTest();

            var coursesService = new CoursesService(db, mapper, userManager);

            var categoryDevelopment = (Category)Enum.Parse(typeof(Category), "Development");
            var categoryMarkiting = (Category)Enum.Parse(typeof(Category), "Marketing");

            var courses = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Category = categoryDevelopment
                },
                new Course
                {
                    Id = 2,
                    Category = categoryDevelopment
                },
                new Course
                {
                    Id = 3,
                    Category = categoryMarkiting
                },
            };

            db.Courses.AddRange(courses);
            db.SaveChanges();


            Assert.Throws<InvalidCastException>(() => coursesService.GetCoursesByCategory(categoryName));
        }

        [Theory]
        [InlineData("Development", 2)]
        public void GetCoursesByCategoryReturnCoursesWithLectures(string categoryName, int expected)
        {
            var db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            var mapper = MapperMock.AutoMapperMock();
            var userManager = UserManagerMock.UserManagerMockTest();

            var coursesService = new CoursesService(db, mapper, userManager);

            var categoryDevelopment = (Category)Enum.Parse(typeof(Category), "Development");
            var categoryMarkiting = (Category)Enum.Parse(typeof(Category), "Marketing");

            var lectures = new List<Lecture>
            {
                new Lecture
                {
                    Id = 1,
                },
                 new Lecture
                {
                    Id = 2,
                }
            };

            var courses = new List<Course>
            {
                new Course
                {
                    Id = 1,
                    Category = categoryDevelopment,
                    Lectures = lectures
                },
                new Course
                {
                    Id = 2,
                    Category = categoryDevelopment
                },
                new Course
                {
                    Id = 3,
                    Category = categoryMarkiting
                },
            };

            db.Courses.AddRange(courses);
            db.SaveChanges();

            var getLecture = coursesService.GetCoursesByCategory(categoryName)
                .Select(x => x.Lectures.Count)
                .Sum();

            Assert.Equal(expected, getLecture);
        }
    }
}
