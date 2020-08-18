using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestCoreApp.Domain.Entities
{
    [Table("CoveragePlans")]
    public class CoveragePlan
    {
        [Key]
        public int Id { get; set; }
        public string PlanName { get; set; }
        public DateTime EligibilityDateFrom { get; set; }
        public DateTime EligibilityDateTo { get; set; }
        public string EligibilityCountry { get; set; }
    }
}
