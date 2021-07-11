using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class MessageManager : IMessageService
    {
        private readonly IMessageDal _messageDAL;

        public MessageManager(IMessageDal messageDAL)
        {
            _messageDAL = messageDAL;
        }

        public Message GetById(int id)
        {
            return _messageDAL.Get(x => x.MessageID == id);
        }
        public List<Message> GetDraft(string mail) 
        {
            return _messageDAL.List().Where(x=> x.SenderMail==mail && x.isDraft == true).ToList();
        }

        public List<Message> GetListInbox(string mail)
        {
            return _messageDAL.List(x => x.ReceiverMail == mail);
        }

        public List<Message> GetListSendbox(string mail)
        {
            return _messageDAL.List(x => x.SenderMail == mail && x.isDraft==false);
        }

        public void MessageAdd(Message message)
        {
            _messageDAL.Insert(message);
        }

        public void MessageDelete(Message message)
        {
            _messageDAL.Delete(message);
        }

        public void MessageUpdate(Message message)
        {
            _messageDAL.Update(message);
        }
    }
}
