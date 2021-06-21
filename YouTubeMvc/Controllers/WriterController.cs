using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.Utilities;
using BusinessLayer.ValidationRules_FluentValidation;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using EntityLayer.Dtos;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YouTubeMvc.Controllers
{
    public class WriterController : Controller
    {
        readonly private WriterManager wm = new WriterManager(new EfWriterDal());
        readonly WriterValidator writervalidator = new WriterValidator();
        [Authorize]
        public ActionResult Index()
        {
            var WriterValues = wm.GetList();
            if (WriterValues==null)
            {
                return View();
            }
            else
            {
                return View(WriterValues);
            }
           
        }

        [HttpGet]
        public ActionResult AddWriter()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddWriter(WriterEditDto editDto)
        {
            IAuthService authService = new AuthManager(new AdminManager(new EfAdminDal()), new WriterManager(new EfWriterDal()));
            authService.WriterAdd(editDto);
            return View("Index");
        }
        //[HttpPost]
        //public ActionResult AddWriter(Writer p)
        //{
        //    IAuthService authService = new AuthManager(new AdminManager(new EfAdminDal()), new WriterManager(new EfWriterDal()));
        //   ValidationResult results = writervalidator.Validate(p);
        //   if (results.IsValid)
        //   {

        //    wm.WriterAdd(p);
        //    return RedirectToAction("Index");
        //   }
        //   else
        //   {
        //   foreach (var item in results.Errors)
        //   {
        //        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
        //      }
        //   }
        //   return View();
        //}

        [HttpGet]
        public ActionResult EditWriter(int id)
        {
            WriterEditDto editDto = new WriterEditDto
            {
                WriterID = id,
                WriterEmail=wm.GetById(id).WriterEmail,
                WriterName= wm.GetById(id).WriterName,
                WriterSurname= wm.GetById(id).WriterSurname,
                WriterAbout= wm.GetById(id).WriterAbout,
                WriterImage= wm.GetById(id).WriterImage,
                WriterTitle= wm.GetById(id).WriterTitle,
                WriterStatus=wm.GetById(id).WriterStatus
            };
            return View(editDto);
        }

        [HttpPost]
        public ActionResult EditWriter(WriterEditDto writerEditDto)
        {
            IAuthService authService = new AuthManager(new AdminManager(new EfAdminDal()), new WriterManager(new EfWriterDal()));
            ValidationResult results = writervalidator.Validate(writerEditDto);
            if (results.IsValid)
            {
                authService.WriterEdit(writerEditDto);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
    }
}