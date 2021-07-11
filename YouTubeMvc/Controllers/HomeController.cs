using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using YouTubeMvc.Models;

namespace YouTubeMvc.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult HomePage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult HomePage(ContactMailModel model)
        {
            if (ModelState.IsValid)
            {
                var body = new StringBuilder();
                body.AppendLine("Ad & Soyad: " + model.Ad);
                body.AppendLine("E-Mail Adresi: " + model.Email);
                body.AppendLine("Konu: " + model.Konu);
                body.AppendLine("Mesaj: " + model.Mesaj);
                MailSender(body.ToString());
                ViewBag.Success = true;
            }
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "";

            return View();
        }
        public static void MailSender(string body)
        {
            var fromAddress = new MailAddress("mail@mail.com.tr");
            var toAddress = new MailAddress("mail@mail.com.tr");
            const string subject = "MVC PROJE KAMPI | Sitenizden Gelen İletişim Formu";
            using (var smtp = new SmtpClient
            {
                Host = "smtp-mail.outlook.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, "mailsifre")
            })
            {
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject, 
                    Body = body 
                })
                {
                    smtp.Send(message);
                }
            }
        }
    }
}