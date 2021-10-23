using AutoMapper;
using SubjectService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectService.Database.Repositories
{
    public class SubjectRepository : Repository<Entities.Subject, Subject>, ISubjectRepository
    {
        public SubjectRepository(DatabaseContext context, IMapper _mapper) : base(context, _mapper)
        {

        }
        public List<Subject> GetAllFiltered(SubjectFilter subjectFilter, out int totalCount)
        {
            var toFilter = Filter();

            if (subjectFilter.Type.HasValue)
                toFilter = toFilter.Where(c => c.Type == subjectFilter.Type);

            if (!string.IsNullOrEmpty(subjectFilter.KeyWord))
                toFilter = toFilter.Where(c => c.Title.Contains(subjectFilter.KeyWord)
                                            || c.Subtitle.Contains(subjectFilter.KeyWord));

            switch (subjectFilter.SubjectSortBy)
            {
                case (int)SubjectSortProps.Title:
                    toFilter = subjectFilter.SortDesc?  toFilter.OrderByDescending(c => c.Title) :
                                                        toFilter.OrderBy(c => c.Title);
                    break;

                case (int)SubjectSortProps.Subtitle:
                    toFilter = subjectFilter.SortDesc ? toFilter.OrderByDescending(c => c.Subtitle) :
                                                      toFilter.OrderBy(c => c.Subtitle);
                    break;
                default:
                    toFilter = toFilter.OrderByDescending(c => c.Id);
                    break;
            }

            totalCount = toFilter.Count();

            return toFilter .Skip(subjectFilter.Page * subjectFilter.CountPerPage)
                            .Take(subjectFilter.CountPerPage)
                            .Select(_mapper.Map<Subject>)
                            .ToList();
        }
        public Subject GetByName(string title)
        {
            var dbEntity = context.Set<Entities.Subject>().FirstOrDefault(x => x.Title == title);
            return _mapper.Map<Subject>(dbEntity);
        }
    }
}
