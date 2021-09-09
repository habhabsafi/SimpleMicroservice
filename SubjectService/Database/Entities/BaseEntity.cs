using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectService.Database.Entities
{

    public class BaseEntity:IEntity
    {
        public int Id { get; set; }
    }
}
