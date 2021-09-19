using SubjectService.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectService.Models
{
    public class Subject:BaseEntity
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Content { get; set; }
        public bool IsVisible { get; set; }
        public int Type { get; set; }
    }
}
