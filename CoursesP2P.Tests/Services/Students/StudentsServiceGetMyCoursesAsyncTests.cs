using AutoMapper;
using CoursesP2P.Data;
using CoursesP2P.Models;
using CoursesP2P.Services.Students;
using CoursesP2P.Tests.Configuration;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoursesP2P.Tests.Services.Students
{
    public class StudentsServiceGetMyCoursesAsyncTests
    {
        private CoursesP2PDbContext db;
        private readonly IMapper mapper;
        private UserManager<User> userManager;
        private StudentsService studentsService;

        public StudentsServiceGetMyCoursesAsyncTests()
        {
            this.db = new CoursesP2PDbContext(MemoryDatabase.OptionBuilder());
            this.mapper = MapperMock.AutoMapperMock();
            this.userManager = UserManagerMock.UserManagerMockTest();
            this.studentsService = new StudentsService(this.db, this.mapper, this.userManager);
        }
    }
}
