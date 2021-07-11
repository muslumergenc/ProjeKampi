using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules_Fluent;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YouTubeMvc.Controllers
{
    [Authorize(Roles ="A")]
    public class AboutController : Controller
    {
        readonly private AboutManager abm = new AboutManager(new EfAboutDal());
        public ActionResult Index()
        {
            var aboutvalues = abm.GetList();
            return View(aboutvalues);
        }
        [HttpGet]
        public ActionResult AddAbout()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAbout(About about)
        {
            AboutValidator aboutValidator = new AboutValidator();
            ValidationResult result = aboutValidator.Validate(about);
            if (result.IsValid)
            {
                abm.AboutAdd(about);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return PartialView("AboutPartial");
            }
        }
        [HttpGet]
        public ActionResult EditAbout(int id)
        {
            var aboutValues = abm.GetById(id);
            if (aboutValues != null)
            {
                return View(aboutValues);
            }
            return View();
        }
        [HttpPost]
        public ActionResult EditAbout(About about)
        {
            abm.AboutUpdate(about);
            return RedirectToAction("Index");

        }

        public ActionResult DeleteAbout(int id)
        {
            var aboutValues = abm.GetById(id);
            abm.AboutDelete(aboutValues);
            return RedirectToAction("Index");
        }

        public ActionResult AktifMi(int id)
        {
            var value = abm.GetById(id);
            if (value.Activation == true)
            {
                value.Activation = false;
            }
            else
            {
                value.Activation = true;
            }
            abm.AboutUpdate(value);
            return RedirectToAction("Index");
        }
        public PartialViewResult AboutPartial()
        {
            return PartialView();
        }
    }
}