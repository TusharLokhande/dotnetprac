using Prosares.Wow.Utility.Entities;
using Prosares.Wow.Utility.Repository;
using Prosares.Wow.Utility.Services.Mail;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prosares.Wow.Utility.Services.EmployeeDiscripency
{
    public class EmployeeDiscripency: IEmployeeDiscripency
    {
        private readonly IRepository<Entities.TasksTimeSheet> _timesheet;
        private readonly IMailService _mailService;

        public EmployeeDiscripency(IRepository<TasksTimeSheet> timesheet, IMailService mailService)
        {
            _timesheet = timesheet;
            _mailService = mailService;
        }

        public void Mail(string body, string email, string subject)
        {
            SendMailModel MailDetails = new SendMailModel();
            EmailTemplate template = new EmailTemplate();



            string TempBody = body;
            string TempSub = subject;
            template.Body = TempBody;
            template.Subject = TempSub;

            string TemplateBody = template.Body;
            string TemplateSubject = template.Subject;
            StringBuilder sbTemplateBody = new StringBuilder(TemplateBody);
            StringBuilder sbTemplateSubject = new StringBuilder(TemplateSubject);


            MailDetails.To = email;  //mail

            MailDetails.Body = sbTemplateBody.ToString();
            MailDetails.Subject = sbTemplateSubject.ToString();
            var returnValue = _mailService.SendMail(MailDetails);
        }


        public void  Process()
        {
            SqlCommand command = new SqlCommand("stp_EmployeeDiscripency");
            command.CommandType = CommandType.StoredProcedure;
            var data = _timesheet.GetRecords(command);
            SendMailModel MailDetails = new SendMailModel();

            string body = $"Your Hours Less than 8.5 hours";
            string subject = $"Discripency";

            foreach (var x in data)
            {
                string email = x.ToString();
                Mail(body,email,subject);
            }
            
            
        }

    }

    
}
