using System;
using System.Collections.Generic;
using System.Text;

namespace EcareMob.Models
{
    public class UserProfile
    {
        public string FullName { get; set; }
        public string HomeAddress { get; set; }
        public string MailAddress { get; set; }
               
        public string PhoneNumber { get; set; }
               
        public string MobileNumber { get; set; }
               
        public string Email { get; set; }

        public bool IsPrivatePerson { get; set; }

    }
}
