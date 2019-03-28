using System;
using System.Collections.Generic;
using System.Text;

namespace EcareMob.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }

        //Taken
        public string Token { get; set; }
        public DateTime? ExpireTokenDate { get; set; }
        public string RefreshToken { get; set; }

        public int Id { get; set; }
        public bool IsApproved { get; set; }
        public bool AuthenticationError { get; set; }




        public string FullName { get; set; }
        public string Vat { get; set; }
        public string Email { get; set; }
        //public string PkId { get; set; }
        public string CharismaCode { get; set; }


    }
}
