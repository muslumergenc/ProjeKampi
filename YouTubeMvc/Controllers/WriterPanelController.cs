using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules_FluentValidation;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using EntityLayer.Dtos;
using FluentValidation.Results;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YouTubeMvc.Controllers
{
    public class WriterPanelController : Controller
    {
        readonly private HeadingManager hm = new HeadingManager(new EfHeadingDal());
        readonly private CategoryManager cm = new CategoryManager(new EfCategoryDal());
        readonly private WriterManager wm = new WriterManager(new EfWriterDal());
        readonly WriterValidator writervalidator = new WriterValidator();
        [HttpGet]
        public ActionResult WriterProfile(int id=0)
        {
            string mail = (string)Session["WriterEmail"];
            id = wm.GetList().Where(x => x.WriterEmail == mail).Select(x => x.WriterID).FirstOrDefault();
            WriterEditDto writerDto = new WriterEditDto
            {
                WriterID = id,
                WriterEmail = wm.GetById(id).WriterEmail,
                WriterName = wm.GetById(id).WriterName,
                WriterSurname = wm.GetById(id).WriterSurname,
                WriterAbout = wm.GetById(id).WriterAbout,
                WriterImage = wm.GetById(id).WriterImage,
                WriterTitle = wm.GetById(id).WriterTitle,
                WriterStatus = wm.GetById(id).WriterStatus
            };
            return View(writerDto);
        }
        [HttpPost]
        public ActionResult WriterProfile(WriterEditDto writerEditDto)
        {
            IAuthService authService = new AuthManager(new AdminManager(new EfAdminDal()), new WriterManager(new EfWriterDal()));
            ValidationResult results = writervalidator.Validate(writerEditDto);
            if (results.IsValid)
            {
                authService.WriterEdit(writerEditDto);
                return RedirectToAction("AllHeading","WriterPanel");
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
        public ActionResult MyHeading(string p)
        {
            p = (string)Session["WriterEMail"];
            var writerIdinfo = wm.GetList().Where(x => x.WriterEmail == p).Select(y => y.WriterID).FirstOrDefault();
            var values = hm.GetListByWriter(writerIdinfo);
            return View(values);
        }
        [HttpGet]
        public ActionResult NewHeading()
        {
            List<SelectListItem> valuecategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();
            ViewBag.vlc = valuecategory;
            return View();
        }
        [HttpPost]
        public ActionResult NewHeading(Heading p)
        {
            string deger = (string)Session["WriterEmail"];
            var writeridhead = wm.GetList().Where(w => w.WriterEmail == deger).Select(x => x.WriterID).FirstOrDefault();
            p.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.WriterID = writeridhead;
            p.HeadingStatus = true;
            hm.HeadingAdd(p);
            return RedirectToAction("MyHeading");
        }
        [HttpGet]
        public ActionResult EditHeading(int id)
        {
            List<SelectListItem> valuecategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();
            ViewBag.vlc = valuecategory;
            var headingvalue = hm.GetById(id);
            return View(headingvalue);
        }
        [HttpPost]
        public ActionResult EditHeading(Heading p)
        {
            hm.HeadingUpdate(p);
            return RedirectToAction("myheading");
        }
        public ActionResult DeleteHeading(int id)
        {
            var result = hm.GetById(id);
            if (result.HeadingStatus == true)
            {
                result.HeadingStatus = false;
            }
            else
            {
                result.HeadingStatus = true;
            }
            hm.HeadingDelete(result);
            return RedirectToAction("MyHeading");
        }
        public ActionResult AllHeading(int p=1) 
        {
            var headings = hm.GetList().ToPagedList(p,3);
            return View(headings);
        }
    }
}