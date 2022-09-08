using Microsoft.AspNetCore.Mvc;
using Prosares.Wow.Data.Entities;
using Prosares.Wow.Data.Models;
using Prosares.Wow.Data.Repository;
using Prosares.Wow.Data.Services.Employee;
using System.Linq;

namespace WowLoginModule.Controllers
{
    public class ChangePasswordController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public ChangePasswordController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChangePassword(EmployeeMasterEntity? obj)
        {
            obj.Eid = 100;

            bool check = _employeeService.CheckFirstLogin(obj);
            if (check)
            {
                obj.FirstLogin = true;
            }
            return View(obj);
        }


       [HttpPost]
       public bool ChangePasswordResponse(EmployeeMasterEntity emp)
       {

            emp.Eid = 100;
           
            bool data = _employeeService.ChangeEmployeePasswordByEmployeeId(emp);
            if (data)
            {
                return true;
            }

            return false;
        }

        [HttpPost]
        public bool CheckCurrentPassword(EmployeeMasterEntity emp)
        {
            emp.Eid = 100;
            bool check = _employeeService.CheckCurrentPasswordByEmployeeId(emp);
            return check;
        }

     }
}
