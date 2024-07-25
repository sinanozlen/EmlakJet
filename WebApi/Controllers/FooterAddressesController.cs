using BusinessLayer.Abstract;
using DtoLayer.FooterAddressDtos;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FooterAddressesController : ControllerBase
    {
        private readonly IFooterAddressService _footerAddressService;
        private readonly ILogger<FooterAddressesController> _logger;

        public FooterAddressesController(IFooterAddressService footerAddressService, ILogger<FooterAddressesController> logger)
        {
            _footerAddressService = footerAddressService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult FooterAddressList()
        {
            try
            {
                var values = _footerAddressService.TGetListAll();
                if (values == null || !values.Any())
                {
                    return NotFound("Footer adresi bulunamadı.");
                }
                return Ok(values);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Footer adres listesi getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetFooterAddress(int id)
        {
            try
            {
                var value = _footerAddressService.TGetbyID(id);
                if (value == null)
                {
                    return NotFound("Belirtilen ID'ye sahip footer adresi bulunamadı.");
                }
                return Ok(value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {id} olan footer adresi getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFooterAddress(int id)
        {
            try
            {
                var value = _footerAddressService.TGetbyID(id);
                if (value == null)
                {
                    return NotFound("Belirtilen ID'ye sahip footer adresi bulunamadı.");
                }

                _footerAddressService.TDelete(value);
                return Ok("Footer adresi başarıyla silindi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {id} olan footer adresi silinirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpPost]
        public IActionResult CreateFooterAddress(CreateFooterAddressDto createFooterAddressDto)
        {
            if (createFooterAddressDto == null || string.IsNullOrWhiteSpace(createFooterAddressDto.Address)
                || string.IsNullOrWhiteSpace(createFooterAddressDto.Description)
                || string.IsNullOrWhiteSpace(createFooterAddressDto.Email)
                || string.IsNullOrWhiteSpace(createFooterAddressDto.Phone))
            {
                return BadRequest("Geçersiz giriş verileri.");
            }

            try
            {
                var footer = new FooterAddress
                {
                    Address = createFooterAddressDto.Address,
                    Description = createFooterAddressDto.Description,
                    Email = createFooterAddressDto.Email,
                    Phone = createFooterAddressDto.Phone
                };

                _footerAddressService.TAdd(footer);
                return StatusCode(StatusCodes.Status201Created, "Footer adresi başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Footer adresi eklenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpPut]
        public IActionResult UpdateFooterAddress(UpdateFooterAddressDto updateFooterAddressDto)
        {
            if (updateFooterAddressDto == null || string.IsNullOrWhiteSpace(updateFooterAddressDto.Address)
                || string.IsNullOrWhiteSpace(updateFooterAddressDto.Description)
                || string.IsNullOrWhiteSpace(updateFooterAddressDto.Email)
                || string.IsNullOrWhiteSpace(updateFooterAddressDto.Phone))
            {
                return BadRequest("Geçersiz giriş verileri.");
            }

            try
            {
                var footer = _footerAddressService.TGetbyID(updateFooterAddressDto.FooterAddressID);
                if (footer == null)
                {
                    return NotFound("Belirtilen ID'ye sahip footer adresi bulunamadı.");
                }

                footer.Address = updateFooterAddressDto.Address;
                footer.Description = updateFooterAddressDto.Description;
                footer.Email = updateFooterAddressDto.Email;
                footer.Phone = updateFooterAddressDto.Phone;

                _footerAddressService.TUpdate(footer);
                return Ok("Footer adresi başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {updateFooterAddressDto.FooterAddressID} olan footer adresi güncellenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }
    }
}