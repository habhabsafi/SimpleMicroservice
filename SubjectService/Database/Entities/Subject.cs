using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectService.Database.Entities
{
    public class Subject:BaseEntity
    {
        [MaxLength(255)]
        public string Title { get; set; }
        [MaxLength(255)]
        public string Subtitle { get; set; }
        public string Content { get; set; }
        [MaxLength(10)]
        public string ImageExtension { get; set; }
        public bool IsVisible { get; set; }
        public int Type { get; set; }
    }
}
