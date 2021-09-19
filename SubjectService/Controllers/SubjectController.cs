using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SubjectService.Models;
using SubjectService.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectMethods _subjectService;
        public SubjectController(ISubjectMethods subjectService)
        {
            _subjectService = subjectService;
        }
        [HttpGet("{id}")]
        public Subject Get(int id)
        {
            return _subjectService.GetSubjectById(id);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _subjectService.DeleteSubject(id);
        }
        [HttpGet]
        public IEnumerable<Subject> Get()
        {
            return _subjectService.GetAllSubjects();
        }
        [HttpGet("{page},{countPerPage}")]
        public IEnumerable<Subject> Get(int page , int countPerPage)
        {
            return _subjectService.GetAllPaged(page,countPerPage,out int totalCount);

        }
        [HttpPut]
        public void Put(Subject subject)
        {
             _subjectService.EditSubject(subject);
        }
        [HttpPost]
        public void Post (Subject subject)
        {
            _subjectService.AddSubject(subject);
        }
    }
}
