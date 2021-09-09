using AutoMapper;
using SubjectService.Database.Repositories;
using SubjectService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectService.Methods
{
    public class SubjectMethods: ISubjectMethods
    {
        private readonly ISubjectRepository _subjectRepository;
        public SubjectMethods(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }
  
        public IEnumerable<Subject> GetAllSubjects() => _subjectRepository.GetAll();
        public Subject GetSubjectById(int subjectId) => _subjectRepository.GetById(subjectId);
        public void AddSubject( Subject subject) => _subjectRepository.Add(subject);
        public void DeleteSubject(int subjectId) => _subjectRepository.Delete(subjectId);
        public void EditSubject(Subject subject) => _subjectRepository.Update(subject);
        public IEnumerable<Subject> GetAllPaged(int page, int counterPerPage, out int totalCount)
        {
           return _subjectRepository.GetAll(page, counterPerPage,out totalCount);
        }
    }
}
