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
    public class WriterManager : IWriterService
    {
        private readonly IWriterDal _writerDAL;

        public WriterManager(IWriterDal writerDAL)
        {
            _writerDAL = writerDAL;
        }

        public Writer GetById(int id)
        {
            return _writerDAL.Get(x => x.WriterID == id);
        }
        public Writer GetByMail(string mail)
        {
            return _writerDAL.Get(x => x.WriterEmail== mail);
        }

        public List<Writer> GetList()
        {
            return _writerDAL.List();
        }

        public void WriterAdd(Writer writer)
        {
            _writerDAL.Insert(writer);
        }

        public void WriterDelete(Writer writer)
        {
            _writerDAL.Delete(writer);
        }

        public void WriterUpdate(Writer writer)
        {
            _writerDAL.Update(writer);
        }
    }
}
