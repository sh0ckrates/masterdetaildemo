using System;
using System.Collections.Generic;
using System.Text;

namespace EcareMob.Models
{
    public class Contract
    {
        public string ContractType { get; set; }
        public string ContractNo { get; set; }
        public string VehicleDescr { get; set; }
        public decimal FinancedValue { get; set; }
        public float YearlyInterest { get; set; }
        public int InstallmentNr { get; set; }
        public bool HasEotStatus { get; set; }
        public string EotMessage { get; set; }

        public bool IsRefinance { get; set; }
        public decimal FirstRate { get; set; }

        public bool IsGiveBack { get; set; }
        public bool IsPayOff { get; set; }
        public bool IsGiveBuy { get; set; }
        public bool IsBuyNew { get; set; }

        public DateTime? NextInstallmentDate { get; set; }
        public decimal OutstandingAmountSalesPrice { get; set; }



        public bool IsGuarantor { get; set; }
        public bool IsInsurance { get; set; }




        public string DictionaryItemName { get; set; }
        public decimal Ballon { get; set; }
    }
}
