using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using AddressBook.DAL;
using AddressBook.Areas.LOC_Country.Models;
using AddressBook.Areas.LOC_State.Models;
using AddressBook.BAL;

namespace AddressBook.Areas.LOC_State.Controllers
{
    [CheckAccess]
    [Area("LOC_State")]
    [Route("LOC_State/[controller]/[action]")]
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
            DataTable dt = dalloc.PR_LOC_State_SelectAll(str,Convert.ToInt32(HttpContext.Session.GetString("UserID")));
            return View("Index", dt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int StateID)
        {
            try
            {
                string str = Configuration.GetConnectionString("myConnectionString");
                LOC_DAL dalloc = new LOC_DAL();
                bool result = dalloc.PR_LOC_State_DeleteByID(str, StateID, Convert.ToInt32(HttpContext.Session.GetString("UserID")));
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
        public IActionResult Add(int? StateID)
        {
            #region Dropdown

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
            conn1.Close();

            #endregion

            if (StateID != null)
            {
                string str = Configuration.GetConnectionString("myConnectionString");
                LOC_DAL dalloc = new LOC_DAL();
                DataTable dt = dalloc.PR_LOC_State_SelectByID(str, StateID, Convert.ToInt32(HttpContext.Session.GetString("UserID")));

                LOC_StateModel modelLOC_State = new LOC_StateModel();

                foreach (DataRow dr in dt.Rows)
                {
                    modelLOC_State.StateID = Convert.ToInt32(dr["StateID"]);
                    modelLOC_State.StateName = dr["StateName"].ToString();
                    modelLOC_State.StateCode = dr["StateCode"].ToString();
                    modelLOC_State.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelLOC_State.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelLOC_State.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                }
                return View("LOC_State_InsertOrUpdate", modelLOC_State);
            }
            return View("LOC_State_InsertOrUpdate");
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(LOC_StateModel modelLOC_State)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            LOC_DAL dalloc = new LOC_DAL();
            bool result = false;

            if (modelLOC_State.StateID == null)
            {
                result = dalloc.PR_LOC_State_Insert(str, modelLOC_State.StateName, modelLOC_State.StateCode, modelLOC_State.CountryID, null, null,Convert.ToInt32(HttpContext.Session.GetString("UserID")));
            }
            else
            {
                result = dalloc.PR_LOC_State_UpdateByID(str, modelLOC_State.StateID, modelLOC_State.StateName, modelLOC_State.StateCode, modelLOC_State.CountryID, null,Convert.ToInt32(HttpContext.Session.GetString("UserID")));
            }

            if (result)
            {
                TempData["alertClass"] = "alert alert-success";
                TempData["alertDisplay"] = "show";
                if (modelLOC_State.StateID == null)
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

        public IActionResult SearchByPage(string CountryName, string CountryCode, string StateName, string StateCode)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_State_SelectByPage";
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
            cmd.Parameters.AddWithValue("@CountryName", CountryName);
            cmd.Parameters.AddWithValue("@CountryCode", CountryCode);
            cmd.Parameters.AddWithValue("@StateName", StateName);
            cmd.Parameters.AddWithValue("@StateCode", StateCode);
            cmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(HttpContext.Session.GetString("UserID")));
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            return View("Index", dt);
        }
        #endregion

    }
}
