﻿using Prosares.Wow.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prosares.Wow.Data.Services.TimeSheetPolicy
{
     public interface ITimeSheetPolicy
     {
        public dynamic GetTimeSheetPolicy(Entities.TimesheetPolicy value);

        public dynamic InsertUpdateTimesheet(Entities.TimesheetPolicy value);

        public dynamic GetTimeSheetPolicyById(Entities.TimesheetPolicy value);

     
     }
}
