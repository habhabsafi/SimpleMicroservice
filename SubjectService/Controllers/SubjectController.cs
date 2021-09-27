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

        [HttpGet, Route("GetAll")]
        public ClientResponseModel<IEnumerable<Subject>> GetAll(int page, int countPerPage)
        {
            ClientResponseModel<IEnumerable<Subject>> model = new ClientResponseModel<IEnumerable<Subject>>
            {
                Record = _subjectService.GetAllPaged(page, countPerPage, out int totalCount),
                ExtraData = totalCount
            };
            return model;
        }
        [HttpPut]
        public ClientResponseModel<Subject> Put(EditSubjectModel subject)
        {
            ClientResponseModel<Subject> responseModel = new ClientResponseModel<Subject>
            {
                Record = _subjectService.EditSubject(subject)
        };
            return responseModel;
        }
        [HttpPost]
        public ClientResponseModel<Subject> Post(AddSubjectModel subject)
        {
            ClientResponseModel<Subject> responseModel = new ClientResponseModel<Subject>
            {
                Record = _subjectService.AddSubject(subject)
            };
            return responseModel;
        }
    }
}
