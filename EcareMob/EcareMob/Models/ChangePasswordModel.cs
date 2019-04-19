using System;
using System.Collections.Generic;
using System.Text;

namespace EcareMob.Models
{
    public class ChangePasswordModel
    {
        public int UserId { get; set; }
        public string NewPassword { get; set; }
        public string PreviousPassword { get; set; }
    }
}
