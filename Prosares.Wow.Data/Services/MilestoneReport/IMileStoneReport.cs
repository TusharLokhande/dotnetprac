using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prosares.Wow.Data.Services.MilestoneReport
{
    public interface IMileStoneReport
    {
        public dynamic MilestoneDashboardData(Entities.MilestoneReportEntity value);

        public dynamic GetDropedownCustomerNameList();

        IEnumerable<Models.MilestoneReportModel> GetDropedownEngagementTypeList();

    }
}
