using AddressBook.Areas.CON_ContactCategory.Models;
using AddressBook.Areas.LOC_City.Models;
using AddressBook.Areas.LOC_Country.Models;
using AddressBook.Areas.LOC_State.Models;
using AddressBook.Areas.MST_Contact.Models;
using AddressBook.DAL;
using AddressBook.BAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace AddressBook.Areas.MST_Contact.Controllers
{
    [CheckAccess]
    [Area("MST_Contact")]
    [Route("MST_Contact/[controller]/[action]")]
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
            MST_DAL dalloc = new MST_DAL();
            DataTable dt = dalloc.PR_MST_Contact_SelectAll(str,Convert.ToInt32(HttpContext.Session.GetString("UserID")));
            return View("Index", dt);
        }
        #endregion

        #region Delete
        public IActionResult Delete(int ContactID)
        {
            try
            {
                string str = Configuration.GetConnectionString("myConnectionString");
                MST_DAL dalloc = new MST_DAL();
                bool result = dalloc.PR_MST_Contact_DeleteByID(str, ContactID,Convert.ToInt32(HttpContext.Session.GetString("UserID")));
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
            catch (Exception e)
            {
                TempData["alertClass"] = "alert alert-danger";
                TempData["alertDisplay"] = "show";
                TempData["alertTitle"] = "Warning";
                TempData["alertMessage"] = "You Can't Delete the record " + e;
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Add
        public IActionResult Add(int? ContactID)
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

            #region Dropdown For Contact Category

            string strC = Configuration.GetConnectionString("myConnectionString");
            SqlConnection connC = new SqlConnection(strC);
            connC.Open();
            SqlCommand cmdC = connC.CreateCommand();
            cmdC.CommandType = CommandType.StoredProcedure;
            cmdC.CommandText = "PR_CON_ContactCategory_Dropdown";
            cmdC.Parameters.AddWithValue("@UserID", Convert.ToInt32(HttpContext.Session.GetString("UserID")));
            DataTable dtC = new DataTable();
            SqlDataReader drC = cmdC.ExecuteReader();
            dtC.Load(drC);
            connC.Close();

            List<CON_ContactCategoryDropdownModel> CON_ContactCategoryDropdown_List = new List<CON_ContactCategoryDropdownModel>();

            foreach (DataRow dr in dtC.Rows)
            {
                CON_ContactCategoryDropdownModel tempObj = new CON_ContactCategoryDropdownModel();
                tempObj.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                tempObj.ContactCategoryName = dr["ContactCategoryName"].ToString();
                CON_ContactCategoryDropdown_List.Add(tempObj);
            }
            ViewBag.ContactCategoryList = CON_ContactCategoryDropdown_List;

            #endregion

            #region Dropdown For State 

            List<LOC_StateDropdownModel> LOC_StateDropdown_List = new List<LOC_StateDropdownModel>();
            ViewBag.StateList = LOC_StateDropdown_List;

            #endregion

            #region Dropdown For City 

            List<LOC_CityDropdownModel> LOC_CityDropdown_List = new List<LOC_CityDropdownModel>();
            ViewBag.CityList = LOC_CityDropdown_List;

            #endregion

            #region Initial Value For Edit


            if (ContactID != null)
            {
                string str = Configuration.GetConnectionString("myConnectionString");
                MST_DAL dalloc = new MST_DAL();
                DataTable dt = dalloc.PR_MST_Contact_SelectByID(str, ContactID, Convert.ToInt32(HttpContext.Session.GetString("UserID")));

                MST_ContactModel modelMST_Contact = new MST_ContactModel();

                modelMST_Contact.CountryID = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    StateDropdownByCountry(Convert.ToInt32(dr["CountryID"]));
                    CityDropdownByState(Convert.ToInt32(dr["StateID"]));
                    modelMST_Contact.ContactID = Convert.ToInt32(dr["ContactID"]);
                    modelMST_Contact.ContactName = dr["ContactName"].ToString();
                    modelMST_Contact.ContactMobileNumber = dr["ContactMobileNumber"].ToString();
                    modelMST_Contact.ContactAddress = dr["ContactAddress"].ToString();
                    modelMST_Contact.ContactEmail = dr["ContactEmail"].ToString();
                    modelMST_Contact.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelMST_Contact.StateID = Convert.ToInt32(dr["StateID"]);
                    modelMST_Contact.CityID = Convert.ToInt32(dr["CityID"]);
                    modelMST_Contact.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                    modelMST_Contact.PhotoPath = dr["PhotoPath"].ToString();
                    ViewBag.EditImageURL = Convert.ToString(dr["PhotoPath"]);
                    modelMST_Contact.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelMST_Contact.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                }
                return View("MST_Contact_InsertOrUpdate", modelMST_Contact);
            }
            return View("MST_Contact_InsertOrUpdate");
        }


        #endregion

        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(MST_ContactModel modelMST_Contact)
        {

            if (modelMST_Contact.File != null)
            {
                string FilePath = "wwwroot\\UploadPhoto";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string fileNameWithPath = Path.Combine(path, modelMST_Contact.File.FileName);

                modelMST_Contact.PhotoPath = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + modelMST_Contact.File.FileName;

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    modelMST_Contact.File.CopyTo(stream);
                }

            }

            string str = Configuration.GetConnectionString("myConnectionString");

            MST_DAL dalloc = new MST_DAL();
            bool result = false;

            if (modelMST_Contact.ContactID == null)
            {
                result = dalloc.PR_MST_Contact_Insert(str, modelMST_Contact.ContactName, modelMST_Contact.ContactMobileNumber, modelMST_Contact.ContactAddress, modelMST_Contact.ContactEmail, modelMST_Contact.CountryID, modelMST_Contact.StateID, modelMST_Contact.CityID, modelMST_Contact.ContactCategoryID, modelMST_Contact.PhotoPath, null, null,Convert.ToInt32(HttpContext.Session.GetString("UserID")));
            }
            else
            {
                result = dalloc.PR_MST_Contact_UpdateByID(str, modelMST_Contact.ContactID, modelMST_Contact.ContactName, modelMST_Contact.ContactMobileNumber, modelMST_Contact.ContactAddress, modelMST_Contact.ContactEmail, modelMST_Contact.CountryID, modelMST_Contact.StateID, modelMST_Contact.CityID, modelMST_Contact.ContactCategoryID, modelMST_Contact.PhotoPath, null, Convert.ToInt32(HttpContext.Session.GetString("UserID")));
            }

            if (result)
            {
                TempData["alertClass"] = "alert alert-success";
                TempData["alertDisplay"] = "show";
                if (modelMST_Contact.ContactID == null)
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

        #region CasecadeForCityName
        public IActionResult CityDropdownByState(int StateID)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_City_DropdownByState";
            cmd.Parameters.Add("@StateID", SqlDbType.Int).Value = StateID;
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            DataTable dt = new DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            conn.Close();

            List<LOC_CityDropdownModel> LOC_CityDropdownByState_List = new List<LOC_CityDropdownModel>();

            foreach (DataRow dr in dt.Rows)
            {
                LOC_CityDropdownModel tempObj = new LOC_CityDropdownModel();
                tempObj.CityID = Convert.ToInt32(dr["CityID"]);
                tempObj.CityName = dr["CityName"].ToString();
                LOC_CityDropdownByState_List.Add(tempObj);
            }
            ViewBag.CityList = LOC_CityDropdownByState_List;
            var casecade = LOC_CityDropdownByState_List;

            return Json(casecade);
        }
        #endregion

        #region Search By Page

        public IActionResult SearchByPage(string CountryName, string CountryCode, string StateName, string StateCode, string CityName, string CityCode, string ContactCategoryName, string ContactName)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_MST_Contact_SelectByPage";
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
            if (ContactCategoryName == null)
            {
                ContactCategoryName = "";

            }
            if (ContactName == null)
            {
                ContactName = "";

            }
            cmd.Parameters.AddWithValue("@CountryName", CountryName);
            cmd.Parameters.AddWithValue("@CountryCode", CountryCode);
            cmd.Parameters.AddWithValue("@StateName", StateName);
            cmd.Parameters.AddWithValue("@StateCode", StateCode);
            cmd.Parameters.AddWithValue("@CityName", CityName);
            cmd.Parameters.AddWithValue("@CityCode", CityCode);
            cmd.Parameters.AddWithValue("@ContactCategoryName", ContactCategoryName);
            cmd.Parameters.AddWithValue("@ContactName", ContactName);
            cmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(HttpContext.Session.GetString("UserID")));
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            return View("Index", dt);
        }
        #endregion
    }
}
