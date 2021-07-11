using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YouTubeMvc.Controllers
{
    [Authorize]
    public class GalleryController : Controller
    {
        readonly private ImageFileManager ifm = new ImageFileManager(new EfImageFileDal());
        public ActionResult Index()
        {
            var files = ifm.GetList().OrderByDescending(x=> x.ImageID).ToList();
            return View(files);
        }
        [HttpGet]
        public ActionResult ImageAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImageAdd(ImageFile p)
        {
            if (Request.Files.Count > 0)
            {
                string fileName = Path.GetFileName(Request.Files[0].FileName);
                string expansion = Path.GetExtension(Request.Files[0].FileName);
                string path = "/Content/img/" + fileName + expansion;
                Request.Files[0].SaveAs(Server.MapPath(path));
                p.ImagePath = "/Content/img/" + fileName + expansion;
                ifm.Add(p);
                return RedirectToAction("Index");
            }
            return View();

        }
    }
}