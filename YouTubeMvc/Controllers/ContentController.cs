using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YouTubeMvc.Controllers
{
    [Authorize]
    public class ContentController : Controller
    {
        readonly private ContentManager cm = new ContentManager(new EfContentDal());
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAllContent(string p)
        {
            var values = cm.GetList(p);
            if (values.Count == 0)
            {
                values = cm.GetAllList();
                return View(values);
            }
            return View(values);
        }
        public ActionResult ContentByHeading(int id = 0)
        {
            var contentvalues = cm.GetListByHeadingID(id);
            return View(contentvalues);
        }
        public ActionResult ContentByHeadingWriter(int id=0)
        {
            var contentvalues = cm.GetListByHeadingID(id);
            return View(contentvalues);
        }
    }
}