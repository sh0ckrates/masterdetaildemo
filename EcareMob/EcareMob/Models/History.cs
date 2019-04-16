using System;
using System.Collections.Generic;
using System.Text;

namespace EcareMob.Models
{
    public class History 
    {
        public DateTime? DateApplied { get; set; }
        public string ContractNo { get; set; }
        public string Description { get; set; }
        public string TicketNo { get; set; }
    }
}
