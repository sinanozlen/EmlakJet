using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using DtoLayer.RealEstateDtos;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfRealEstateDal : GenericRepository<RealEstate>, IRealEstateDal
    {
        private readonly EmlakJetContext _context;
        public EfRealEstateDal(EmlakJetContext emlakJetContext, EmlakJetContext context) : base(emlakJetContext)
        {
            _context = context;
        }

        public List<ResultRealEstateWithCategoryName> GetRealEstatesBySaleStatus(string saleStatus)
        {
            var values = _context.RealEstates
                                 .Include(x => x.Category)
                                 .Where(y => y.SaleStatus == saleStatus)
                                 .ToList(); // Senkron olarak verileri al

            var result = values.Select(z => new ResultRealEstateWithCategoryName
            {
                RealEstateId = z.RealEstateId,
                CategoryName = z.Category.CategoryName,
                SaleStatus = z.SaleStatus,
                Price = z.Price,
                Title = z.Title,
                Address = z.Address,
                Area = z.Area,
                RoomCount = z.RoomCount,
                BathRoomCount = z.BathRoomCount,
                ImageUrl = z.ImageUrl
            }).ToList();

            return result;
        }


        public List<ResultRealEstateWithCategoryName> GetRealEstatesWithCategory()
        {
            var values = _context.RealEstates
                             .Include(x => x.Category)
                             .Select(x => new ResultRealEstateWithCategoryName
                             {
                                 RealEstateId = x.RealEstateId,
                                 CategoryId = x.CategoryId,
                                 CategoryName = x.Category.CategoryName,
                                 SaleStatus = x.SaleStatus,
                                 Price = x.Price,
                                 Title = x.Title,
                                 Address = x.Address,
                                 Area = x.Area,
                                 RoomCount = x.RoomCount,
                                 BathRoomCount = x.BathRoomCount,
                                 ImageUrl = x.ImageUrl
                             }).ToList();
            return values;
        }

    }
}
