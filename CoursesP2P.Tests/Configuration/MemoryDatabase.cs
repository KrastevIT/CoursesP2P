using CoursesP2P.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace CoursesP2P.Tests.Configuration
{
    public static class MemoryDatabase
    {
        public static DbContextOptions<CoursesP2PDbContext> OptionBuilder()
        {
            return new DbContextOptionsBuilder<CoursesP2PDbContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        }
    }
}
