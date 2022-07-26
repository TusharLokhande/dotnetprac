﻿using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Prosares.Wow.Data.Entities;
using Prosares.Wow.Data.Helpers;
using Prosares.Wow.Data.Models;
using Prosares.Wow.Data.Repository;
using Prosares.Wow.Data.Services.Mail;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Prosares.Wow.Data.Services.LeaveRequest
{
    public class LeaveRequestMasterService : ILeaveRequestMasterService
    {

        #region Prop
        private readonly IRepository<Entities.LeaveRequestsMaster> _leaveRequest;
        private readonly ILogger<LeaveRequestMasterService> _logger;
        private readonly IRepository<Entities.EmployeeMasterEntity> _employeeMaster;
        private readonly IRepository<Entities.EmployeeLeaveBalance> _employeeLeaveBalance;
        private readonly IMailService _mailService;
        private readonly IRepository<Entities.LeavesResonMaster> _leaveReason;

        #endregion

        #region Constructor
        public LeaveRequestMasterService(IRepository<Entities.LeaveRequestsMaster> LeaveRequestMaster,
                        ILogger<LeaveRequestMasterService> logger, IRepository<Entities.EmployeeMasterEntity> employeeMaster, IRepository<EmployeeLeaveBalance> employeeLeaveBalance, IMailService mailService
                        ,IRepository<Entities.LeavesResonMaster> leaveReason)
        {
            _leaveRequest = LeaveRequestMaster;
            _logger = logger;
            _employeeMaster = employeeMaster;
            _employeeLeaveBalance = employeeLeaveBalance;
            _mailService = mailService;
            _leaveReason = leaveReason;
        }
        #endregion

        #region Method
        public dynamic GetLeaveReqMasterMasterGridData(Entities.LeaveRequestsMaster value)//LeaveRequestsMasterResponse //Entities.LeaveRequestsMaster value
        {
            try
            {
                //SqlCommand command = new SqlCommand("stpGetAllLeaveRequests");
                //command.CommandType = CommandType.StoredProcedure;
                //var leaveRequestMasterData = _leaveRequest.GetRecords(command);
                //return leaveRequestMasterData;

                var data = new LeaveRequestsMasterResponse();

                List<LeaveRequestsMaster> leaveRequestsMasters = _leaveRequest.GetAll().ToList();

                foreach (var item in leaveRequestsMasters)
                {
                    item.ApproverName = _employeeMaster.Table.Where(k => k.Id == item.ApproverId).Select(s => s.Name).SingleOrDefault();
                    item.RequesterName = _employeeMaster.Table.Where(k => k.Id == item.RequestorId).Select(s => s.Name).SingleOrDefault();
                    item.FYIUsersIntList = item.FYIUsers != null ? new List<int>(Array.ConvertAll(item.FYIUsers.Split(','), int.Parse)) : new List<int>();
                    item.LeavesReson = _leaveReason.Table.Where(k => k.IsActive == true && k.Id == item.ResonId).Select(s => s.LeavesReson).FirstOrDefault();
                }

                //Expression<Func<Entities.LeaveRequestsMaster, bool>> InitialCondition;
                //Expression<Func<Entities.LeaveRequestsMaster, bool>> SearchText;

                //data.leaveRequestsMasterData = leaveRequestsMasters.ToList();

                //if (value.RoleId == 1) // get All request
                //{
                //    InitialCondition = k => k.IsActive == true && k.ApproverId != 0 && k.RequestorId != 0;
                //}
                //else if (value.RoleId == 2) // get request only for approver
                //{
                //    InitialCondition = k => k.IsActive == true && k.ApproverId == value.EmployeeId || k.FYIUsersIntList.Contains((int)value.EmployeeId);
                //}
                //else if (value.RoleId == 3) // get request only for requestor
                //{
                //    InitialCondition = k => k.IsActive == true && k.RequestorId == value.EmployeeId || k.FYIUsersIntList.Contains((int)value.EmployeeId);
                //}
                //else
                //{
                //    InitialCondition = k => k.IsActive == true;
                //}

                //if (value.searchText != null)
                //{
                //    SearchText = k => k.ApproverName.Contains(value.searchText) || k.RequesterName.Contains(value.searchText);
                //}
                //else
                //{
                //    SearchText = k => k.ApproverName != "";
                //}

                if (value.sortColumn == "" || value.sortDirection == "")
                {
                    
                    if (value.RoleId == 1) // get All request
                    {
                        data.count = leaveRequestsMasters.AsEnumerable().Where(k => k.IsActive == true && k.ApproverId != 0 && k.RequestorId != 0)
                            .Where(k => value.searchText != null ?
                            k.ApproverName != null && k.ApproverName.Contains(value.searchText) || k.RequesterName != null && k.RequesterName.Contains(value.searchText) :
                            k.ApproverName != ""
                            )
                            .ToList().Count();

                        data.leaveRequestsMasterData = leaveRequestsMasters.AsEnumerable().Where(k => k.IsActive == true && k.ApproverId != 0 && k.RequestorId != 0)
                            .Where(k => value.searchText != null ?
                            k.ApproverName != null && k.ApproverName.Contains(value.searchText) || k.RequesterName != null && k.RequesterName.Contains(value.searchText) :
                            k.ApproverName != ""
                            )
                            .OrderByDescending(k => k.CreatedDate)
                            .Skip(value.start).Take(value.pageSize).ToList();

                    } else if (value.RoleId == 2) // get request only for approver
                    {
                        data.count = leaveRequestsMasters.AsEnumerable().Where(k => k.IsActive == true && (k.ApproverId == value.EmployeeId || k.FYIUsersIntList.Contains((int)value.EmployeeId)))
                            .Where(k => value.searchText != null ?
                            k.ApproverName != null && k.ApproverName.Contains(value.searchText) || k.RequesterName != null && k.RequesterName.Contains(value.searchText) :
                            k.ApproverName != ""
                            )
                            .ToList().Count();
                        data.leaveRequestsMasterData = leaveRequestsMasters.AsEnumerable().Where(k => k.IsActive == true && (k.ApproverId == value.EmployeeId || k.FYIUsersIntList.Contains((int)value.EmployeeId)))
                            .Where(k => value.searchText != null ?
                            k.ApproverName != null && k.ApproverName.Contains(value.searchText) || k.RequesterName != null && k.RequesterName.Contains(value.searchText) :
                            k.ApproverName != ""
                            )
                            .OrderByDescending(k => k.CreatedDate)
                            .Skip(value.start).Take(value.pageSize).ToList();
                    }
                    else if (value.RoleId == 3) // get request only for requestor
                    
                    {

                        data.count = leaveRequestsMasters.AsEnumerable().Where(k => k.IsActive == true && (k.RequestorId == value.EmployeeId || k.FYIUsersIntList.Contains((int)value.EmployeeId)))
                            .Where(k => value.searchText != null ?
                            k.ApproverName != null && k.ApproverName.Contains(value.searchText) || k.RequesterName != null && k.RequesterName.Contains(value.searchText) :
                            k.ApproverName != ""
                            )
                            .ToList().Count();
                        data.leaveRequestsMasterData = leaveRequestsMasters.AsEnumerable().Where(k => k.IsActive == true && (k.RequestorId == value.EmployeeId || k.FYIUsersIntList.Contains((int)value.EmployeeId)))
                            .Where(k => value.searchText != null ?
                            k.ApproverName != null && k.ApproverName.Contains(value.searchText) || k.RequesterName != null && k.RequesterName.Contains(value.searchText) :
                            k.ApproverName != ""
                            )
                            .OrderByDescending(k => k.CreatedDate)
                            .Skip(value.start).Take(value.pageSize).ToList();
                    }

                }
                else if (value.sortDirection == "desc")
                {
                    if (value.RoleId == 1) // get All request
                    {
                        data.count = leaveRequestsMasters.AsEnumerable().Where(k => k.IsActive == true && k.ApproverId != 0 && k.RequestorId != 0)
                            .Where(k => value.searchText != null ?
                            k.ApproverName != null && k.ApproverName.Contains(value.searchText) || k.RequesterName != null && k.RequesterName.Contains(value.searchText) :
                            k.ApproverName != ""
                            )
                            .ToList().Count();

                        data.leaveRequestsMasterData = leaveRequestsMasters.AsEnumerable().Where(k => k.IsActive == true && k.ApproverId != 0 && k.RequestorId != 0)
                            .Where(k => value.searchText != null ?
                            k.ApproverName != null && k.ApproverName.Contains(value.searchText) || k.RequesterName != null && k.RequesterName.Contains(value.searchText) :
                            k.ApproverName != ""
                            )
                            .OrderByDescending(k => value.sortColumn)
                            .Skip(value.start).Take(value.pageSize).ToList();

                    }
                    else if (value.RoleId == 2) // get request only for approver
                    {
                        data.count = leaveRequestsMasters.AsEnumerable().Where(k => k.IsActive == true && (k.ApproverId == value.EmployeeId || k.FYIUsersIntList.Contains((int)value.EmployeeId)))
                            .Where(k => value.searchText != null ?
                            k.ApproverName != null && k.ApproverName.Contains(value.searchText) || k.RequesterName != null && k.RequesterName.Contains(value.searchText) :
                            k.ApproverName != ""
                            )
                            .ToList().Count();
                        data.leaveRequestsMasterData = leaveRequestsMasters.AsEnumerable().Where(k => k.IsActive == true && (k.ApproverId == value.EmployeeId || k.FYIUsersIntList.Contains((int)value.EmployeeId)))
                            .Where(k => value.searchText != null ?
                            k.ApproverName != null && k.ApproverName.Contains(value.searchText) || k.RequesterName != null && k.RequesterName.Contains(value.searchText) :
                            k.ApproverName != ""
                            )
                            .OrderByDescending(k => value.sortColumn)
                            .Skip(value.start).Take(value.pageSize).ToList();
                    }
                    else if (value.RoleId == 3) // get request only for requestor

                    {

                        data.count = leaveRequestsMasters.AsEnumerable().Where(k => k.IsActive == true && (k.RequestorId == value.EmployeeId || k.FYIUsersIntList.Contains((int)value.EmployeeId)))
                            .Where(k => value.searchText != null ?
                            k.ApproverName != null && k.ApproverName.Contains(value.searchText) || k.RequesterName != null && k.RequesterName.Contains(value.searchText) :
                            k.ApproverName != ""
                            )
                            .ToList().Count();
                        data.leaveRequestsMasterData = leaveRequestsMasters.AsEnumerable().Where(k => k.IsActive == true && (k.RequestorId == value.EmployeeId || k.FYIUsersIntList.Contains((int)value.EmployeeId)))
                            .Where(k => value.searchText != null ?
                            k.ApproverName != null && k.ApproverName.Contains(value.searchText) || k.RequesterName != null && k.RequesterName.Contains(value.searchText) :
                            k.ApproverName != ""
                            )
                            .OrderByDescending(k => value.sortColumn)
                            .Skip(value.start).Take(value.pageSize).ToList();
                    }
                }
                else if (value.sortDirection == "asc")
                {
                    if (value.RoleId == 1) // get All request
                    {
                        data.count = leaveRequestsMasters.AsEnumerable().Where(k => k.IsActive == true && k.ApproverId != 0 && k.RequestorId != 0)
                            .Where(k => value.searchText != null ?
                            k.ApproverName != null && k.ApproverName.Contains(value.searchText) || k.RequesterName != null && k.RequesterName.Contains(value.searchText) :
                            k.ApproverName != ""
                            )
                            .ToList().Count();

                        data.leaveRequestsMasterData = leaveRequestsMasters.AsEnumerable().Where(k => k.IsActive == true && k.ApproverId != 0 && k.RequestorId != 0)
                            .Where(k => value.searchText != null ?
                            k.ApproverName != null && k.ApproverName.Contains(value.searchText) || k.RequesterName != null && k.RequesterName.Contains(value.searchText) :
                            k.ApproverName != ""
                            )
                            .OrderBy(k => value.sortColumn)
                            .Skip(value.start).Take(value.pageSize).ToList();

                    }
                    else if (value.RoleId == 2) // get request only for approver
                    {
                        data.count = leaveRequestsMasters.AsEnumerable().Where(k => k.IsActive == true && (k.ApproverId == value.EmployeeId || k.FYIUsersIntList.Contains((int)value.EmployeeId)))
                            .Where(k => value.searchText != null ?
                            k.ApproverName != null && k.ApproverName.Contains(value.searchText) || k.RequesterName != null && k.RequesterName.Contains(value.searchText) :
                            k.ApproverName != ""
                            )
                            .ToList().Count();
                        data.leaveRequestsMasterData = leaveRequestsMasters.AsEnumerable().Where(k => k.IsActive == true && (k.ApproverId == value.EmployeeId || k.FYIUsersIntList.Contains((int)value.EmployeeId)))
                            .Where(k => value.searchText != null ?
                            k.ApproverName != null && k.ApproverName.Contains(value.searchText) || k.RequesterName != null && k.RequesterName.Contains(value.searchText) :
                            k.ApproverName != ""
                            )
                            .OrderBy(k => value.sortColumn)
                            .Skip(value.start).Take(value.pageSize).ToList();
                    }
                    else if (value.RoleId == 3) // get request only for requestor

                    {

                        data.count = leaveRequestsMasters.AsEnumerable().Where(k => k.IsActive == true && (k.RequestorId == value.EmployeeId || k.FYIUsersIntList.Contains((int)value.EmployeeId)))
                            .Where(k => value.searchText != null ?
                            k.ApproverName != null && k.ApproverName.Contains(value.searchText) || k.RequesterName != null && k.RequesterName.Contains(value.searchText) :
                            k.ApproverName != ""
                            )
                            .ToList().Count();
                        data.leaveRequestsMasterData = leaveRequestsMasters.AsEnumerable().Where(k => k.IsActive == true && (k.RequestorId == value.EmployeeId || k.FYIUsersIntList.Contains((int)value.EmployeeId)))
                            .Where(k => value.searchText != null ?
                            k.ApproverName != null && k.ApproverName.Contains(value.searchText) || k.RequesterName != null && k.RequesterName.Contains(value.searchText) :
                            k.ApproverName != ""
                            )
                            .OrderBy(k => value.sortColumn)
                            .Skip(value.start).Take(value.pageSize).ToList();
                    }
                }

                return data;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public dynamic GetLeaveRequestMasterById(Entities.LeaveRequestsMaster value)
        {
            var response = (from lr in _leaveRequest.Table
                            join em in _employeeMaster.Table on lr.ApproverId equals em.Id
                            join emp in _employeeMaster.Table on lr.RequestorId equals emp.Id
                            select new
                            {
                                RequestorId = lr.RequestorId,
                                ApproverId = lr.ApproverId,
                                FromDate = lr.FromDate,
                                FromDateLeaveType = lr.FromDateLeaveType,
                                ToDate = lr.ToDate,
                                ToDateLeaveType = lr.ToDateLeaveType,
                                LeaveDays = lr.LeaveDays,
                                ResonId = lr.ResonId,
                                Remark = lr.Remark,
                                RequestStatus = lr.RequestStatus,
                                FYIUsers = lr.FYIUsers,
                                //SortColumn = "",
                                //SortDirection = "",
                                //PageSize = 0,
                                //Start = 0,
                                //SearchText = "",
                                Approver = em.Name,
                                Requestor = emp.Name,
                                Reson = lr.Reson.LeavesReson,
                                Id = lr.Id,
                                //IsActive = lr.IsActive,
                                CreatedDate = lr.CreatedDate,
                                CreatedBy = lr.CreatedBy,
                                ModifiedDate = lr.ModifiedDate,
                                ModifiedBy = lr.ModifiedBy
                            }).Where(k => k.Id == value.Id);
            //var leaveRequest = _leaveRequest.GetById(value.Id);
            //data = response;
            return response;
        }
        public bool LeaveRequestMasterExists(long id)
        {
            return _leaveRequest.IsExist(id);
        }
        public dynamic InsertUpdateLeaveRequestMasterData(Entities.LeaveRequestsMaster value)
        {
            Entities.LeaveRequestsMaster data = new Entities.LeaveRequestsMaster();

            data.Id = value.Id;

            if (value.Id == 0) // Insert in DB
            {
                // join all fyi userIds to store in db
                List<string> fyi = value.FYIUsersList.Select(s => s.Value.ToString()).ToList();
                value.FYIUsers = string.Join(",", fyi);

                EmployeeMasterEntity requestor = _employeeMaster.Table.Where(k => k.IsActive == true && k.Id == value.RequestorId).FirstOrDefault();
                EmployeeMasterEntity approver = _employeeMaster.Table.Where(k => k.IsActive == true && k.Id == value.ApproverId).FirstOrDefault();
                string leaveReason = _leaveReason.Table.Where(k => k.IsActive == true && k.Id == value.ResonId).Select(s => s.LeavesReson).FirstOrDefault();

                // get all email from FYIUsers to CC
                List<string> fyiUsersList = new List<string>();
                string fyiUsersEmailIds = string.Empty;
                foreach (var item in value.FYIUsersList)
                {
                    string emailID = _employeeMaster.Table.Where(k => k.IsActive == true && k.Id == item.Value).Select(s => s.LoginId).FirstOrDefault();
                    fyiUsersList.Add(emailID);
                }
                fyiUsersEmailIds = string.Join(",", fyiUsersList);

                // email of leave request
                //Mail Start
                SendMailModel MailDetails = new SendMailModel();
                EmailTemplate template = new EmailTemplate();

                string TempBody = "<p>Dear " + approver.Name + ",</br>Following leave request is awaiting your approval in <a href=\"https://wow.azurewebsites.net/\">WoW</a> portal:</p></br><table><tr><td>Name : </td><td>" + requestor.Name + "</td></tr><tr><td>From : </td><td>" + value.FromDate.ToString("dd/MM/yyyy") + "</td></tr><tr><td>To : </td><td>" + value.ToDate.ToString("dd/MM/yyyy") + "</td></tr><tr><td>Days : </td><td>" + value.LeaveDays + "</td></tr><tr><td>Reason : </td><td>" + leaveReason + "</td></tr></table></br><p>Thanking you</p></br>(Note: This is a system generated message.)";
                string TempSub = "WoW Leave Request Intimation";
                template.Body = TempBody;
                template.Subject = TempSub;

                string TemplateBody = template.Body;
                string TemplateSubject = template.Subject;
                StringBuilder sbTemplateBody = new StringBuilder(TemplateBody);
                StringBuilder sbTemplateSubject = new StringBuilder(TemplateSubject);

                MailDetails.To = approver.LoginId;
                MailDetails.CC = fyiUsersEmailIds;
                MailDetails.Body = sbTemplateBody.ToString();
                MailDetails.Subject = sbTemplateSubject.ToString();
                var returnValue = _mailService.SendMail(MailDetails);

                //Mail End 

                if (returnValue) { 
                // Insert in DB
                Entities.LeaveRequestsMaster response = _leaveRequest.InsertAndGet(value);

                 //substract leave days from balance
                 EmployeeLeaveBalance employeeLeaveBalance = _employeeLeaveBalance.GetAll(b => b.Where(k => k.IsActive == true && k.EmployeeId == value.RequestorId)).SingleOrDefault();
                 employeeLeaveBalance.LeaveBalance = employeeLeaveBalance.LeaveBalance - value.LeaveDays;
                 _employeeLeaveBalance.UpdateAsNoTracking(employeeLeaveBalance);

                 return response.Id;
                }
            }
            else if (value.Id != 0)
            {
                Entities.LeaveRequestsMaster leaveReqGetById = _leaveRequest.GetById(value.Id);
                EmployeeMasterEntity requestor = _employeeMaster.Table.Where(k => k.IsActive == true && k.Id == leaveReqGetById.RequestorId).FirstOrDefault();
                EmployeeMasterEntity approver = _employeeMaster.Table.Where(k => k.IsActive == true && k.Id == leaveReqGetById.ApproverId).FirstOrDefault();

                // get all email from FYIUsers to CC
                List<int> fyiusers = new List<int>(Array.ConvertAll(leaveReqGetById.FYIUsers.Split(','), int.Parse));
                List<string> fyiUsersList = new List<string>();
                string fyiUsersEmailIds = string.Empty;
                foreach (var item in fyiusers)
                {
                    string emailID = _employeeMaster.Table.Where(k => k.IsActive == true && k.Id == item).Select(s => s.LoginId).FirstOrDefault();
                    fyiUsersList.Add(emailID);
                }
                fyiUsersEmailIds = string.Join(",", fyiUsersList);

                if (leaveReqGetById.Id != 0)
                {
                    if (value.RequestStatus == 1)
                    {
                        //leave approved
                        //email of leave approval
                        //Mail Start
                        SendMailModel MailDetails = new SendMailModel();
                        EmailTemplate template = new EmailTemplate();

                        string TempBody = "<p>Dear " + requestor.Name + ",</br>Your leave request for following days has been approved.</p></br><table><tr><td>From : </td><td>" + leaveReqGetById.FromDate.ToString("dd/MM/yyyy") + "</td></tr><tr><td>To : </td><td>" + leaveReqGetById.ToDate.ToString("dd/MM/yyyy") + "</td></tr><tr><td>Days : </td><td>" + leaveReqGetById.LeaveDays + "</td></tr></table></br><p>Thanking you,</br>" + approver.Name + "</p></br>(Note: This is a system generated message.)";
                        string TempSub = "WoW Leave request Approved";
                        template.Body = TempBody;
                        template.Subject = TempSub;

                        string TemplateBody = template.Body;
                        string TemplateSubject = template.Subject;
                        StringBuilder sbTemplateBody = new StringBuilder(TemplateBody);
                        StringBuilder sbTemplateSubject = new StringBuilder(TemplateSubject);

                        MailDetails.To = requestor.LoginId;
                        MailDetails.CC = fyiUsersEmailIds;
                        MailDetails.Body = sbTemplateBody.ToString();
                        MailDetails.Subject = sbTemplateSubject.ToString();
                        var returnValue = _mailService.SendMail(MailDetails);
                        //Mail End 

                        if (returnValue)
                        {
                            // update in db

                            leaveReqGetById.RequestStatus = value.RequestStatus;
                            _leaveRequest.UpdateAsNoTracking(leaveReqGetById);

                            //substract leave days from balance
                            //EmployeeLeaveBalance employeeLeaveBalance = _employeeLeaveBalance.GetAll(b => b.Where(k => k.IsActive == true && k.EmployeeId == leaveReqGetById.RequestorId)).SingleOrDefault();
                            //employeeLeaveBalance.LeaveBalance = employeeLeaveBalance.LeaveBalance - leaveReqGetById.LeaveDays;
                            //_employeeLeaveBalance.UpdateAsNoTracking(employeeLeaveBalance);
                        }

                    }
                    else if (value.RequestStatus == 2)
                    {
                        //leave rejected
                        //email of leave rejection
                        //Mail Start
                        SendMailModel MailDetails = new SendMailModel();
                        EmailTemplate template = new EmailTemplate();

                        string TempBody = "<p>Dear " + requestor.Name + ",</br>Your leave request for following days has been rejected.</p></br><table><tr><td>From : </td><td>" + leaveReqGetById.FromDate.ToString("dd/MM/yyyy") + "</td></tr><tr><td>To : </td><td>" + leaveReqGetById.ToDate.ToString("dd/MM/yyyy") + "</td></tr><tr><td>Days : </td><td>" + leaveReqGetById.LeaveDays + "</td></tr></table></br><p>Thanking you,</br>" + approver.Name + "</p></br>(Note: This is a system generated message.)";
                        string TempSub = "WoW Leave request Rejected";
                        template.Body = TempBody;
                        template.Subject = TempSub;

                        string TemplateBody = template.Body;
                        string TemplateSubject = template.Subject;
                        StringBuilder sbTemplateBody = new StringBuilder(TemplateBody);
                        StringBuilder sbTemplateSubject = new StringBuilder(TemplateSubject);

                        MailDetails.To = requestor.LoginId;
                        MailDetails.CC = fyiUsersEmailIds;
                        MailDetails.Body = sbTemplateBody.ToString();
                        MailDetails.Subject = sbTemplateSubject.ToString();
                        var returnValue = _mailService.SendMail(MailDetails);
                        //Mail End 

                        if (returnValue)
                        {
                            //update in db
                            leaveReqGetById.RequestStatus = value.RequestStatus;
                            _leaveRequest.UpdateAsNoTracking(leaveReqGetById);

                            //if leave rejected add leave days back in leave balance
                            EmployeeLeaveBalance employeeLeaveBalance = _employeeLeaveBalance.GetAll(b => b.Where(k => k.IsActive == true && k.EmployeeId == leaveReqGetById.RequestorId)).SingleOrDefault();
                            employeeLeaveBalance.LeaveBalance = employeeLeaveBalance.LeaveBalance + leaveReqGetById.LeaveDays;
                            _employeeLeaveBalance.UpdateAsNoTracking(employeeLeaveBalance);

                        }

                    }
                    else if (value.RequestStatus == 4)
                    {
                        //leave cancelled
                        //email of leave cancelled
                        //Mail Start
                        SendMailModel MailDetails = new SendMailModel();
                        EmailTemplate template = new EmailTemplate();

                        string TempBody = "<p>Dear " + approver.Name + ",</br>Following leave request has been cancelled.</p></br><table><tr><td>From : </td><td>" + leaveReqGetById.FromDate.ToString("dd/MM/yyyy") + "</td></tr><tr><td>To : </td><td>" + leaveReqGetById.ToDate.ToString("dd/MM/yyyy") + "</td></tr><tr><td>Days : </td><td>" + leaveReqGetById.LeaveDays + "</td></tr></table></br><p>Thanking you,</br>" + requestor.Name + "</p></br>(Note: This is a system generated message.)";
                        string TempSub = "WoW Leave request Cancelled";
                        template.Body = TempBody;
                        template.Subject = TempSub;

                        string TemplateBody = template.Body;
                        string TemplateSubject = template.Subject;
                        StringBuilder sbTemplateBody = new StringBuilder(TemplateBody);
                        StringBuilder sbTemplateSubject = new StringBuilder(TemplateSubject);

                        MailDetails.To = approver.LoginId;
                        MailDetails.CC = fyiUsersEmailIds;
                        MailDetails.Body = sbTemplateBody.ToString();
                        MailDetails.Subject = sbTemplateSubject.ToString();
                        var returnValue = _mailService.SendMail(MailDetails);
                        //Mail End 

                        if (returnValue) { 

                         //update in db
                        leaveReqGetById.RequestStatus = value.RequestStatus;
                        _leaveRequest.UpdateAsNoTracking(leaveReqGetById);

                        //if leave cancelled add leave days back in leave balance
                        EmployeeLeaveBalance employeeLeaveBalance = _employeeLeaveBalance.GetAll(b => b.Where(k => k.IsActive == true && k.EmployeeId == leaveReqGetById.RequestorId)).SingleOrDefault();
                        employeeLeaveBalance.LeaveBalance = employeeLeaveBalance.LeaveBalance + leaveReqGetById.LeaveDays;
                        _employeeLeaveBalance.UpdateAsNoTracking(employeeLeaveBalance);
                        }
                    }
                    return leaveReqGetById.Id;
                }
            }
            return 0;
        }
        #endregion
        public class LeaveRequestsMasterResponse
        {
            public int count { get; set; }
            public List<Entities.LeaveRequestsMaster> leaveRequestsMasterData { get; set; }
        }
    }
}
