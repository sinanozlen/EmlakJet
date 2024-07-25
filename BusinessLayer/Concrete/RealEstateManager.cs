using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DtoLayer.RealEstateDtos;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class RealEstateManager : IRealEstateService
    {
        private readonly IRealEstateDal _realEstateDal;

        public RealEstateManager(IRealEstateDal realEstateDal)
        {
            _realEstateDal = realEstateDal;
        }

        public void TAdd(RealEstate entity)
        {
            _realEstateDal.Add(entity);
        }

        public void TDelete(RealEstate entity)
        {
            _realEstateDal.Delete(entity);
        }

        public RealEstate TGetbyID(int ID)
        {
            var values = _realEstateDal.GetbyID(ID);
            return values;
        }

        public List<RealEstate> TGetListAll()
        {
            var values = _realEstateDal.GetListAll();
            return values;
        }

        public List<ResultRealEstateWithCategoryName>TGetRealEstatesBySaleStatus(string saleStatus)
        {
            var values = _realEstateDal.GetRealEstatesBySaleStatus(saleStatus);
            return values;
        }

        public List<ResultRealEstateWithCategoryName> TGetRealEstatesWithCategory()
        {
            var values = _realEstateDal.GetRealEstatesWithCategory();
            return values;
        }

        public void TUpdate(RealEstate entity)
        {
            _realEstateDal.Update(entity);
        }
    }
}
