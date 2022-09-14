using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using Prosares.Wow.Data.Entities;
using Prosares.Wow.Data.Models;
using Prosares.Wow.Data.Services.CapacityUtilizationReport;
using Prosares.Wow.Data.Services.MilestoneReport;
using System.IO;
using System;
using System.Drawing;

namespace Prosares.Wow.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MileStoneReportController : ControllerBase
    {

        private readonly IMileStoneReport _mileStoneReport;

        public MileStoneReportController(IMileStoneReport mileStoneReport)
        {
            _mileStoneReport = mileStoneReport;
        }

        [HttpPost]
        public dynamic GetMileStoneReportData([FromBody]  MilestoneReportEntity value)
        {
            JsonResponseModel apiResponse = new JsonResponseModel();
            try
            {
                apiResponse.Status = ApiStatus.OK;
                apiResponse.Data = _mileStoneReport.MilestoneDashboardData(value);
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
        public dynamic MileStoneExportToExcel([FromBody] MilestoneReportEntity value)
        {
            MileStoneReportResponse x = new MileStoneReportResponse();
             x = _mileStoneReport.MilestoneDashboardData(value);
            var data = x.Data;
            MemoryStream ms = new MemoryStream();
            byte[] dataBytes;
            if (data == null)
            {
                throw new ArgumentNullException("stream");
            }
            var properties = Array.Empty<object>();
            properties = new[] { "Engagement", "Engagement Type", "Milestone", "PO Value", "Amount", "Planned Date", "Actual Date", "Invoice Date" };

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
                foreach (var item in data)
                {
                    int col = 1;
                    worksheet.Cells[row, col].Value = item.Engagement;
                    col++;
                    worksheet.Cells[row, col].Value = item.EngagementType;
                    col++;  
                    worksheet.Cells[row, col].Value = item.MileStone;
                    col++;
                    worksheet.Cells[row, col].Value = item.POValue;
                    col++;
                    worksheet.Cells[row, col].Value = item.Amount;
                    col++;
                    worksheet.Cells[row, col].Value = item.PlannedDate;
                    col++;
                    worksheet.Cells[row, col].Value = item.RevisedDate;
                    col++;
                    worksheet.Cells[row, col].Value = item.InvoicedDate;
                    col++;
                    row++;
                }
                worksheet.Cells.AutoFitColumns();

                xlPackage.Save();
                dataBytes = ms.ToArray();
            }
            return File(dataBytes, "text/xls", "" + "MileStone Report" + "" + DateTime.Now.ToString() + ".xlsx");
        }
    }
 }


