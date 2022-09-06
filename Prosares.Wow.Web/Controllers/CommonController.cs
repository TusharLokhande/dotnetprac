﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Prosares.Wow.Data.Models;
using Prosares.Wow.Data.Services.Application;
using Prosares.Wow.Data.Services.CostCenterMaster;
using Prosares.Wow.Data.Services.Customers;
using Prosares.Wow.Data.Services.Employee;
using Prosares.Wow.Data.Services.EngagementMaster;
using Prosares.Wow.Data.Services.ManagerDashboard;
using Prosares.Wow.Data.Services.Milestone;
using Prosares.Wow.Data.Services.Phase;
using Prosares.Wow.Web.Controllers.Prosares.Wow.Data.Models;
using System;
using System.Drawing;
using System.IO;

namespace Prosares.Wow.Web.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly IManagerDashboardService _managerDashboardService;
        private readonly ICustomerService _customerService;
        private readonly IApplicationService _applicationService;
        private readonly IEmployeeService _employeeService;
        private readonly IPhaseMasterService _phaseMasterService;
        private readonly ICostCenterService _costCenterService;
        private readonly IMilestoneService _milestoneService;
        private readonly IEngagementMasterService _engagementService;
        public CommonController(IManagerDashboardService managerDashboardService,
            ICustomerService customerService,
        IApplicationService applicationService,
        IEmployeeService employeeService,
        IPhaseMasterService phaseMasterService,
        ICostCenterService costCenterService,
        IMilestoneService milestoneService,
        IEngagementMasterService engagementMasterService)
        {
            _managerDashboardService = managerDashboardService;
            _customerService = customerService;
            _applicationService = applicationService;
            _employeeService = employeeService;
            _phaseMasterService = phaseMasterService;
            _costCenterService = costCenterService;
            _milestoneService = milestoneService;
            _engagementService = engagementMasterService;
        }

        [HttpPost]
        public ActionResult CommonExportToExcel([FromBody] CommonMasterModel common)
        {

            try
            {
                if (common != null)
                {

                    byte[] byt;

                    using (MemoryStream stream = new MemoryStream())
                    {

                        if (common.master == "application")
                        {
                            var applicationData = _applicationService.ApplicationExportToExcel(common.searchText, common.sortColumn, common.sortDirection);
                            ExportToExcelForAllMasters(stream, applicationData, common.master);
                            byt = stream.ToArray();

                        }
                        else if (common.master == "customer")
                        {
                            var customerData = _customerService.CustomerExportToExcel(common.searchText, common.sortColumn, common.sortDirection);
                            ExportToExcelForAllMasters(stream, customerData, common.master);

                            byt = stream.ToArray();
                        }
                        else if (common.master == "employee")
                        {
                            var employeeData = _employeeService.EmployeeExportToExcel(common.searchText, common.sortColumn, common.sortDirection);
                            ExportToExcelForAllMasters(stream, employeeData, common.master);
                            byt = stream.ToArray();

                        }
                        else if (common.master == "phase")
                        {

                            var phaseData = _phaseMasterService.PhaseExportToExcel(common.searchText, common.sortColumn, common.sortDirection);
                            ExportToExcelForAllMasters(stream, phaseData, common.master);
                            byt = stream.ToArray();

                        }
                        else if (common.master == "costcenter")
                        {
                            var costcenterData = _costCenterService.CostcenterExportToExcel(common.searchText, common.sortColumn, common.sortDirection);
                            ExportToExcelForAllMasters(stream, costcenterData, common.master);
                            byt = stream.ToArray();
                        }
                        else if (common.master == "milestone")
                        {
                            var milestoneData = _milestoneService.MilestoneExportToExcel(common.searchText, common.sortColumn, common.sortDirection);
                            ExportToExcelForAllMasters(stream, milestoneData, common.master);
                            byt = stream.ToArray();
                        }
                        else if (common.master == "engagement")
                        {
                            var engagementData = _engagementService.EngagementExportToExcel(common.searchText, common.sortColumn, common.sortDirection);
                            ExportToExcelForAllMasters(stream, engagementData, common.master);
                            byt = stream.ToArray();

                        }

                        else
                        {
                            return NotFound();
                        }

                    }
                    return File(byt, "text/xls", "" + common.master + "" + DateTime.Now.ToString() + ".xlsx");

                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [NonAction]
        public virtual void ExportToExcelForAllMasters(Stream stream, dynamic lstentity, string master)
        {

            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("Masters");

                var properties = Array.Empty<object>();
                if (master == "application")
                {
                    properties = new[]
                    {
                        "Application",
                        "IsActive"
                     };
                }
                else if (master == "customer")
                {

                    properties = new[]
                    {
                        "Name",
                        "Abbreviation",
                        "IsActive"
                    };
                }
                else if (master == "employee")
                {

                    properties = new[]
                    {
                        "Name",
                        "ShortName",
                        "IsActive"
                    };
                }
                else if (master == "phase")
                {

                    properties = new[]
                    {
                        "Phase",
                        "Description",
                        "IsActive"
                    };
                }
                else if (master == "costcenter")
                {

                    properties = new[]
                    {
                        "CostCenter",
                        "IsActive"
                    };
                }
                else if (master == "milestone")
                {

                    properties = new[]
                    {
                        "MileStone",
                        "Amount",
                        "PlannedDate",
                        "RevisedDate",
                        "CompletedDate",
                        "InvoiceDate",
                        "IsActive"
                    };
                }
                else if (master == "engagement")
                {

                    properties = new[]
                    {
                        "Engagement",
                        "POValue",
                        "POMonths",
                        "Billing",
                        "BudgetResoucesPerMonth",
                        "POManDaysPerMonth",
                        "BudgetMandaysPerMonth",
                        "EngagementStatus",
                        "InvoiceValue",
                        "BalanceValue",
                        "SpentMandates",
                        "BalanceMandays",
                        "ActualCost",
                        "POCompletionDate",
                        "ActualCompletionDate",
                        "IsActive"

                    };
                }

                for (int i = 0; i < properties.Length; i++)
                {

                    worksheet.Cells[1, i + 1].Value = properties[i];

                    //Bold Text
                    worksheet.Cells[1, i + 1].Style.Font.Bold = true;

                    //Border
                    worksheet.Cells[1, i + 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[1, i + 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[1, i + 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[1, i + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    //Border Color
                    worksheet.Cells[1, i + 1].Style.Border.Top.Color.SetColor(Color.Black);
                    worksheet.Cells[1, i + 1].Style.Border.Bottom.Color.SetColor(Color.Black);
                    worksheet.Cells[1, i + 1].Style.Border.Right.Color.SetColor(Color.Black);
                    worksheet.Cells[1, i + 1].Style.Border.Left.Color.SetColor(Color.Black);

                    //center alignment of text
                    worksheet.Cells[1, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[1, i + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    //Set Font Size
                    worksheet.Cells[1, i + 1].Style.Font.Size = 12;
                }
                int row = 2;

                foreach (var item in lstentity)
                {
                    if (master == "application")
                    {
                        int col = 1;
                        worksheet.Cells[row, col].Value = item.Application;
                        col++;

                        worksheet.Cells[row, col].Value = item.IsActive;

                        if (item.IsActive)
                        {
                            worksheet.Cells[row, col].Value = "Yes";
                            col++;
                        }
                        else
                        {
                            worksheet.Cells[row, col].Value = "No";
                            col++;
                        }
                        row++;

                    }

                    else if (master == "customer")
                    {
                        int col = 1;
                        worksheet.Cells[row, col].Value = item.Name;
                        col++;

                        worksheet.Cells[row, col].Value = item.Abbreviation;
                        col++;

                        worksheet.Cells[row, col].Value = item.IsActive;

                        if (item.IsActive)
                        {
                            worksheet.Cells[row, col].Value = "Yes";
                            col++;
                        }
                        else
                        {
                            worksheet.Cells[row, col].Value = "No";
                            col++;
                        }
                        row++;

                    }
                    else if (master == "employee")
                    {
                        int col = 1;
                        worksheet.Cells[row, col].Value = item.Name;
                        col++;

                        worksheet.Cells[row, col].Value = item.ShortName;
                        col++;

                        worksheet.Cells[row, col].Value = item.IsActive;

                        if (item.IsActive)
                        {
                            worksheet.Cells[row, col].Value = "Yes";
                            col++;
                        }
                        else
                        {
                            worksheet.Cells[row, col].Value = "No";
                            col++;
                        }
                        row++;

                    }
                    else if (master == "phase")
                    {
                        int col = 1;
                        worksheet.Cells[row, col].Value = item.Phase;
                        col++;

                        worksheet.Cells[row, col].Value = item.Description;
                        col++;

                        worksheet.Cells[row, col].Value = item.IsActive;

                        if (item.IsActive)
                        {
                            worksheet.Cells[row, col].Value = "Yes";
                            col++;
                        }
                        else
                        {
                            worksheet.Cells[row, col].Value = "No";
                            col++;
                        }
                        row++;
                    }
                    else if (master == "costcenter")
                    {
                        int col = 1;
                        worksheet.Cells[row, col].Value = item.CostCenter1;
                        col++;

                        worksheet.Cells[row, col].Value = item.IsActive;

                        if (item.IsActive)
                        {
                            worksheet.Cells[row, col].Value = "Yes";
                            col++;
                        }
                        else
                        {
                            worksheet.Cells[row, col].Value = "No";
                            col++;
                        }
                        row++;
                    }
                    else if (master == "milestone")
                    {
                        int col = 1;
                        worksheet.Cells[row, col].Value = item.MileStones;
                        col++;

                        worksheet.Cells[row, col].Value = item.Amount;
                        col++;

                        worksheet.Cells[row, col].Value = Convert.ToString(Convert.ToDateTime(item.PlannedDate));
                        col++;

                        worksheet.Cells[row, col].Value = Convert.ToString(Convert.ToDateTime(item.RevisedDate));
                        col++;

                        worksheet.Cells[row, col].Value = Convert.ToString(Convert.ToDateTime(item.CompletedDate));
                        col++;

                        worksheet.Cells[row, col].Value = Convert.ToString(Convert.ToDateTime(item.InvoicedDate));
                        col++;

                        worksheet.Cells[row, col].Value = item.IsActive;

                        if (item.IsActive)
                        {
                            worksheet.Cells[row, col].Value = "Yes";
                            col++;
                        }
                        else
                        {
                            worksheet.Cells[row, col].Value = "No";
                            col++;
                        }
                        row++;
                    }
                    else if (master == "engagement")
                    {
                        int col = 1;
                        worksheet.Cells[row, col].Value = item.Engagement;
                        col++;

                        worksheet.Cells[row, col].Value = item.Povalue;
                        col++;

                        worksheet.Cells[row, col].Value = item.Pomonths;
                        col++;

                        worksheet.Cells[row, col].Value = item.Billing;
                        col++;

                        worksheet.Cells[row, col].Value = item.BudgetResoucesPerMonth;
                        col++;

                        worksheet.Cells[row, col].Value = item.PomanDaysPerMonth;
                        col++;

                        worksheet.Cells[row, col].Value = item.BudgetMandaysPerMonth;
                        col++;

                        worksheet.Cells[row, col].Value = item.EngagementStatus;
                        col++;


                        worksheet.Cells[row, col].Value = item.InvoiceValue;
                        col++;

                        worksheet.Cells[row, col].Value = item.BalanceValue;
                        col++;

                        worksheet.Cells[row, col].Value = item.SpentMandates;
                        col++;

                        worksheet.Cells[row, col].Value = item.BalanceMandays;
                        col++;

                        worksheet.Cells[row, col].Value = item.ActualCost;
                        col++;


                        worksheet.Cells[row, col].Value = Convert.ToString(Convert.ToDateTime(item.PocompletionDate));
                        col++;

                        worksheet.Cells[row, col].Value = Convert.ToString(Convert.ToDateTime(item.ActualCompletionDate));
                        col++;

                        worksheet.Cells[row, col].Value = item.IsActive;

                        if (item.IsActive)
                        {
                            worksheet.Cells[row, col].Value = "Yes";
                            col++;
                        }
                        else
                        {
                            worksheet.Cells[row, col].Value = "No";
                            col++;
                        }
                        row++;
                    }
                }
                worksheet.Cells.AutoFitColumns();

                xlPackage.Save();
            }
        }

        [HttpPost]



        public ActionResult ExportToExcel([FromBody] ManagerDashboardModel value)
        {
            try
            {
                if (value != null)
                {

                    ManagerDashboardModelResponse lst = _managerDashboardService.GetManagerDashboardData(value);
                    byte[] bytes;

                    using (var stream = new MemoryStream())
                    {
                        if (value.Type == "task")
                        {
                            ExportToExcelForTaskAndTicket(stream, lst.TaskData, "task");
                            bytes = stream.ToArray();
                        }
                        else
                        {
                            ExportToExcelForTaskAndTicket(stream, lst.TicketData, "ticket");
                            bytes = stream.ToArray();
                        }

                    }
                    return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet; charset=utf-8", "TaskGrid_" + DateTime.Now.ToString() + ".xlsx");

                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [NonAction]
        public virtual void ExportToExcelForTaskAndTicket(Stream stream, dynamic lstentity, string type)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            //run the real code of the sample now
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var xlPackage = new ExcelPackage(stream))
            {
                // get handle to the existing worksheet
                dynamic worksheet="";
                if (type == "task")
                {
                     worksheet = xlPackage.Workbook.Worksheets.Add("Tasks");
                }
                else
                {
                     worksheet = xlPackage.Workbook.Worksheets.Add("Tickets");

                }
                //Create Headers and format them+
                var properties = Array.Empty<object>();
                if (type == "task")
                {
                    properties = new[]
                    {
                        "Task Id",
                        "Customer",
                        "Engagement",
                        "Engagement Type",  
                        "Phase Name",
                        "Task Title",
                        "Task Description",
                        "Assigned To",
                        "Hours Assigned",
                        "Hours Spent",
                        "Is Chargeable",
                        "Not Chargeable Reason",
                        "Planned Start Date",
                        "Planned End Date",
                        "Remarks",
                        "Created By",
                        "Created Date"
                        
                     };
                }
                else
                {
                    properties = new[]
                    {
                        
                        "TicketId",
                        "Customer",
                        "Engagement",
                        "Engagement Type",
                        "Application Name",
                        "TicketTitle",
                        "TicketDescription",
                        "Assigned To",
                        "Hours Assigned",
                        "HoursSpent",
                        "Priority",
                        "Is Chargeable",
                        "Not Chargeable Reason",
                        "Incident Date",
                        "Report Date",
                        "Start Date",
                        "Resolve Date",
                        "Remarks",
                        "Created By",
                        "Created Date"

                     };
                }

                for (int i = 0; i < properties.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = properties[i];

                    //Bold Text
                    worksheet.Cells[1, i + 1].Style.Font.Bold = true;

                    //Border
                    worksheet.Cells[1, i + 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[1, i + 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[1, i + 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[1, i + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    //Border Color
                    worksheet.Cells[1, i + 1].Style.Border.Top.Color.SetColor(Color.Black);
                    worksheet.Cells[1, i + 1].Style.Border.Bottom.Color.SetColor(Color.Black);
                    worksheet.Cells[1, i + 1].Style.Border.Right.Color.SetColor(Color.Black);
                    worksheet.Cells[1, i + 1].Style.Border.Left.Color.SetColor(Color.Black);

                    //center alignment of text
                    worksheet.Cells[1, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[1, i + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    //Set Font Size
                    worksheet.Cells[1, i + 1].Style.Font.Size = 12;
                }

                int row = 2;

                foreach (var item in lstentity)
                {
                    int col = 1;

                    worksheet.Cells[row, col].Value = item.Id;
                    col++;

                    worksheet.Cells[row, col].Value = item.Customer;
                    col++;

                    worksheet.Cells[row, col].Value = item.EngagementString;
                    col++;

                    worksheet.Cells[row, col].Value = item.EngagementTypeString;
                    col++;

                    if (type == "task")
                    {
                        //worksheet.Cells[row, col].Value = item.Id;
                        //col++;

                        worksheet.Cells[row, col].Value = item.PhaseString;
                        col++;

                     


                        worksheet.Cells[row, col].Value = item.TaskTitle;
                        col++;

                        worksheet.Cells[row, col].Value = item.TaskDescription;
                        col++;
                    }
                    else
                    {
                        //worksheet.Cells[row, col].Value = item.Id;
                        //col++;


                        worksheet.Cells[row, col].Value = item.ApplicationString;
                        col++;

                        worksheet.Cells[row, col].Value = item.TicketTitle;
                        col++;

                        worksheet.Cells[row, col].Value = item.TicketDescription;
                        col++;


                    }
                    worksheet.Cells[row, col].Value = item.AssignedToString;
                    col++;

        

                    worksheet.Cells[row, col].Value = item.HoursAssigned;
                    col++;

                    worksheet.Cells[row, col].Value = item.HoursSpent;
                    col++;

                    if (type != "task")
                    {
                        worksheet.Cells[row, col].Value = item.PriorityString;
                        col++;
                    }
                    worksheet.Cells[row, col].Value = item.IsChargeable;
                    col++;

                    worksheet.Cells[row, col].Value = item.NoChargesReason;
                    col++;

                    if (type == "task")
                    {
                        worksheet.Cells[row, col].Value = (Convert.ToDateTime(item.PlannedStartDate)).ToString("dd/MMM/yyyy");
                        col++;

                        worksheet.Cells[row, col].Value = (Convert.ToDateTime(item.PlannedEndDate)).ToString("dd/MMM/yyyy");
                        col++;
                    }
                    else
                    {
                        worksheet.Cells[row, col].Value = (Convert.ToDateTime(item.IncidentDate)).ToString("dd/MMM/yyyy"); 
                        col++;

                        worksheet.Cells[row, col].Value = (Convert.ToDateTime(item.ReportDate)).ToString("dd/MMM/yyyy"); 
                        col++; 
                        
                        worksheet.Cells[row, col].Value = (Convert.ToDateTime(item.StartDate)).ToString("dd/MMM/yyyy"); 
                        col++;

                        worksheet.Cells[row, col].Value = (Convert.ToDateTime(item.ResolveDate)).ToString("dd/MMM/yyyy"); 
                        col++;
                    }

                    worksheet.Cells[row, col].Value = item.Remarks;
                    col++;


                    worksheet.Cells[row, col].Value = item.CreatedByString;
                    col++;

                    worksheet.Cells[row, col].Value = (Convert.ToDateTime(item.CreatedDate)).ToString("dd/MMM/yyyy");
                    col++;

                    row++;
                }

                worksheet.Cells.AutoFitColumns();

                xlPackage.Save();
            }

        }
    }
}