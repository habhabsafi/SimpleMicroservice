using AutoMapper;
using SubjectService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectService.Database.Repositories
{
    public class SubjectRepository : Repository<Entities.Subject,Subject>, ISubjectRepository
    {
        public SubjectRepository(DatabaseContext context, IMapper _mapper) : base(context, _mapper)
        {
            
        }

        public  Subject GetByName(string title)
        {
            var dbEntity = context.Set<Entities.Subject>().FirstOrDefault(x => x.Title == title);
            return _mapper.Map<Subject>(dbEntity);
        }
    }
}
