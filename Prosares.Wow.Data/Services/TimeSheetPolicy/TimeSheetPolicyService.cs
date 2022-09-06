using Prosares.Wow.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prosares.Wow.Data.Entities;

namespace Prosares.Wow.Data.Services.TimeSheetPolicy
{
    public  class TimeSheetPolicyService : ITimeSheetPolicy
    {

        #region Fields
        private readonly IRepository<TimesheetPolicy> _timeSheet;
        #endregion



        #region Constructor
        public TimeSheetPolicyService(IRepository<TimesheetPolicy> timeSheet)
        {
            _timeSheet = timeSheet;
        }

        #endregion


        #region Methods
        public dynamic GetTimeSheetPolicy()
        {
            var data = from x in _timeSheet.Table
                       select x;
            return data;
        }


        public dynamic GetTimeSheetPolicyById(Entities.TimesheetPolicy value)
        {
            var data = _timeSheet.GetById(value.Id);
            return data;
        }

        public dynamic InsertUpdateTimesheet(TimesheetPolicy value)
        {
            try
            {
                if(value.Id  == 0) //Insert 
                {
                    bool checkDuplicate = _timeSheet.Table.Any(k => k.Name == value.Name);
                    if (checkDuplicate)
                    {
                        return false;
                    }
                    _timeSheet.Insert(value);
                    return true;
                }
                else // update
                {
                    bool checkDuplicate =  _timeSheet.Table.Any(k => k.Id != value.Id && k.Name == value.Name);
                    if (checkDuplicate)
                    {
                        return false;
                    }
                    _timeSheet.Update(value);
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public dynamic InsertUpdateTimesheet(Entities.TimesheetPolicy value)
        //{
        //    try
        //    {
        //        if(value.Id == 0) //Insert 
        //        {

        //            bool checkDuplicate = _timeSheet.Table.Any(k => k.Id != value.Id && (k.Name == value.Name));
        //            value.CreatedDate = DateTime.UtcNow;
        //            TimesheetPolicy response = _timeSheet.InsertAndGet(value);
        //            return value.Id;
        //        }
        //        else //Update
        //        {
        //            TimesheetPolicy TimeSheetById = _timeSheet.GetById(value.Id);
        //            DateTime abx = (DateTime)TimeSheetById.CreatedDate;
        //            value.CreatedDate = abx;
        //            value.ModifiedDate = DateTime.UtcNow;
        //            _timeSheet.UpdateAsNoTracking(value);
        //            return value.Id;
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        #endregion
    }
}
