using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class FooterImageGalleryManager : IFooterImageGalleryService
    {
        private readonly IFooterImageGalleryDal _footerImageGalleryDal;

        public FooterImageGalleryManager(IFooterImageGalleryDal footerImageGalleryDal)
        {
            _footerImageGalleryDal = footerImageGalleryDal;
        }

        public void TAdd(FooterImageGallery entity)
        {
            _footerImageGalleryDal.Add(entity);
            
        }

        public void TDelete(FooterImageGallery entity)
        {
            _footerImageGalleryDal.Delete(entity);
        }

        public FooterImageGallery TGetbyID(int ID)
        {
            var values= _footerImageGalleryDal.GetbyID(ID);
            return values;
        }

        public List<FooterImageGallery> TGetListAll()
        {
            var values = _footerImageGalleryDal.GetListAll();
            return values;
        }

        public void TUpdate(FooterImageGallery entity)
        {
            _footerImageGalleryDal.Update(entity);
        }
    }
}
