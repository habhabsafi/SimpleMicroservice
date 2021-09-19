using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectService.Models
{
    public class ClientException : Exception
    {
        public ClientException()
        {

        }
        public ClientException(string message)
            : base(message)
        {

        }
    }
}
