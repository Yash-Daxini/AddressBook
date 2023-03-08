using AddressBook.Areas.CON_ContactCategory.Models;
using AddressBook.DAL;
using AddressBook.BAL;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace AddressBook.Areas.CON_ContactCategory.Controllers
{
    [CheckAccess]
    [Area("CON_ContactCategory")]
    [Route("CON_ContactCategory/[controller]/[action]")]
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
            CON_DAL dalloc = new CON_DAL();
            DataTable dt = dalloc.PR_CON_ContactCategory_SelectAll(str,Convert.ToInt32(HttpContext.Session.GetString("UserID")));
            ViewBag.PhotoPath = HttpContext.Session.GetString("PhotoPath");
            return View("Index", dt);
        }

        #endregion

        #region Delete
        public IActionResult Delete(int ContactCategoryID)
        {
            try
            {
                string str = Configuration.GetConnectionString("myConnectionString");
                CON_DAL dalloc = new CON_DAL();
                bool result = dalloc.PR_CON_ContactCategory_DeleteByID(str, ContactCategoryID,Convert.ToInt32(HttpContext.Session.GetString("UserID")));
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
                TempData["alertMessage"] = "You Can't Delete the record";
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Add
        public IActionResult Add(int? ContactCategoryID)
        {
            if (ContactCategoryID != null)
            {
                string str = Configuration.GetConnectionString("myConnectionString");
                CON_DAL dalloc = new CON_DAL();
                DataTable dt = dalloc.PR_CON_ContactCategory_SelectByID(str, ContactCategoryID,Convert.ToInt32(HttpContext.Session.GetString("UserID")));

                CON_ContactCategoryModel modelCON_ContactCategory = new CON_ContactCategoryModel();

                foreach (DataRow dr in dt.Rows)
                {
                    modelCON_ContactCategory.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                    modelCON_ContactCategory.ContactCategoryName = dr["ContactCategoryName"].ToString();
                    modelCON_ContactCategory.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelCON_ContactCategory.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                }
                return View("CON_ContactCategory_InsertOrUpdate", modelCON_ContactCategory);
            }
            return View("CON_ContactCategory_InsertOrUpdate");
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(CON_ContactCategoryModel modelCON_ContactCategory)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            CON_DAL dalloc = new CON_DAL();
            bool result = false;

            if (modelCON_ContactCategory.ContactCategoryID == null)
            {
                result = dalloc.PR_CON_ContactCategory_Insert(str, modelCON_ContactCategory.ContactCategoryName, null, null,Convert.ToInt32(HttpContext.Session.GetString("UserID")));
            }
            else
            {
                result = dalloc.PR_CON_ContactCategory_UpdateByID(str, modelCON_ContactCategory.ContactCategoryID, modelCON_ContactCategory.ContactCategoryName, null,Convert.ToInt32(HttpContext.Session.GetString("UserID")));
            }

            if (result)
            {
                TempData["alertClass"] = "alert alert-success";
                TempData["alertDisplay"] = "show";
                if (modelCON_ContactCategory.ContactCategoryID == null)
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

        public IActionResult SearchByPage(string ContactCategoryName)
        {
            string str = Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_CON_ContactCategory_SelectByPage";

            if (ContactCategoryName == null)
            {
                ContactCategoryName = "";

            }
            cmd.Parameters.AddWithValue("@CategoryName", ContactCategoryName);
            cmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(HttpContext.Session.GetString("UserID")));
            DataTable dt = new DataTable();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            return View("Index", dt);
        }
        #endregion
    }
}
