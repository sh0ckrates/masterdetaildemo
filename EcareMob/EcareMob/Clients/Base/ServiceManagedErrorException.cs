using System;

namespace EcareMob.Clients.Base
{
    public class ServiceManagedErrorException : Exception
    {

        public ServiceManagedErrorException(string message):base(message)
        {
        }
       
    }
}
