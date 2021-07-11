using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules_FluentValidation;
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
    [Authorize]
    public class MessageController : Controller
    {
        readonly private MessageManager mm = new MessageManager(new EfMessageDal());
        readonly private MessageValidator messagevalidator = new MessageValidator();
    
        public ActionResult Inbox()
        {
            string mail = Session["AdminUserName"].ToString();
            var messagelist = mm.GetListInbox(mail);
            return View(messagelist);
        }

        public ActionResult Sendbox()
        {
            string mail = Session["AdminUserName"].ToString();
            var messagelist = mm.GetListSendbox(mail);
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
            string mail = Session["AdminUserName"].ToString();
            ValidationResult results = new ValidationResult();
            if (button == "draft")
            {

                results = messagevalidator.Validate(p);
                if (results.IsValid)
                {
                    p.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                    p.SenderMail = mail;
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
                    p.SenderMail = mail;
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

        public ActionResult Draft()
        {
            string mail = Session["AdminUserName"].ToString();
            var draftlist = mm.GetDraft(mail);
            return View(draftlist);
        }

        public ActionResult GetDraftMessageDetails(int id)
        {
            var Values = mm.GetById(id);
            return View(Values);
        }

        public ActionResult Delete(List<int> ids)
        {
            foreach (var id in ids)
            {
                if (id == 0)
                {
                    return RedirectToAction("Inbox");
                }
                else
                {
                    Message message = mm.GetById(id);
                    mm.MessageDelete(message);
                }
            }
            return RedirectToAction("Inbox");
        }
    }
}