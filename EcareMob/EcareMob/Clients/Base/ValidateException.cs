using System;
using System.Collections.Generic;
using System.Text;

namespace EcareMob.Clients.Base
{ 
    public class ValidateException:Exception
    {
        public ValidateException(string message):base(message)
        {
            
        }
    }
}
