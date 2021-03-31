using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBasedSystem.Models;
using WebBasedSystem.ViewModels;

namespace WebBasedSystem.Controllers
{
    public class AdminController : Controller
    {
        WebBaseSystemEntities db = new WebBaseSystemEntities();
        // GET: AdminDashboard
        public ActionResult Dashboard()
        {
            return View();
        }


        public ActionResult AddSpecialistType()
        {
            List<SpecialistTypeModel> listEmp = db.SpecialistTypes.Select(x => new SpecialistTypeModel
            {
                SpecialistTypeId = x.SpecialistTypeId,
                SpecialistTypeName = x.SpecialistTypeName,

            }).ToList();
            ViewBag.EmployeeList = listEmp;

            return View();
        }
   


        [HttpPost]
        public ActionResult AddSpecialistType(SpecialistTypeModel model)
        {
            try
            {


                SpecialistType emp = new SpecialistType();
                emp.SpecialistTypeName = model.SpecialistTypeName;
     

                db.SpecialistTypes.Add(emp);
                db.SaveChanges();

                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public JsonResult Get(int SpecialistTypeId)
        {
            SpecialistType model = db.SpecialistTypes.Where(x => x.SpecialistTypeId == SpecialistTypeId).SingleOrDefault();

            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(SpecialistType employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(employee, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteEmployee(int SpecialistTypeId)
        {
            bool result = false;
            var emp = db.SpecialistTypes.SingleOrDefault(x => x.SpecialistTypeId == SpecialistTypeId);
            if (emp != null)
            {
                //emp.IsDeleted = true;
                db.SpecialistTypes.Remove(emp);

                db.SaveChanges();
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddSpecialist()
        {
            return View();
        }
    }
}