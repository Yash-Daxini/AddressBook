using AddressBook.DAL;
using AddressBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Xml.Linq;

namespace AddressBook.Controllers
{
    public class SEC_UserController : Controller
    {

        #region Config

        private IConfiguration Configuration;
        public SEC_UserController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        #endregion

        #region Index

        public IActionResult Index()
        {
            SEC_DAL secUserPhotoPath = new SEC_DAL();

            //string connstr = this.Configuration.GetConnectionString("myConnectionString");

            //ViewBag.PhotoPath = secUserPhotoPath.PR_SEC_User_SelectPhotoPathByPK(connstr, Convert.ToInt32(HttpContext.Session.GetString("UserID")));
            return View();
        }

        #endregion

        #region Login

        [HttpPost]
        public IActionResult Login(SEC_UserModel modelSEC_User)
        {
            string connstr = this.Configuration.GetConnectionString("myConnectionString");
            string error = null;
            if (modelSEC_User.UserName == null)
            {
                error += "User Name is required";
            }
            if (modelSEC_User.Password == null)
            {
                error += "<br/>Password is required";
            }

            if (error != null)
            {
                TempData["Error"] = error;
                return RedirectToAction("Index");
            }
            else
            {
                SEC_DAL dal = new SEC_DAL();
                DataTable dt = dal.dbo_PR_SEC_User_SelectByUserNamePassword(connstr, modelSEC_User.UserName, modelSEC_User.Password);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        HttpContext.Session.SetString("UserName", dr["UserName"].ToString());
                        HttpContext.Session.SetString("UserID", dr["UserID"].ToString());
                        HttpContext.Session.SetString("Password", dr["Password"].ToString());
                        HttpContext.Session.SetString("FirstName", dr["FirstName"].ToString());
                        HttpContext.Session.SetString("LastName", dr["LastName"].ToString());
                        HttpContext.Session.SetString("PhotoPath", dr["PhotoPath"].ToString());
                        break;
                    }
                }
                else
                {
                    TempData["Error"] = "User Name or Password is invalid!";
                    return RedirectToAction("Index");
                }
                if (HttpContext.Session.GetString("UserName") != null && HttpContext.Session.GetString("Password") != null)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Go to Sign Up Page

        public IActionResult SignUp()
        {
            return View("SEC_UserSignUp");
        }

        #endregion

        #region Save Or Sign Up User
        public IActionResult Save(SEC_UserModel modelSEC_User)
        {

            if (modelSEC_User.File != null)
            {
                string FilePath = "wwwroot\\UploadPhoto";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string fileNameWithPath = Path.Combine(path, modelSEC_User.File.FileName);

                modelSEC_User.PhotoPath = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + modelSEC_User.File.FileName;

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    modelSEC_User.File.CopyTo(stream);
                }

            }

            string str = Configuration.GetConnectionString("myConnectionString");

            SEC_DAL dalloc = new SEC_DAL();

            decimal? result;

            result = dalloc.dbo_PR_SEC_User_Insert(str, modelSEC_User.UserName, modelSEC_User.Password, modelSEC_User.FirstName,modelSEC_User.LastName, modelSEC_User.EmailAddress, modelSEC_User.PhotoPath, null, null);

            //if (result)
            //{
            //    TempData["alertClass"] = "alert alert-success";
            //    TempData["alertDisplay"] = "show";
            //    TempData["alertTitle"] = "Inserted";
            //    TempData["alertMessage"] = "Data Inserted Successfully";
            //}
            //else
            //{
            //    TempData["alertDisplay"] = "show";
            //    TempData["alertClass"] = "alert alert-danger";
            //    TempData["alertTitle"] = "Error";
            //    TempData["alertMessage"] = "Some Error Occured";
            //}

            return RedirectToAction("Index");
        }

        #endregion
         
        #region Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        #endregion
    }
}
