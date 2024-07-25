using BusinessLayer.Abstract;
using DtoLayer.RealEstateDtos;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RealEstatesController : ControllerBase
    {
        private readonly IRealEstateService _realEstateService;
        private readonly ILogger<RealEstatesController> _logger;

        public RealEstatesController(IRealEstateService realEstateService, ILogger<RealEstatesController> logger)
        {
            _realEstateService = realEstateService;
            _logger = logger;
        }


        [HttpGet]
        public IActionResult GetRealEstateList()
        {
            try
            {
                var realEstates = _realEstateService.TGetListAll();
                if (realEstates == null || !realEstates.Any())
                {
                    return NotFound("Emlak bilgisi bulunamadı.");
                }
                return Ok(realEstates);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Emlak listesi getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetRealEstateDetail(int id)
        {
            try
            {
                var realEstate = _realEstateService.TGetbyID(id);
                if (realEstate == null)
                {
                    return NotFound("Belirtilen ID'ye sahip emlak bilgisi bulunamadı.");
                }
                return Ok(realEstate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {id} olan emlak bilgisi getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteRealEstate(int id)
        {
            try
            {
                var realEstate = _realEstateService.TGetbyID(id);
                if (realEstate == null)
                {
                    return NotFound("Belirtilen ID'ye sahip emlak bilgisi bulunamadı.");
                }

                _realEstateService.TDelete(realEstate);
                return Ok("Emlak bilgisi başarıyla silindi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {id} olan emlak bilgisi silinirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }


        [HttpPost]
        public IActionResult CreateRealEstate(CreateRealEstateDto createRealEstateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Geçersiz giriş verileri.");
            }

            try
            {
                var realEstate = new RealEstate
                {
                    Address = createRealEstateDto.Address,
                    Area = createRealEstateDto.Area,
                    BathRoomCount = createRealEstateDto.BathRoomCount,
                    CategoryId = createRealEstateDto.CategoryId,
                    ImageUrl = createRealEstateDto.ImageUrl,
                    Price = createRealEstateDto.Price,
                    RoomCount = createRealEstateDto.RoomCount,
                    SaleStatus = createRealEstateDto.SaleStatus,
                    Title = createRealEstateDto.Title
                };

                _realEstateService.TAdd(realEstate);
                return StatusCode(StatusCodes.Status201Created, "Emlak bilgisi başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Emlak bilgisi eklenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }


        [HttpPut]
        public IActionResult UpdateRealEstate(UpdateRealEstateDto updateRealEstateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Geçersiz giriş verileri.");
            }

            try
            {
                var realEstate = _realEstateService.TGetbyID(updateRealEstateDto.RealEstateId);
                if (realEstate == null)
                {
                    return NotFound("Belirtilen ID'ye sahip emlak bilgisi bulunamadı.");
                }

                realEstate.Address = updateRealEstateDto.Address;
                realEstate.Area = updateRealEstateDto.Area;
                realEstate.BathRoomCount = updateRealEstateDto.BathRoomCount;
                realEstate.CategoryId = updateRealEstateDto.CategoryId;
                realEstate.ImageUrl = updateRealEstateDto.ImageUrl;
                realEstate.Price = updateRealEstateDto.Price;
                realEstate.RoomCount = updateRealEstateDto.RoomCount;
                realEstate.SaleStatus = updateRealEstateDto.SaleStatus;
                realEstate.Title = updateRealEstateDto.Title;

                _realEstateService.TUpdate(realEstate);
                return Ok("Emlak bilgisi başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {updateRealEstateDto.RealEstateId} olan emlak bilgisi güncellenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }


        [HttpGet("GetRealEstateByCategoryName")]
        public IActionResult GetRealEstateByCategoryName()
        {
            try
            {
                var values = _realEstateService.TGetRealEstatesWithCategory();
                if (values == null || !values.Any())
                {
                    return NotFound("Kategoriye göre emlak bilgisi bulunamadı.");
                }
                return Ok(values);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategoriye göre emlak bilgisi getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }


        [HttpGet("GetRealEstateByStatus/{saleStatus}")]
        public IActionResult GetRealEstateByStatus(string saleStatus)
        {
            try
            {
                var values = _realEstateService.TGetRealEstatesBySaleStatus(saleStatus);
                if (values == null || !values.Any())
                {
                    return NotFound($"Satış durumu '{saleStatus}' olan emlak bilgisi bulunamadı.");
                }
                return Ok(values);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Satış durumu '{saleStatus}' olan emlak bilgisi getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }
    }
}