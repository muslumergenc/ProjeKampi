using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using YouTubeMvc.Models;

namespace YouTubeMvc.Controllers
{
    [Authorize]
    public class ChartController : Controller
    {
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DynamicCategory1()
        {
            ArrayList xvalue = new ArrayList();
            ArrayList yvalue = new ArrayList();
            var veriler = cm.GetList();
            veriler.ToList().ForEach(x => xvalue.Add(x.CategoryName));
            veriler.ToList().ForEach(y => yvalue.Add(y.CategoryName.Count()));
            var grafik = new Chart(width: 500, height: 500).AddTitle("Kategoriler").AddSeries(chartType: "Column", name: "Kategori-Başlık Grafiği", xValue: xvalue, yValues: yvalue);
            return File(grafik.ToWebImage().GetBytes(), "image/jpeg");
        }
        public ActionResult CategoryChart()
        {
            return Json(BlogList(), JsonRequestBehavior.AllowGet);
        }

        public List<CategoryClass> BlogList()
        {
            List<CategoryClass> ct = new List<CategoryClass>();
            ct.Add(new CategoryClass()
            {
                CategoryName = "Yazılım",
                CategoryCount = 12
            });
            ct.Add(new CategoryClass()
            {
                CategoryName = "Seyahat",
                CategoryCount = 8
            });
            ct.Add(new CategoryClass()
            {
                CategoryName = "Teknoloji",
                CategoryCount = 6
            });
            ct.Add(new CategoryClass()
            {
                CategoryName = "Spor",
                CategoryCount = 3
            });
            return ct;
        }
        public ActionResult CategoryChartDynamic()
        {
            return Json(BlogListDynamic(), JsonRequestBehavior.AllowGet);
        }
        public List<CategoryClass> BlogListDynamic()
        {
            List<CategoryClass> ct = new List<CategoryClass>();
            ct = cm.GetList().Select(x => new CategoryClass 
            {
                CategoryName=x.CategoryName,
                CategoryCount=x.CategoryName.Count()
            }).ToList();
            return ct;
        }

        public ActionResult SweetAlert() 
        {
            return View();
        }
    }
}