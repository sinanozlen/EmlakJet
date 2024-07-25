using DtoLayer.RealEstateDtos;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IRealEstateDal:IGenericDal<RealEstate>
    {
        List<ResultRealEstateWithCategoryName>GetRealEstatesWithCategory();
        List<ResultRealEstateWithCategoryName> GetRealEstatesBySaleStatus(string saleStatus);

    }
}
