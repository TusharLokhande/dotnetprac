﻿using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
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

namespace Prosares.Wow.Data.Services.Milestone
{
    public class MilestoneService : IMilestoneService
    {
        #region Prop
        private readonly IRepository<MileStone> _milestone;
        private readonly ILogger<MilestoneService> _logger;
        #endregion

        #region Constructor
        public MilestoneService(IRepository<MileStone> milestone,
                        ILogger<MilestoneService> logger)
        {
            _milestone = milestone;
            _logger = logger;
        }
        #endregion

        #region Methods
        public MilestoneResponse GetMilestoneData(MileStone value)
        {
            var data = new MilestoneResponse();

            Expression<Func<Entities.MileStone, bool>> InitialCondition;
            Expression<Func<Entities.MileStone, bool>> SearchText;
            Expression<Func<Entities.MileStone, bool>> DateFilter;


            InitialCondition = k => k.Id != 0;

            if (value.searchText != null)
            {

                // SearchText = k => k.Engagement.Engagement.Contains(value.searchText);
                SearchText = k => k.MileStones.Contains(value.searchText);

            }
            else
            {
                // SearchText = k => k.Engagement.Engagement != "";
                SearchText = k => k.MileStones != "";
            }

            DateFilter = k => k.RevisedDate >= value.fromDate && k.RevisedDate <= value.toDate;

            if (value.sortColumn == "" || value.sortDirection == "")
            {

                data.count = _milestone.GetAll(b => b.Where(InitialCondition).Where(DateFilter).Where(SearchText)).ToList().Count();
                data.milestoneData = _milestone.GetAll(b => b.Where(InitialCondition).Where(DateFilter).Where(SearchText).OrderByPropertyDescending("createdDate")).Skip(value.start).Take(value.pageSize).ToList();
            }
            else if (value.sortDirection == "desc")
            {
                data.count = _milestone.GetAll(b => b.Where(InitialCondition).Where(DateFilter).Where(SearchText)).ToList().Count();
                data.milestoneData = _milestone.GetAll(b => b.Where(InitialCondition).Where(DateFilter).Where(SearchText).OrderByPropertyDescending(value.sortColumn)).Skip(value.start).Take(value.pageSize).ToList();
            }
            else if (value.sortDirection == "asc")
            {
                data.count = _milestone.GetAll(b => b.Where(InitialCondition).Where(DateFilter).Where(SearchText)).ToList().Count();
                data.milestoneData = _milestone.GetAll(b => b.Where(InitialCondition).Where(DateFilter).Where(SearchText).OrderByProperty(value.sortColumn)).Skip(value.start).Take(value.pageSize).ToList();
            }

            return data;
        }

        public bool CheckIfMilestoneExists(string MileStone)
        {
            var data = _milestone.GetAll(b => b.Where(k => k.IsActive == true && k.MileStones == MileStone)).ToList();
            if (data.Count > 0)
            {
                return true;
            }
            return false;
        }

        public MileStone GetMilestoneById(MileStone value)
        {
            var milestone = _milestone.GetById(value.Id);

            return milestone;
        }

        public void InsertUpdateMilestoneData(MileStone value)
        {
            try
            {
                if (value.Id == 0)
                {
                    _milestone.Insert(value);
                }
                else
                {
                    _milestone.Update(value);
                }
            }
            catch (Exception ex)
            {
                throw;
            }


        }

        public List<MileStone> MilestoneExportToExcel(string SearchText, string sortColumn, string sortDirection)
        {
            SqlCommand command = new SqlCommand("stpMileStoneForExportToExcel");
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add("@searchText", SqlDbType.VarChar).Value = SearchText;
            command.Parameters.Add("@sortColumn", SqlDbType.VarChar).Value = sortColumn;
            command.Parameters.Add("@sortDirection", SqlDbType.VarChar).Value = sortDirection;
            var data = _milestone.GetRecords(command).ToList();
            return data;
        }
        #endregion
        public class MilestoneResponse
        {
            public int count { get; set; }
            public List<Entities.MileStone> milestoneData { get; set; }
        }
    }
}
