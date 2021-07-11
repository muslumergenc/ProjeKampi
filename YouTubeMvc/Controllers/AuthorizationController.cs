using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.Utilities;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using EntityLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YouTubeMvc.Models;

namespace YouTubeMvc.Controllers
{
    [Authorize(Users ="muslum@mail.com")]
    public class AuthorizationController : Controller
    {
        readonly private IAuthService authService = new AuthManager(new AdminManager(new EfAdminDal()), new WriterManager(new EfWriterDal()));
        private AdminManager adminManager = new AdminManager(new EfAdminDal());
        private RoleManager roleManager = new RoleManager(new EfRoleDal());
        public ActionResult Index()
        {
            var adminValues = adminManager.GetList();
            return View(adminValues);
        }
        [HttpGet]
        public ActionResult AddAdmin()
        {
            List<SelectListItem> valueadminrole = (from c in roleManager.GetRoles()
                                                   select new SelectListItem
                                                   {
                                                       Text = c.RoleName,
                                                       Value = c.RoleId.ToString()

                                                   }).ToList();

            ViewBag.valueadmin = valueadminrole;
            return View();
        }
        [HttpPost]
        public ActionResult AddAdmin(LoginDto loginDto)
        {
            authService.Register(loginDto.AdminUserName, loginDto.AdminPassword, loginDto.AdminName, loginDto.RoleId);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult UpdateAdmin(int id)
        {
            List<SelectListItem> valueadminrole = (from c in roleManager.GetRoles()
                                                   select new SelectListItem
                                                   {
                                                       Text = c.RoleName,
                                                       Value = c.RoleId.ToString()

                                                   }).ToList();

            ViewBag.valueadmin = valueadminrole;
            var result = adminManager.GetById(id);
            return View(result);
        }
        [HttpPost]
        public ActionResult UpdateAdmin(Admin admin)
        {
            adminManager.AdminUpdate(admin);
            return RedirectToAction("Index");
        }
        public ActionResult StatusAdmin(int id)
        {
            var result = adminManager.GetById(id);
            if (result.AdminStatus == true)
            {
                result.AdminStatus = false;
            }
            else
            {
                result.AdminStatus = true;
            }
            adminManager.AdminUpdate(result);
            return RedirectToAction("Index");
        }
        public ActionResult AdminDelete(int id)
        {
            var adminvalues = adminManager.GetById(id);
            adminManager.AdminDelete(adminvalues);
            return RedirectToAction("Index");
        }
    }
}