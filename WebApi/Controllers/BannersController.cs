using BusinessLayer.Abstract;
using DtoLayer.BannerDtos;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannersController : ControllerBase
    {
        private readonly IBannerService _bannerService;
        private readonly ILogger<BannersController> _logger;

        public BannersController(IBannerService bannerService, ILogger<BannersController> logger)
        {
            _bannerService = bannerService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult BannerList()
        {
            try
            {
                var banners = _bannerService.TGetListAll();
                if (banners == null || !banners.Any())
                {
                    return NotFound("Banner bilgisi bulunamadı.");
                }
                return Ok(banners);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Banner listesi getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetBanner(int id)
        {
            try
            {
                var banner = _bannerService.TGetbyID(id);
                if (banner == null)
                {
                    return NotFound("Belirtilen ID'ye sahip banner bulunamadı.");
                }
                return Ok(banner);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {id} olan banner getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBanner(int id)
        {
            try
            {
                var banner = _bannerService.TGetbyID(id);
                if (banner == null)
                {
                    return NotFound("Belirtilen ID'ye sahip banner bulunamadı.");
                }

                _bannerService.TDelete(banner);
                return Ok("Banner başarıyla silindi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {id} olan banner silinirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpPost]
        public IActionResult CreateBanner(CreateBannerDto createBannerDto)
        {
            if (createBannerDto == null || string.IsNullOrWhiteSpace(createBannerDto.Description) || string.IsNullOrWhiteSpace(createBannerDto.Title) || string.IsNullOrWhiteSpace(createBannerDto.ImageUrl))
            {
                return BadRequest("Geçersiz giriş verileri.");
            }

            try
            {
                var banner = new Banner
                {
                    Description = createBannerDto.Description,
                    ImageUrl = createBannerDto.ImageUrl,
                    Title = createBannerDto.Title
                };

                _bannerService.TAdd(banner);
                return StatusCode(StatusCodes.Status201Created, "Banner başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Banner eklenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpPut]
        public IActionResult UpdateBanner(UpdateBannerDto updateBannerDto)
        {
            if (updateBannerDto == null || string.IsNullOrWhiteSpace(updateBannerDto.Description) || string.IsNullOrWhiteSpace(updateBannerDto.Title) || string.IsNullOrWhiteSpace(updateBannerDto.ImageUrl))
            {
                return BadRequest("Geçersiz giriş verileri.");
            }

            try
            {
                var banner = _bannerService.TGetbyID(updateBannerDto.BannerID);
                if (banner == null)
                {
                    return NotFound("Belirtilen ID'ye sahip banner bulunamadı.");
                }

                banner.Description = updateBannerDto.Description;
                banner.ImageUrl = updateBannerDto.ImageUrl;
                banner.Title = updateBannerDto.Title;

                _bannerService.TUpdate(banner);
                return Ok("Banner başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {updateBannerDto.BannerID} olan banner güncellenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }
    }
}