using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YouTubeMvc.Controllers
{
    [Authorize]
    public class WriterPanelContentController : Controller
    {
        // GET: WriterPanelContent
        readonly private ContentManager cm = new ContentManager(new EfContentDal());
        readonly private WriterManager wm = new WriterManager(new EfWriterDal());
        public ActionResult MyContent(string p)
        {
            p = (string)Session["WriterEmail"];
            var writerIdinfo = wm.GetList()
                .Where(x => x.WriterEmail == p)
                .Select(y => y.WriterID).FirstOrDefault();
            if (writerIdinfo!=0)
            {
                var contentvalues = cm.GetListByWriter(writerIdinfo);
                return View(contentvalues);
            }
            else
            {
                return RedirectToAction("WriterLogin", "Login");
            }
           
        }
        [HttpGet]
        public ActionResult AddContent(int id) 
        {
            ViewBag.Id = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddContent(Content content) 
        {
            string mail = (string)Session["WriterEmail"];
            var writerIdinfo = wm.GetList()
               .Where(x => x.WriterEmail == mail)
               .Select(y => y.WriterID).FirstOrDefault();
            content.ContentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            content.WriterID = writerIdinfo;
            content.ContentStatus = true;
            cm.ContentAddBL(content);
            return RedirectToAction("MyContent");
        }
        public ActionResult ContentDelete(int id)
        {
            var contentvalues = cm.GetById(id);
            cm.ContentDelete(contentvalues);
            return RedirectToAction("MyContent");

        }
    }
}