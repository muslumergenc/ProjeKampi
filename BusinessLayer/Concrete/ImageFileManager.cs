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
    public class ImageFileManager : IImageFileService
    {
        private readonly IImageFileDal _imageFileDAL;

        public ImageFileManager(IImageFileDal imageFileDAL)
        {
            _imageFileDAL = imageFileDAL;
        }

        public void Add(ImageFile imageFile)
        {
            _imageFileDAL.Insert(imageFile);
        }

        public List<ImageFile> GetList()
        {
            return _imageFileDAL.List();
        }
    }
}
