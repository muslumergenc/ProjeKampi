﻿using BusinessLayer.Abstract;
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

        public List<Message> GetListInbox()
        {
            return _messageDAL.List(x => x.ReceiverMail == "admin@mail.com");
        }

        public List<Message> GetListSendbox()
        {
            return _messageDAL.List(x => x.SenderMail == "admin@mail.com");
        }

        public void MessageAdd(Message message)
        {
            _messageDAL.Insert(message);
        }

        public void MessageDelete(Message message)
        {
            throw new NotImplementedException();
        }

        public void MessageUpdate(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
