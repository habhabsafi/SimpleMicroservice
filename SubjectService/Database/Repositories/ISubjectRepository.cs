using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SubjectService.Models;
namespace SubjectService.Database.Repositories
{
    public interface ISubjectRepository : IRepository<Entities.Subject, Subject>
    {
        Subject GetByName(string title);
    }
}
