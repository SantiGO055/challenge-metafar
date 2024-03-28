using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public class ServiceResult<T>
    {
        public bool IsError { get; set; }
        public string? Message { get; set; }
        public T? Payload { get; set; }
    }
}
