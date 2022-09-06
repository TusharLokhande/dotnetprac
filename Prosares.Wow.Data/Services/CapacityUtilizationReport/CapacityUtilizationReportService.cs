using Microsoft.Data.SqlClient;
using Prosares.Wow.Data.Entities;
using Prosares.Wow.Data.Helpers;
using Prosares.Wow.Data.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Prosares.Wow.Data.Services.CapacityUtilizationReport
{
    public class CapacityUtilizationReportService : ICapacityUtilizationReport
    {
        private readonly IRepository<Entities.CapacityUtilizationReport> _capacity;
        private readonly IRepository<Entities.Customer> _Customer;
        private readonly IRepository<Entities.EngagementTypeOption> _EngageTypeOption;
        private readonly IRepository<Entities.EngagementMaster> _EngagementMaster;

        public CapacityUtilizationReportService(IRepository<Entities.EngagementMaster> EngagementMaster, IRepository<Entities.EngagementTypeOption> EngageTypeOption,IRepository<Entities.CapacityUtilizationReport> capcity, IRepository<Entities.Customer> customer)
        {
            _capacity = capcity;
            _Customer = customer;
            _EngageTypeOption = EngageTypeOption;
            _EngagementMaster = EngagementMaster;
            
        }

        public dynamic GetCapacityAllocation(Entities.CapacityUtilizationReport value)
        {
            Expression<Func<Entities.CapacityUtilizationReport, bool>> InitialCondition;
            Expression<Func<Entities.CapacityUtilizationReport, bool>> SearchText;

            

            SqlCommand command = new SqlCommand("stp_capacityallocation");
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@dropdown", SqlDbType.VarChar).Value = value.MasterType;
            command.Parameters.Add("@customer", SqlDbType.NVarChar).Value = value.Customers;
            command.Parameters.Add("@EngagementType", SqlDbType.NVarChar).Value = value.EngagementTypes;
            command.Parameters.Add("@Engagement", SqlDbType.NVarChar).Value = value.Engagements;
            command.Parameters.Add("@fromdate", SqlDbType.Date).Value = value.FromDate;
            command.Parameters.Add("@todate", SqlDbType.Date).Value = value.ToDate;

            var data = _capacity.GetRecords(command);
          
            //if(value.MasterType == "Resource")
            //{
            //    if (value.searchText != null)
            //    {
                       
            //           data  = data.Where(k => k.Resource.Contains(value.searchText) || k.EngagementType.Contains(value.searchText));
            //    }
            //    else
            //    {
            //        data = data.Where(k => k.Resource.Contains(""));
            //    }


            //    if (value.sortColumn == "" || value.sortDirection == "")
            //    {
            //      data  = data.OrderBy(x => x.Resource);
            //    }
            //    else if (value.sortDirection == "desc")
            //    {
            //        data = data.
            //    }
            //}

            




            return data;
        }




        public dynamic Getallcustomers()
        {
            var data = from x in _Customer.Table select new { Id = x.Id, Name = x.Name };
            return data;
        }

        public dynamic Getallengagementtype()
        {
            var data = from x in _EngageTypeOption.Table select new { Id = x.EngagementTypeId, Name = x.EngagementType };
            return data;
        }

        public dynamic Getallengagement()
        {
            var data = from x in _EngagementMaster.Table select new { Id = x.Id, Name = x.Engagement };
            return data;
        }
    }
}
