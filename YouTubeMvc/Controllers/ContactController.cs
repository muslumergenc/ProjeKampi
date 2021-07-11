using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules_FluentValidation;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YouTubeMvc.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private Context _Context = new Context();
        readonly private ContactManager cm = new ContactManager(new EfContactDal());
        private ContactValidator cv = new ContactValidator();
        public ActionResult Index()
        {
            var contactvalues = cm.GetList();
            return View(contactvalues);
        }

        public ActionResult GetContactDetails(int id)
        {
            var contactvalues = cm.GetById(id);
            if (contactvalues != null)
            {
                contactvalues.ContactID = cm.GetById(id).ContactID;
                contactvalues.ContactDate = cm.GetById(id).ContactDate;
                contactvalues.Message = cm.GetById(id).Message;
                contactvalues.Subject = cm.GetById(id).Subject;
                contactvalues.UserMail = cm.GetById(id).UserMail;
                contactvalues.UserName = cm.GetById(id).UserName;
                contactvalues.IsRead = true;
                cm.ContactUpdate(contactvalues);
            }
            return View(contactvalues);
        }
        public PartialViewResult ContactMenuPartial()
        {
            string mail = Session["AdminUserName"].ToString();
            Context _Context = new Context();
            string topContact = _Context.Contacts.Where(x => x.UserMail == mail).Count().ToString();
            ViewBag.TopContact = topContact;

            string topMessage = _Context.Messages.Count(x => x.ReceiverMail == mail).ToString();
            ViewBag.TopMessage = topMessage;

            string receiverMail = _Context.Messages.Count(x => x.ReceiverMail == mail && x.isRead == false).ToString();
            ViewBag.receiverMail = receiverMail;

            string senderMail = _Context.Messages.Count(x => x.SenderMail == mail && x.isDraft == false).ToString();
            ViewBag.senderMail = senderMail;

            string contact = _Context.Contacts.Count(x => x.UserMail == mail && x.IsRead == false).ToString();
            ViewBag.contact = contact;

            string draft = _Context.Messages.Count(x => x.SenderMail == mail && x.isDraft == true).ToString();
            ViewBag.draft = draft;

            return PartialView();
        }
    }
}