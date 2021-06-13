using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
namespace YouTubeMvc.Controllers
{
    public class LoginController : Controller
    {
        readonly private AdminManager manager = new AdminManager(new EfAdminDal());
        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Index(Admin admin)
        {
          var adminValues= manager.GetList().Where(x => x.AdminUserName == admin.AdminUserName && x.AdminPassword==admin.AdminPassword).FirstOrDefault();
            if (adminValues!=null)
            {
                FormsAuthentication.SetAuthCookie(admin.AdminUserName, false);
                Session["AdminUserName"] = adminValues.AdminUserName;
                return RedirectToAction("Index", "AdminCategory");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}