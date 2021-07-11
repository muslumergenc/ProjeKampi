using EntityLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
   public interface IAuthService
    {
        void Register(string adminMail, string password,string userName, int roleId);
        bool Login(LoginDto loginDto);
        bool WriterLogin(WriterLoginDto writerLoginDto);
        void WriterRegister(string mail, string password);
        void WriterAdd(WriterEditDto writerEditDto);
        void WriterEdit(WriterEditDto writerEditDto);
        void RegisterWriter(string mail, string password, string Name,string surname);
    }
}
