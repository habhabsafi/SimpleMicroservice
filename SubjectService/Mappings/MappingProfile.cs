using AutoMapper;
using SubjectService.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectService.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Subject, Models.Subject>();
            CreateMap<Models.Subject, Subject>();
        }
    }
}
