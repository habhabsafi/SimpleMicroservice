using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Saderlexstore.Utility;
using SubjectService.Database.Repositories;
using SubjectService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectService.Methods
{
    public class SubjectMethods : ISubjectMethods
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IDataHelper _dataHelper;
        public SubjectMethods(ISubjectRepository subjectRepository, IWebHostEnvironment webHostEnvironment, IDataHelper dataHelper)
        {
            _subjectRepository = subjectRepository;
            _webHostEnvironment = webHostEnvironment;
            _dataHelper = dataHelper;
        }

        public IEnumerable<Subject> GetAllSubjects() => _subjectRepository.GetAll();
        public Subject GetSubjectById(int subjectId) => _subjectRepository.GetById(subjectId);
        public Subject AddSubject(AddSubjectModel subject)
        {
            Subject subjectToAdd = subject;
            if (!string.IsNullOrEmpty(subject.ImageData))
                subjectToAdd.ImageExtension = _dataHelper.GetBase64Extension(subject.ImageData);
            var addedSubject = _subjectRepository.Add(subjectToAdd);

            if (!string.IsNullOrEmpty(subject.ImageData))
            {
                string contentRootPath = _webHostEnvironment.ContentRootPath;
                //string contentRootPath = _webHostEnvironment.ContentRootPath;
                string subjectImagesDirectory = Path.Combine(contentRootPath, Subject.DirectoryPath.Replace("/", "\\").TrimStart('\\'));
                System.IO.DirectoryInfo dir = new DirectoryInfo(subjectImagesDirectory);
                if (!dir.Exists)
                    dir.Create();
                _dataHelper.UploadFile(subject.ImageData, Subject.DirectoryPath, addedSubject.Id.ToString(), null, out _);
            }
            return addedSubject;
        }
        public void DeleteSubject(int subjectId) => _subjectRepository.Delete(subjectId);
        public Subject EditSubject(EditSubjectModel subject)
        {
            Subject subjectToEdit = subject;
            string contentRootPath = _webHostEnvironment.ContentRootPath;

            if (!string.IsNullOrEmpty(subject.ImageData))
            {

                subjectToEdit.ImageExtension = _dataHelper.GetBase64Extension(subject.ImageData);
                string subjectImagesDirectory = Path.Combine(contentRootPath, Subject.DirectoryPath.Replace("/", "\\").TrimStart('\\'));
                System.IO.DirectoryInfo dir = new DirectoryInfo(subjectImagesDirectory);
                if (!dir.Exists)
                    dir.Create();
                _dataHelper.UploadFile(subject.ImageData, Subject.DirectoryPath, subjectToEdit.Id.ToString(), null, out _);
            }
            else if (string.IsNullOrEmpty(subjectToEdit.ImageExtension) && !string.IsNullOrEmpty(subject.ImagePath))
            {
                string subjectImagePath = Path.Combine(contentRootPath, "wwwroot\\" + subject.ImagePath.Replace("/", "\\").TrimStart('\\'));

                if (File.Exists(subjectImagePath))
                {
                    File.Delete(subjectImagePath);
                }

            }

            return _subjectRepository.Update(subject);

        }
        public IEnumerable<Subject> GetAllFiltered(SubjectFilter subjectFilter, out int totalCount)
        {
            return _subjectRepository.GetAllFiltered(subjectFilter, out totalCount);
        }
        public IEnumerable<Subject> GetAllPaged(int page, int counterPerPage, out int totalCount)
        {
            return _subjectRepository.GetAll(page, counterPerPage, out totalCount);
        }
    }
}
