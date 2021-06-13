using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;

namespace YouTubeMvc.Controllers
{
    public class SkillsController : Controller
    {
        readonly private SkillsManager sm = new SkillsManager(new EfSkillDal());
        // GET: Skills
        [Authorize]
        public ActionResult Index()
        {
            return View(sm.GetList());
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Skills skills)
        {
            if (ModelState.IsValid)
            {
                sm.SkillAddBL(skills);
                return RedirectToAction("Index");
            }

            return View(skills);
        }

        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var skills = sm.GetById(id);
            if (skills == null)
            {
                return HttpNotFound();
            }
            return View(skills);
        }
        [HttpPost]
        public ActionResult Edit(Skills skills)
        {
            if (ModelState.IsValid)
            {
                sm.SkillsUpdate(skills);
                return RedirectToAction("Index");
            }
            return View(skills);
        }

        public ActionResult Delete(int id)
        {
            var skills = sm.GetById(id);
            if (skills != null)
            {
                sm.SkillsDelete(skills);
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }

        }

        public ActionResult Yetenekler()
        {
            return View(sm.GetList());
        }
    }
}
