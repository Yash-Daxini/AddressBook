using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using AddressBook.DAL;
using AddressBook.Areas.LOC_Country.Models;
using AddressBook.BAL;

namespace AddressBook.Areas.LOC_Country.Controllers
{
    [CheckAccess]
    [Area("LOC_Country")]
    [Route("LOC_Country/[controller]/[action]")]
    public class HomeController : Controller
    {
        #region Config
        private IConfiguration Configuration;
        public HomeController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #endregion

        #region Index SelectAll
        public IActionResult Index()
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            LOC_DAL dalloc = new LOC_DAL();
            DataTable dt = dalloc.PR_LOC_Country_SelectAll(str , Convert.ToInt32(HttpContext.Session.GetString("UserID")));
            return View("Index", dt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int CountryID)
        {
            try
            {
                string str = Configuration.GetConnectionString("myConnectionString");
                LOC_DAL dalloc = new LOC_DAL();
                bool result = dalloc.PR_LOC_Country_DeleteByID(str, CountryID , Convert.ToInt32(HttpContext.Session.GetString("UserID")));
                if (result)
                {
                    TempData["alertClass"] = "alert alert-success";
                    TempData["alertDisplay"] = "show";
                    TempData["alertTitle"] = "Deleted";
                    TempData["alertMessage"] = "Data Deleted Successfully";
                }
                else
                {
                    TempData["alertDisplay"] = "show";
                    TempData["alertClass"] = "alert alert-danger";
                    TempData["alertTitle"] = "Error";
                    TempData["alertMessage"] = "Some Error Occured";
                }
            }
            catch (Exception ex)
            {
                TempData["alertClass"] = "alert alert-danger";
                TempData["alertDisplay"] = "show";
                TempData["alertTitle"] = "Warning";
                TempData["alertMessage"] = "You Can't Delete the record";
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Add
        public IActionResult Add(int? CountryID)
        {
            if (CountryID != null)
            {
                string str = Configuration.GetConnectionString("myConnectionString");
                LOC_DAL dalloc = new LOC_DAL();
                DataTable dt = dalloc.PR_LOC_Country_SelectByID( str, CountryID , Convert.ToInt32(HttpContext.Session.GetString("UserID")));

                LOC_CountryModel modelLOC_Country = new LOC_CountryModel();

                foreach (DataRow dr in dt.Rows)
                {
                    modelLOC_Country.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelLOC_Country.CountryName = dr["CountryName"].ToString();
                    modelLOC_Country.CountryCode = dr["CountryCode"].ToString();
                    modelLOC_Country.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelLOC_Country.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                }
                return View("LOC_Country_InsertOrUpdate", modelLOC_Country);
            }
            return View("LOC_Country_InsertOrUpdate");
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(LOC_CountryModel modelLOC_Country)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            LOC_DAL dalloc = new LOC_DAL();
            bool result = false;
            if (modelLOC_Country.CountryID == null)
            {
                result = dalloc.PR_LOC_Country_Insert(str, modelLOC_Country.CountryName, modelLOC_Country.CountryCode, null, null , Convert.ToInt32(HttpContext.Session.GetString("UserID")));
            }
            else
            {
                result = dalloc.PR_LOC_Country_UpdateByID(str, modelLOC_Country.CountryID, modelLOC_Country.CountryName, modelLOC_Country.CountryCode, null , Convert.ToInt32(HttpContext.Session.GetString("UserID")));
            }

            if (result)
            {
                TempData["alertClass"] = "alert alert-success";
                TempData["alertDisplay"] = "show";
                if (modelLOC_Country.CountryID == null)
                {
                    TempData["alertTitle"] = "Inserted";
                    TempData["alertMessage"] = "Data Inserted Successfully";
                }
                else
                {
                    TempData["alertTitle"] = "Updated";
                    TempData["alertMessage"] = "Data Updated Successfully";
                }
            }
            else
            {
                TempData["alertDisplay"] = "show";
                TempData["alertClass"] = "alert alert-danger";
                TempData["alertTitle"] = "Error";
                TempData["alertMessage"] = "Some Error Occured";
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Search By Page

        public IActionResult SearchByPage(string CountryName, string CountryCode)
        {
            //TempData["SearchInput"] = CountryName + " " + CountryCode;

            string str = Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_Country_SelectByPage";
            if (CountryName == null)
            {
                CountryName = "";

            }
            if (CountryCode == null)
            {
                CountryCode = "";

            }
            cmd.Parameters.AddWithValue("@CountryName", CountryName);
            cmd.Parameters.AddWithValue("@CountryCode", CountryCode);
            cmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(HttpContext.Session.GetString("UserID")));
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            return View("Index", dt);
        }
        #endregion

    }
}
