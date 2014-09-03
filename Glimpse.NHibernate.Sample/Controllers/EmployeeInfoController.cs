using System.Web.Mvc;
using Glimpse.NHibernate.Sample.Models;

namespace Glimpse.NHibernate.Sample.Controllers
{
    public class EmployeeInfoController : Controller
    {
        readonly EmployeeInfoDAL dal;

        public EmployeeInfoController()
        {
            dal = new EmployeeInfoDAL(); 
        }

        //
        // GET: /EmployeeInfo/

        public ActionResult Index()
        {
            var employees = dal.GetEmployees();
            return View(employees);
        }

        //
        // GET: /EmployeeInfo/Create

        public ActionResult Create()
        {
            var employeeInfo = new EmployeeInfo();
            return View(employeeInfo);
        }

        //
        // POST: /EmployeeInfo/Create

        [HttpPost]
        public ActionResult Create(EmployeeInfo employeeInfo)
        {
            try
            {
                dal.CreateEmployee(employeeInfo);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /EmployeeInfo/Edit/5

        public ActionResult Edit(int id)
        {
            var employeeInfo = dal.GetEmployeeById(id);
            return View(employeeInfo);
        }

        //
        // POST: /EmployeeInfo/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, EmployeeInfo employeeInfo)
        {
            try
            {
                dal.UpdateEmployee(employeeInfo);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /EmployeeInfo/Delete/5

        public ActionResult Delete(int id)
        {
            var employeeInfo = dal.GetEmployeeById(id);
            return View(employeeInfo);
        }

        //
        // POST: /EmployeeInfo/Delete/5

        [HttpPost]
        public ActionResult Delete(int id,FormCollection collection)
        {
            try
            {
                var employeeInfo = dal.GetEmployeeById(id);
                dal.DeleteEmployee(employeeInfo);   
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}