using AddressBook.DAL;
using AddressBook.Models;
using AddressBook.BAL;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AddressBook.Controllers
{
    [CheckAccess]
    public class HomeController : Controller
    {
        #region Config
        private IConfiguration Configuration;
        public HomeController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #endregion

        public IActionResult Index()
        {

            #region Log IN 

            ViewBag.UserID = HttpContext.Session.GetString("UserID");
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.PhotoPath = HttpContext.Session.GetString("PhotoPath");

            #endregion

            #region Count of records on home page

            Home_DAL hmd = new Home_DAL();

            string str = this.Configuration.GetConnectionString("myConnectionString");

            ViewBag.Country = hmd.PR_LOC_Country_Row_Count(str, Convert.ToInt32(HttpContext.Session.GetString("UserID")));

            ViewBag.State = hmd.PR_LOC_State_Row_Count(str, Convert.ToInt32(HttpContext.Session.GetString("UserID")));

            ViewBag.City = hmd.PR_LOC_City_Row_Count(str, Convert.ToInt32(HttpContext.Session.GetString("UserID")));

            ViewBag.ContactCategory = hmd.PR_CON_ContactCategory_Row_Count(str, Convert.ToInt32(HttpContext.Session.GetString("UserID")));

            ViewBag.Contact = hmd.PR_MST_Contact_Row_Count(str, Convert.ToInt32(HttpContext.Session.GetString("UserID")));

            ViewBag.User = hmd.PR_MST_Contact_Row_Count(str);

            #endregion

            return View();
        }

    }
}