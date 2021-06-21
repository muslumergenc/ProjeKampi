using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules_FluentValidation;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YouTubeMvc.Controllers
{
    public class WriterPanelMessageController : Controller
    {
        // GET: WriterPanelMessage
        readonly private MessageManager mm = new MessageManager(new EfMessageDal());
        readonly private MessageValidator messagevalidator = new MessageValidator();
        public ActionResult Inbox()
        {
            var messagelist = mm.GetListInbox();
            return View(messagelist);
        }

        public ActionResult Sendbox()
        {
            var messagelist = mm.GetListSendbox();
            return View(messagelist);
        }

        public ActionResult GetInboxMessageDetails(int id)
        {
            var values = mm.GetById(id);
            if (values != null)
            {
                values.MessageID = mm.GetById(id).MessageID;
                values.isDraft = mm.GetById(id).isDraft;
                values.MessageContent = mm.GetById(id).MessageContent;
                values.MessageDate = mm.GetById(id).MessageDate;
                values.ReceiverMail = mm.GetById(id).ReceiverMail;
                values.SenderMail = mm.GetById(id).SenderMail;
                values.Subject = mm.GetById(id).Subject;
                values.isRead = true;
                mm.MessageUpdate(values);
            }
            return View(values);
        }

        public ActionResult GetSendboxMessageDetails(int id)
        {
            var values = mm.GetById(id);
            return View(values);
        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewMessage(Message p, string button)
        {
            ValidationResult results = new ValidationResult();
            if (button == "draft")
            {

                results = messagevalidator.Validate(p);
                if (results.IsValid)
                {
                    p.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                    p.SenderMail = "muslum@mail.com";
                    p.isDraft = true;
                    mm.MessageAdd(p);
                    return RedirectToAction("Draft");
                }
                else
                {
                    foreach (var item in results.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
            }
            else if (button == "kayit")
            {
                results = messagevalidator.Validate(p);
                if (results.IsValid)
                {
                    p.MessageDate = DateTime.Now;
                    p.SenderMail = "muslum@mail.com";
                    p.isDraft = false;
                    mm.MessageAdd(p);
                    return RedirectToAction("SendBox");
                }
                else
                {
                    foreach (var item in results.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
            }
            return View();
        }

        public PartialViewResult MessageListMenu()
        {
            Context _Context = new Context();
            string topContact = _Context.Contacts.Count().ToString();
            ViewBag.TopContact = topContact;

            string topMessage = _Context.Messages.Count(x => x.ReceiverMail == "muslum@mail.com").ToString();
            ViewBag.TopMessage = topMessage;

            string receiverMail = _Context.Messages.Count(x => x.ReceiverMail == "muslum@mail.com" && x.isRead == false).ToString();
            ViewBag.receiverMail = receiverMail;

            string senderMail = _Context.Messages.Count(x => x.SenderMail == "muslum@mail.com" && x.isRead == false).ToString();
            ViewBag.senderMail = senderMail;

            string contact = _Context.Contacts.Count(x => x.IsRead == false).ToString();
            ViewBag.contact = contact;

            string draft = _Context.Messages.Count(x => x.isDraft == true).ToString();
            ViewBag.draft = draft;

            return PartialView();
        }
    }
}