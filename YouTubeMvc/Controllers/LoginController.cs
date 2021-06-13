using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using YouTubeMvc.Models;

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
            string sifre1 = Sifrele.MD5Olustur(admin.AdminPassword);
            var adminValues = manager.GetList().Where(x => x.AdminUserName == admin.AdminUserName && x.AdminPassword == sifre1).FirstOrDefault();
            if (adminValues != null)
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