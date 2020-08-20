using System;
using System.Collections.Generic;
using System.Text;

namespace TestCoreApp.Domain.Contracts.Response
{
    public class ContractDetail
    {
        public int ContractId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerCountry { get; set; }
        public string CustomerDOB { get; set; }
        public string SaleDate { get; set; }
        public string CoveragePlan { get; set; }
        public decimal NetPrice { get; set; }
    }
}
