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
        public List<Subject> GetAllFiltered ( SubjectFilter subjectFilter,out int totalCount)
        {
            var toFilter = Filter();
            if (subjectFilter.Type != 0)
            {
                toFilter = toFilter.Where(c => c.Type == subjectFilter.Type);
            }
            if (!string.IsNullOrEmpty(subjectFilter.SearchKey))
                toFilter = toFilter.Where(c => c.Title.Contains(subjectFilter.SearchKey) || c.Subtitle.Contains(subjectFilter.searchKey));
            totalCount = toFilter.Count();
            return toFilter.OrderByDescending(c => c.Id).Skip(subjectFilter.Page * subjectFilter.CountPerPage)
                .Take(subjectFilter.CountPerPage)
                .Select(_mapper.Map<Subject>)
                .ToList();
        }
        public  Subject GetByName(string title)
        {
            var dbEntity = context.Set<Entities.Subject>().FirstOrDefault(x => x.Title == title);
            return _mapper.Map<Subject>(dbEntity);
        }
    }
}
