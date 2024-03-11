using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EmployeeManagement.Controllers
{

    public class HomeController : Controller
    {
        private readonly MockEmployeeRepository _empRepo;

        public HomeController()
        {
            _empRepo = new MockEmployeeRepository();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet("Home/GetAll/{Id}")]
        //Using JsonResult and Routing to pass Jsondata to View
        public JsonResult GetAllEmployee(int Id)
        {
            try
            {
                if (Id == 0)
                {
                    var model = _empRepo.GetAllEmployee();
                    return Json(new { success = true, data = model });
                }
                else
                {
                    var model = _empRepo.GetEmployee(Id);
                    return Json(new { success = true, data = model });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    msg = ex.Message
                });
            }
        }

        public ActionResult Details(int Id)
        {
            ViewData["EmpId"] = Id;
            return View();
        }

        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return View(new Employee());
            }
            else
            {
                //get employee details
                var model = _empRepo.GetEmployee(Id.Value);
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult Edit(Employee employeeModel)
        {

            _empRepo.Update(employeeModel);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int Id)
        {
            _empRepo.Remove(Id);
            return RedirectToAction("Index");
        }


    }
}
