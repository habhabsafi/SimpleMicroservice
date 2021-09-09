using SubjectService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectService.Methods
{
    public interface ISubjectMethods 
    {
        IEnumerable<Subject> GetAllSubjects();
        IEnumerable<Subject> GetAllPaged(int page, int counterPerPage, out int totalCount );
        Subject GetSubjectById(int subjectId);
        void AddSubject(Subject subject);
        void DeleteSubject(int subjectId); 
        void EditSubject(Subject subject); 
    }
}
