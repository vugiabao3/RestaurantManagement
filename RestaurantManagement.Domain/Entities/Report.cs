using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Domain.Entities
{
    public class Report
    {
        [Key]
        public Guid ReportId { get; set; }

        public Guid BranchId { get; set; }

        public Guid AdminId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public string ReportType { get; set; }
            = string.Empty;
    }
}