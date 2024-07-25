using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLayer.RealEstateDtos
{
    public class ResultRealEstateWithCategoryName
    {
        public int RealEstateId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string SaleStatus { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public int Area { get; set; }
        public int RoomCount { get; set; }
        public int BathRoomCount { get; set; }
        public string ImageUrl { get; set; }
    }
}
