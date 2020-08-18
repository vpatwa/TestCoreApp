using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace TestCoreApp.Domain.Entities
{
    [Table("Contracts")]
    public class Contract
    {
        [Key]
        public int Id { get; set; }
        [ExplicitKey]
        public int RateChartId { get; set; }
        public string CustomerName { get; set; }
        public string CusomerAddress { get; set; }
        public string CustomerCountry { get; set; }
        public DateTime CustomerDOB { get; set; }
        public DateTime SaleDate { get; set; }
        public virtual IList<RateChart> RateCharts { get; set; }

    }
}
