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



        public string FirstName { get; set; }
        public string LastName { get; set; }




    }
}
