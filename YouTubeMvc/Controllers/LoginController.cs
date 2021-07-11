using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.Utilities;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using EntityLayer.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using YouTubeMvc.Models;

namespace YouTubeMvc.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        readonly private IAuthService authService = new AuthManager(new AdminManager(new EfAdminDal()), new WriterManager(new EfWriterDal()));
        readonly private AdminManager adm = new AdminManager(new EfAdminDal());
        readonly private WriterManager wm = new WriterManager(new EfWriterDal());
        [HttpGet]
        public ActionResult Admin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Admin(LoginDto loginDto)
        {
            var response = Request["g-recaptcha-response"];
            const string secret = "6Lfw6T8bAAAAAItuIShiVWQ5-4K-WhNS-m51WOtD";
            var client = new WebClient();
            var reply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));
            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResult>(reply);
            if (captchaResponse.Success && authService.Login(loginDto))
            {
                foreach (Admin item in adm.GetList())
                {
                    bool result = HashingHelper.VerifyAdminHash(loginDto.AdminUserName, loginDto.AdminPassword, item.AdminUserName, item.AdminPasswordHash, item.AdminPasswordSalt);
                    if (result == true)
                    {
                        if (item.AdminStatus==true)
                        {
                            FormsAuthentication.SetAuthCookie(loginDto.AdminUserName, false);
                            Session["AdminUserName"] = loginDto.AdminUserName;
                            return RedirectToAction("Index", "AdminCategory");
                        }
                        else
                        {
                            ViewData["ErrorMessage"] = "Bu Hesap Henüz Aktif Değil !";
                        }
                    }
                }
                return View();
            }
            else
            {
                ViewData["ErrorMessage"] = "Kullanıcı adı veya Parola yanlış";
                return View();
            }
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Admin", "Login");
        }
        [HttpGet]
        public ActionResult WriterLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult WriterLogin(WriterLoginDto writerLoginDto)
        {
            var response = Request["g-recaptcha-response"];
            const string secret = "6Lfw6T8bAAAAAItuIShiVWQ5-4K-WhNS-m51WOtD";
            var client = new WebClient();
            var reply =
                client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));
            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResult>(reply);

            if (authService.WriterLogin(writerLoginDto) && captchaResponse.Success)
            {
                var writerMail = wm.GetList().Where(x => x.WriterEmail == writerLoginDto.WriterEmail).FirstOrDefault();
                if (writerMail != null)
                {
                    bool result = HashingHelper.WriterVerifyPasswordHash(writerLoginDto.WriterPassword, writerMail.WriterPasswordHash, writerMail.WriterPasswordSalt);
                        if (result == true)
                        {
                            FormsAuthentication.SetAuthCookie(writerLoginDto.WriterEmail, false);
                            Session["WriterEmail"] = writerLoginDto.WriterEmail;
                            return RedirectToAction("Index", "WriterPanel");
                        }
                        else
                        {
                            ViewData["ErrorMessage"] = "Kullanıcı adı veya Parola yanlış";
                            return View();
                        }

                }
            }
            else
            {
                ViewData["ErrorMessage"] = "Kullanıcı adı veya Parola yanlış";
                return RedirectToAction("WriterLogin");
            }
            return View();
        }
        public ActionResult WriterLogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Headings", "Default");
        }
    }
}