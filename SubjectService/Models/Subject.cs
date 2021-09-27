using SubjectService.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectService.Models
{
    public class Subject:BaseEntity
    {
        public const string DirectoryPath = "/content/uploads/subjects/images";
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Content { get; set; }
        public bool IsVisible { get; set; }
        public int Type { get; set; }
        public string ImageExtension { get; set; }
        public virtual string ImagePath
        {
            get
            {
                if (ImageExtension == null)
                {
                    return null;
                }

                return $"{DirectoryPath}/{Id}.{ImageExtension}";
            }
            set
            {

            }
        }
    }
    public class AddSubjectModel : Subject
    {
        public string ImageData { get; set; }
    }
    public class EditSubjectModel : Subject
    {
        public string ImageData { get; set; }
        public override string ImagePath { get; set; }
    }
}
