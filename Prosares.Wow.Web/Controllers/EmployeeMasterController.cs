using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using Prosares.Wow.Data.Entities;
using Prosares.Wow.Data.Models;
using Prosares.Wow.Data.Services.Employee;
using System;
using System.Drawing;
using System.IO;
using static Prosares.Wow.Data.Services.Employee.EmployeeService;

namespace Prosares.Wow.Web.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class EmployeeMasterController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeMasterController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        #region Method
        [HttpGet]
        public IActionResult Get()
        {
            EmployeeMasterEntity Employee = _employeeService.GetEmployeeByLoginId("");
            return Ok(Employee);
        }



        [HttpPost]
        public JsonResponseModel getEmployeeMasterGridData([FromBody] EmployeeMasterEntity value)
        {
            JsonResponseModel apiResponse = new JsonResponseModel();
            try
            {

                apiResponse.Status = ApiStatus.OK;
                apiResponse.Data = _employeeService.GetEmployeeMasterGridData(value);
                apiResponse.Message = "Ok";
            }
            catch (System.Exception ex)
            {
                apiResponse.Status = ApiStatus.Error;
                apiResponse.Data = null;
                apiResponse.Message = ex.Message;

            }

            return apiResponse;
        }

        [HttpPost]
        public JsonResponseModel insertUpdatEmployeeMasterData([FromBody] EmployeeMasterEntity value)
        {
            JsonResponseModel apiResponse = new JsonResponseModel();
            try
            {

                bool result = _employeeService.InsertUpdateEmployeeMasterData(value);
                if (result)

                {
                    apiResponse.Status = ApiStatus.OK;
                    apiResponse.Data = "Inserted";
                    apiResponse.Message = "Ok";
                }
                else
                {
                    apiResponse.Status = ApiStatus.Error;
                    apiResponse.Data = null;
                    apiResponse.Message = "Record already Exists!";
                }
            }
            catch (System.Exception ex)
            {
                apiResponse.Status = ApiStatus.Error;
                apiResponse.Data = null;
                apiResponse.Message = ex.Message;

            }

            return apiResponse;
        }

        [HttpPost]
        public JsonResponseModel getEmployeeById([FromBody] EmployeeMasterEntity value)
        {
            JsonResponseModel apiResponse = new JsonResponseModel();
            try
            {
                var employee = _employeeService.GeEmployeeMasterById(value);//EmployeeMasterEntity 
                if (employee == null)
                {
                    apiResponse.Status = ApiStatus.Error;
                    apiResponse.Data = null;
                    apiResponse.Message = "No Employee Found";
                }
                apiResponse.Status = ApiStatus.OK;
                apiResponse.Data = employee;
                apiResponse.Message = "Ok";
            }
            catch (System.Exception ex)
            {
                apiResponse.Status = ApiStatus.Error;
                apiResponse.Data = null;
                apiResponse.Message = ex.Message;

            }

            return apiResponse;
        }

        [HttpPost]
        public JsonResponseModel GetEmployeeAvailableLeaveBalance([FromBody] EmployeeService.EmployeeLeaveRequest employee)
        {
            JsonResponseModel apiResponse = new JsonResponseModel();
            try
            {
                apiResponse.Status = ApiStatus.OK;
                apiResponse.Data = _employeeService.GetEmployeeAvailableLeaveBalance(employee);
                apiResponse.Message = "Ok";
            }
            catch (System.Exception ex)
            {
                apiResponse.Status = ApiStatus.Error;
                apiResponse.Data = null;
                apiResponse.Message = ex.Message;

            }
            return apiResponse;
        }

        [HttpPost]
        public JsonResponseModel GetEmployeeReportingManager([FromBody] EmployeeService.EmployeeLeaveRequest employee)
        {
            JsonResponseModel apiResponse = new JsonResponseModel();
            try
            {
                apiResponse.Status = ApiStatus.OK;
                apiResponse.Data = _employeeService.GetEmployeeReportingManager(employee);
                apiResponse.Message = "Ok";
            }
            catch (System.Exception ex)
            {
                apiResponse.Status = ApiStatus.Error;
                apiResponse.Data = null;
                apiResponse.Message = ex.Message;

            }
            return apiResponse;
        }

        [HttpPost]
        public dynamic ExportToExcel(EmployeeMasterEntity value)
        {
            var data = _employeeService.SaiExportToExcel(value);
            MemoryStream ms = new MemoryStream();
            byte[] dataBytes;
            if (data == null)
            {
                throw new ArgumentNullException("stream");
            }
            var properties = Array.Empty<object>();
            //properties = new[] { "Engagement", "Engagement Type", "Milestone", "PO Value", "Amount", "Planned Date", "Actual Date", "Invoice Date" };

            properties = new[]
                   {     "Name",
                        "ShortName",
                        "Reporting Manager",
                        "Cost Center",
                        "Policy Name",
                        "TimeSheet Policy",
                        "RoleName",
                        "Efficiency",
                        "IsActive"

                    };

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var xlPackage = new ExcelPackage(ms))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("Report");
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
                foreach (EmployeeExportToExcelModel item in data)
                {
                    int col = 1;
                    worksheet.Cells[row, col].Value = item.Name;
                    col++;

                    worksheet.Cells[row, col].Value = item.ShortName;
                    col++;

                    worksheet.Cells[row, col].Value = item;
                    col++;

                    worksheet.Cells[row, col].Value = item.CostCenter;
                    col++;


                    worksheet.Cells[row, col].Value = item.WorkPolicy;
                    col++;


                    worksheet.Cells[row, col].Value = item.TimeSheetPolicy;
                    col++;

                    worksheet.Cells[row, col].Value = item.RoleName;
                    col++;

                    worksheet.Cells[row, col].Value = item.Efficiency;
                    if (item.Efficiency == 1)
                    {
                        worksheet.Cells[row, col].Value = 0.25;
                        col++;
                    }
                    else if (item.Efficiency == 2)
                    {
                        worksheet.Cells[row, col].Value = 0.5;
                        col++;
                    }
                    else if (item.Efficiency == 3)
                    {
                        worksheet.Cells[row, col].Value = 0.75;
                        col++;
                    }
                    else
                    {
                        worksheet.Cells[row, col].Value = 1;
                        col++;
                    }
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
                worksheet.Cells.AutoFitColumns();

                xlPackage.Save();
                dataBytes = ms.ToArray();
            }
            return File(dataBytes, "text/xls", "" + "MileStone Report" + "" + DateTime.Now.ToString() + ".xlsx");
        }

        #endregion

    }
}

