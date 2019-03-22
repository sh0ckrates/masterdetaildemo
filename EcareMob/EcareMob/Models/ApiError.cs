using System;
using System.Collections.Generic;
using System.Text;

namespace EcareMob.Models
{
    public class ApiError
    {
        public string Message { get; set; }
        public bool isError { get; set; }
        public string detail { get; set; }
        //public ValidationErrorCollection errors { get; set; }

        public ApiError(string message)
        {
            this.Message = message;
            isError = true;
        }
    }
}
