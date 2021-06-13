using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YouTubeMvc.Controllers
{
    public class GalleryController : Controller
    {
        readonly private ImageFileManager ifm = new ImageFileManager(new EfImageFileDal());
        [Authorize]
        public ActionResult Index()
        {
            var files = ifm.GetList();
            return View(files);
        }
    }
}