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
    [Authorize]
    public class WriterController : Controller
    {
        readonly private WriterManager wm = new WriterManager(new EfWriterDal());
        readonly private HeadingManager hm = new HeadingManager(new EfHeadingDal());
        readonly WriterValidator writervalidator = new WriterValidator();

        public ActionResult Index()
        {
            var WriterValues = wm.GetList();
            if (WriterValues == null)
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
            ValidationResult results = writervalidator.Validate(editDto);
            if (results.IsValid)
            {
                authService.WriterAdd(editDto);
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

        [HttpGet]
        public ActionResult EditWriter(int id)
        {
            WriterEditDto editDto = new WriterEditDto
            {
                WriterID = id,
                WriterEmail = wm.GetById(id).WriterEmail,
                WriterName = wm.GetById(id).WriterName,
                WriterSurname = wm.GetById(id).WriterSurname,
                WriterAbout = wm.GetById(id).WriterAbout,
                WriterImage = wm.GetById(id).WriterImage,
                WriterTitle = wm.GetById(id).WriterTitle,
                WriterStatus = wm.GetById(id).WriterStatus,
                 WriterPassword=wm.GetById(id).WriterPasswordHash.ToString()
            };
            return View(editDto);
        }
        [HttpPost]
        public ActionResult EditWriter(WriterEditDto EditDto)
        {
            IAuthService authService = new AuthManager(new AdminManager(new EfAdminDal()), new WriterManager(new EfWriterDal()));
            ValidationResult results = writervalidator.Validate(EditDto);
            if (results.IsValid)
            {
                authService.WriterEdit(EditDto);
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

        public ActionResult WriterHeading(int id) 
        {
            var headingValues = hm.GetListByWriter(id);
            return View(headingValues);
        }
        public ActionResult WriterStatus(int id) 
        {
            var result = wm.GetById(id);
            if (result.WriterStatus== true)
            {
                result.WriterStatus = false;
            }
            else
            {
                result.WriterStatus = true;
            }
            wm.WriterUpdate(result);
            return RedirectToAction("Index");
        }
    }
}