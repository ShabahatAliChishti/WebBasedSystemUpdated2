using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBasedSystem.Models;
using WebBasedSystem.ViewModels;
using LinqToExcel;
using System.Data.Entity.Validation;

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
         public ActionResult ViewWayPoints()
        {

            return View();
        }

        public ActionResult AddWayPoints()
        {
            List<WayPointsModel> listEmp = db.WayPoints.Select(x => new WayPointsModel
            {
                Id = x.Id,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                Suburbs = x.Suburbs,
                PostCode = x.PostCode,
                State = x.State,

            }).ToList();
            ViewBag.EmployeeList = listEmp;

            return View();
        }



        [HttpPost]
        public ActionResult AddWayPoints(WayPointsModel model)
        {
            try
            {


                WayPoint emp = new WayPoint();
                emp.Latitude = model.Latitude;
                emp.Longitude = model.Longitude;
                emp.Suburbs = model.Suburbs;
                emp.PostCode = model.PostCode;
                emp.State = model.State;


                db.WayPoints.Add(emp);
                db.SaveChanges();

                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public JsonResult UploadExcelWayPoints(HttpPostedFileBase FileUpload)
        {

            List<string> data = new List<string>();
            if (FileUpload != null)
            {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");  
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {


                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/Doc/");
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();

                    adapter.Fill(ds, "ExcelTable");

                    DataTable dtable = ds.Tables["ExcelTable"];

                    string sheetName = "Sheet1";

                    var excelFile = new ExcelQueryFactory(pathToExcelFile);
                    var listCourses = from c in excelFile.Worksheet<WayPoint>(sheetName) select c;

                    //  BEGIN - Clearing up the existing data in the table before Inserting the records.
                    WebBaseSystemEntities cleanTableEntities = new WebBaseSystemEntities();
                    var objContext = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)cleanTableEntities).ObjectContext;
                    objContext.ExecuteStoreCommand("Truncate table WayPoints");
                    //  END
                    var list = db.WayPoints.ToList();
                    db.WayPoints.RemoveRange(list);
                    foreach (var c in listCourses)
                    {
                        try
                        {


                            //if (a.Name != "" && a.Address != "" && a.ContactNo != "")
                            if (c.PostCode != "")
                            {

                                //User TU = new User();
                                WayPoint TC = new WayPoint();
                                TC.Latitude = c.Latitude;
                                TC.Longitude = c.Longitude;
                                TC.PostCode = c.PostCode;
                                TC.Suburbs = c.Suburbs;
                                TC.State = c.State;
                              
                                db.SaveChanges();
                                db.WayPoints.Add(TC);
                                db.SaveChanges();
                            }
                            else
                            {
                                return Json(data, JsonRequestBehavior.AllowGet);
                            }
                        }

                        catch (DbEntityValidationException ex)
                        {

                        }
                    }
                    //deleting excel file from folder  
                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //alert message for invalid file format  
                    data.Add("<ul>");
                    data.Add("<li>Only Excel file format is allowed</li>");
                    data.Add("</ul>");
                    data.ToArray();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.Add("<ul>");
                if (FileUpload == null) data.Add("<li>Please choose Excel file</li>");
                data.Add("</ul>");
                data.ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }


    }
}