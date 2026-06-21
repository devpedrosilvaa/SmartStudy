using System;
using System.Collections.Generic;
using System.Text;

namespace SmartStudy.Domain.Common.Errors
{
    public class DomainError
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public DomainError(string code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
