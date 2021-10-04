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
        public ClientResponseModel<Subject> Get(int id)
        {
            ClientResponseModel<Subject> responseModel = new ClientResponseModel<Subject>
            {
                Record = _subjectService.GetSubjectById(id)
            };
            return responseModel;
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _subjectService.DeleteSubject(id);
        }
        [HttpGet]
        public ClientResponseModel<IEnumerable<Subject>> Get()
        {
            ClientResponseModel<IEnumerable<Subject>> responseModel = new ClientResponseModel<IEnumerable<Subject>>
            {
                Record = _subjectService.GetAllSubjects()
            };
            return responseModel;
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
        [HttpPost, Route("GetAllFiltered")]
        public ClientResponseModel<IEnumerable<Subject>> GetAllFiltered([FromBody] SubjectFilter subjectFilter)
        {

            ClientResponseModel<IEnumerable<Subject>> model = new ClientResponseModel<IEnumerable<Subject>>
            {
                Record = _subjectService.GetAllFiltered(subjectFilter, out int totalCount),
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
