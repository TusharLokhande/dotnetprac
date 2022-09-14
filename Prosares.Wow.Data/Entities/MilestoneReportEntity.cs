using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prosares.Wow.Data.Entities
{
    


    public class MilestoneReportEntity : BaseEntity
    {
        public string MileStone { get; set; }
        public string Engagement { get; set; }
        public long EngagementId { get; set; }

        public float POValue { get; set; }

        public long TotalCount { get; set; }

        public decimal Amount { get; set; }
        [Column(TypeName = "date")]
        public DateTime PlannedDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime RevisedDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CompletedDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime InvoicedDate { get; set; }
        public long MandaysBalance { get; set; }
        public string Customer { get; set; }
        public string EngagementType { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public int pageSize { get; set; }
        public int start { get; set; }
        public string SearchText { get; set; }

        

    }
}
