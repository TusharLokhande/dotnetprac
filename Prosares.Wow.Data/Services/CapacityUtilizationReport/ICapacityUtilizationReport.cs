using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prosares.Wow.Data.Services.CapacityUtilizationReport
{
    public interface ICapacityUtilizationReport
    {
        public dynamic GetCapacityAllocation(Entities.CapacityUtilizationReport value);

        public dynamic Getallcustomers();

        public dynamic Getallengagementtype();

        public dynamic Getallengagement();
    }
}
