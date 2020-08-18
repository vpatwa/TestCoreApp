using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestCoreApp.Domain.Entities
{
    [Table("RateCharts")]
    public class RateChart
    {
        [Key]
        public int Id { get; set; }
        [ExplicitKey]
        public int CoveragePlanId { get; set; }
        public string CustomerGender { get; set; }
        public string CustomerAge { get; set; }
        public decimal NetPrice { get; set; }
        public virtual IList<CoveragePlan> CoveragePlans { get; set; }
    }
}
