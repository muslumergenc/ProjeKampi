using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YouTubeMvc.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        readonly private HeadingManager hm = new HeadingManager(new EfHeadingDal());
        readonly private ContentManager cm = new ContentManager(new EfContentDal());
        public ActionResult Headings()
        {
            var headingList = hm.GetList().Where(x=> x.HeadingStatus==true).ToList();
            return View(headingList);
        }
        public PartialViewResult Index(int id=0)
        {
            var contentList = cm.GetListByHeadingID(id);
            return PartialView(contentList);
        }
    }
}