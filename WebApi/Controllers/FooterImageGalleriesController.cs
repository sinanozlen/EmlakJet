using BusinessLayer.Abstract;
using DtoLayer.FooterImageGalleryDtos;
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
    public class FooterImageGalleriesController : ControllerBase
    {
        private readonly IFooterImageGalleryService _footerImageGalleryService;
        private readonly ILogger<FooterImageGalleriesController> _logger;

        public FooterImageGalleriesController(IFooterImageGalleryService footerImageGalleryService, ILogger<FooterImageGalleriesController> logger)
        {
            _footerImageGalleryService = footerImageGalleryService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult FooterImageGalleryList()
        {
            try
            {
                var footerImageGalleries = _footerImageGalleryService.TGetListAll();
                if (footerImageGalleries == null || !footerImageGalleries.Any())
                {
                    return NotFound("Footer resim galerisi bulunamadı.");
                }
                return Ok(footerImageGalleries);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Footer resim galerisi listesi getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult FooterImageGalleryDetail(int id)
        {
            try
            {
                var footerImageGallery = _footerImageGalleryService.TGetbyID(id);
                if (footerImageGallery == null)
                {
                    return NotFound("Belirtilen ID'ye sahip footer resim galerisi bulunamadı.");
                }
                return Ok(footerImageGallery);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {id} olan footer resim galerisi getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFooterImageGallery(int id)
        {
            try
            {
                var footerImageGallery = _footerImageGalleryService.TGetbyID(id);
                if (footerImageGallery == null)
                {
                    return NotFound("Belirtilen ID'ye sahip footer resim galerisi bulunamadı.");
                }

                _footerImageGalleryService.TDelete(footerImageGallery);
                return Ok("Footer resim galerisi başarıyla silindi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {id} olan footer resim galerisi silinirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpPost]
        public IActionResult CreateFooterImageGallery(CreateFooterImageGalleryDto createFooterImageGalleryDto)
        {
            if (createFooterImageGalleryDto == null || string.IsNullOrWhiteSpace(createFooterImageGalleryDto.ImageUrl))
            {
                return BadRequest("Geçersiz giriş verileri.");
            }

            try
            {
                var footerImageGallery = new FooterImageGallery
                {
                    ImageUrl = createFooterImageGalleryDto.ImageUrl
                };

                _footerImageGalleryService.TAdd(footerImageGallery);
                return StatusCode(StatusCodes.Status201Created, "Footer resim galerisi başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Footer resim galerisi eklenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpPut]
        public IActionResult UpdateFooterImageGallery(UpdateFooterImageGalleryDto updateFooterImageGalleryDto)
        {
            if (updateFooterImageGalleryDto == null || string.IsNullOrWhiteSpace(updateFooterImageGalleryDto.ImageUrl))
            {
                return BadRequest("Geçersiz giriş verileri.");
            }

            try
            {
                var footerImageGallery = _footerImageGalleryService.TGetbyID(updateFooterImageGalleryDto.GalleryID);
                if (footerImageGallery == null)
                {
                    return NotFound("Belirtilen ID'ye sahip footer resim galerisi bulunamadı.");
                }

                footerImageGallery.ImageUrl = updateFooterImageGalleryDto.ImageUrl;

                _footerImageGalleryService.TUpdate(footerImageGallery);
                return Ok("Footer resim galerisi başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {updateFooterImageGalleryDto.GalleryID} olan footer resim galerisi güncellenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }
    }
}