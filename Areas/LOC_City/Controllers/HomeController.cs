using AddressBook.Areas.LOC_City.Models;
using AddressBook.Areas.LOC_Country.Models;
using AddressBook.Areas.LOC_State.Models;
using AddressBook.DAL;
using AddressBook.BAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace AddressBook.Areas.LOC_City.Controllers
{
    [CheckAccess]
    [Area("LOC_City")]
    [Route("LOC_City/[controller]/[action]")]
    public class HomeController : Controller
    {
        #region Config
        private IConfiguration Configuration;
        public HomeController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #endregion

        #region Select All
        public IActionResult Index()
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            LOC_DAL dalloc = new LOC_DAL();
            DataTable dt = dalloc.PR_LOC_City_SelectAll(str,Convert.ToInt32(HttpContext.Session.GetString("UserID")));
            return View("Index", dt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int CityID)
        {
            try
            {
                string str = Configuration.GetConnectionString("myConnectionString");
                LOC_DAL dalloc = new LOC_DAL();
                bool result = dalloc.PR_LOC_City_DeleteByID(str, CityID,Convert.ToInt32(HttpContext.Session.GetString("UserID")));
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
        public IActionResult Add(int? CityID)
        {
            #region Dropdown For Country
            string str1 = Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn1 = new SqlConnection(str1);
            conn1.Open();
            SqlCommand cmd1 = conn1.CreateCommand();
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "PR_LOC_Country_Dropdown";
            cmd1.Parameters.AddWithValue("@UserID", Convert.ToInt32(HttpContext.Session.GetString("UserID")));
            DataTable dt1 = new DataTable();
            SqlDataReader dr1 = cmd1.ExecuteReader();
            dt1.Load(dr1);

            List<LOC_CountryDropdownModel> LOC_CountryDropdown_List = new List<LOC_CountryDropdownModel>();

            foreach (DataRow dr in dt1.Rows)
            {
                LOC_CountryDropdownModel tempObj = new LOC_CountryDropdownModel();
                tempObj.CountryID = Convert.ToInt32(dr["CountryId"]);
                tempObj.CountryName = dr["CountryName"].ToString();
                LOC_CountryDropdown_List.Add(tempObj);
            }
            ViewBag.CountryList = LOC_CountryDropdown_List;
            #endregion

            #region Dropdown For State

            List<LOC_StateDropdownModel> LOC_StateDropdown_List = new List<LOC_StateDropdownModel>();
            ViewBag.StateList = LOC_StateDropdown_List;

            #endregion

            if (CityID != null)
            {
                string str = Configuration.GetConnectionString("myConnectionString");
                LOC_DAL dalloc = new LOC_DAL();
                DataTable dt = dalloc.PR_LOC_City_SelectByID(str, CityID, Convert.ToInt32(HttpContext.Session.GetString("UserID")));

                LOC_CityModel modelLOC_City = new LOC_CityModel();

                foreach (DataRow dr in dt.Rows)
                {
                    StateDropdownByCountry(Convert.ToInt32(dr["CountryID"]));
                    modelLOC_City.CityID = Convert.ToInt32(dr["CityID"]);
                    modelLOC_City.CityName = dr["CityName"].ToString();
                    modelLOC_City.CityCode = dr["CityCode"].ToString();
                    modelLOC_City.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelLOC_City.StateID = Convert.ToInt32(dr["StateID"]);
                    modelLOC_City.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelLOC_City.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                }
                return View("LOC_City_InsertOrUpdate", modelLOC_City);
            }
            return View("LOC_City_InsertOrUpdate");
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(LOC_CityModel modelLOC_City)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            LOC_DAL dalloc = new LOC_DAL();
            bool result = false;

            if (modelLOC_City.CityID == null)
            {
                result = dalloc.PR_LOC_City_Insert(str, modelLOC_City.CityName, modelLOC_City.CityCode, modelLOC_City.CountryID, modelLOC_City.StateID, null, null,Convert.ToInt32(HttpContext.Session.GetString("UserID")));
            }
            else
            {
                result = dalloc.PR_LOC_City_UpdateByID(str, modelLOC_City.CityID, modelLOC_City.CityName, modelLOC_City.CityCode, modelLOC_City.CountryID, modelLOC_City.StateID, null, Convert.ToInt32(HttpContext.Session.GetString("UserID")));
            }

            if (result)
            {
                TempData["alertClass"] = "alert alert-success";
                TempData["alertDisplay"] = "show";
                if (modelLOC_City.CityID == null)
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

        #region CasecadeForStateName
        public IActionResult StateDropdownByCountry(int CountryID)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_State_DropdownByCountry";
            cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = CountryID;
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            DataTable dt = new DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            conn.Close();

            List<LOC_StateDropdownModel> LOC_StateDropdownByCountry_List = new List<LOC_StateDropdownModel>();

            foreach (DataRow dr in dt.Rows)
            { 
                LOC_StateDropdownModel tempObj = new LOC_StateDropdownModel();
                tempObj.StateID = Convert.ToInt32(dr["StateID"]);
                tempObj.StateName = dr["StateName"].ToString();
                LOC_StateDropdownByCountry_List.Add(tempObj);
            }

            var casecade = LOC_StateDropdownByCountry_List;

            ViewBag.StateList = LOC_StateDropdownByCountry_List;

            return Json(casecade);
        }
        #endregion

        #region Search By Page

        public IActionResult SearchByPage(string CountryName, string CountryCode, string StateName, string StateCode, string CityName, string CityCode)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_City_SelectByPage";
            if (CountryName == null)
            {
                CountryName = "";

            }
            if (CountryCode == null)
            {
                CountryCode = "";

            }
            if (StateName == null)
            {
                StateName = "";

            }
            if (StateCode == null)
            {
                StateCode = "";

            }
            if (CityName == null)
            {
                CityName = "";

            }
            if (CityCode == null)
            {
                CityCode = "";

            }
            cmd.Parameters.AddWithValue("@CountryName", CountryName);
            cmd.Parameters.AddWithValue("@CountryCode", CountryCode);
            cmd.Parameters.AddWithValue("@StateName", StateName);
            cmd.Parameters.AddWithValue("@StateCode", StateCode);
            cmd.Parameters.AddWithValue("@CityName", CityName);
            cmd.Parameters.AddWithValue("@CityCode", CityCode);
            cmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(HttpContext.Session.GetString("UserID")));
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            return View("Index", dt);
        }
        #endregion
    }
}
