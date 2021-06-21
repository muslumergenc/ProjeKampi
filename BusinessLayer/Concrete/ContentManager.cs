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
    public class ContentManager : IContentService
    {
        private readonly IContentDal _contentDAL;

        public ContentManager(IContentDal contentDAL)
        {
            _contentDAL = contentDAL;
        }

        public void ContentAddBL(Content content)
        {
            _contentDAL.Insert(content);
        }

        public void ContentDelete(Content content)
        {
            _contentDAL.Delete(content);
        }

        public void ContentUpdate(Content content)
        {
            _contentDAL.Update(content);
        }

        public Content GetById(int id)
        {
            return _contentDAL.Get(x => x.ContentID == id);
        }

        public List<Content> GetList()
        {
            return _contentDAL.List();
        }

        public List<Content> GetListByHeadingID(int id)
        {
            return _contentDAL.List(x => x.HeadingID == id);
        }

        public List<Content> GetListByWriter(int id)
        {
            return _contentDAL.List(x => x.WriterID == id);
        }
    }
}
