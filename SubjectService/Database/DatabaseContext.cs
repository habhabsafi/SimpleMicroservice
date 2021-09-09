using Microsoft.EntityFrameworkCore;
using SubjectService.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectService.Database
{
    public class DatabaseContext:DbContext
    {
        public DbSet<Subject> Subjects { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options):base (options)
        {

        }
    }
}
