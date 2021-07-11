using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace YouTubeMvc.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        IAuthService authService = new AuthManager(new AdminManager(new EfAdminDal()), new WriterManager(new EfWriterDal()));
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(WriterDto writerDto)
        {
            if (writerDto!=null)
            {
                authService.RegisterWriter(writerDto.WriterEmail, writerDto.WriterPassword, writerDto.WriterName, writerDto.WriterSurname);
                TempData["Writer"] = writerDto.WriterEmail;
                return View(writerDto);
            }
            return View();
           
        }
    }
}