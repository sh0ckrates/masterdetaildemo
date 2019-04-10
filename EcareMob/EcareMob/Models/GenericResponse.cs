using System;
using System.Collections.Generic;
using System.Text;

namespace EcareMob.Models
{
    public class GenericResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public int StatusCode { get; set; }
    }
}
