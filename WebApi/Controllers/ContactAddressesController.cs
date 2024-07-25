using BusinessLayer.Abstract;
using DtoLayer.ContactAddressDtos;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactAddressesController : ControllerBase
    {
        private readonly IContactAddressService _contactAddressService;
        private readonly ILogger<ContactAddressesController> _logger;

        public ContactAddressesController(IContactAddressService contactAddressService, ILogger<ContactAddressesController> logger)
        {
            _contactAddressService = contactAddressService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult ContactAddressList()
        {
            try
            {
                var contactAddresses = _contactAddressService.TGetListAll();
                if (contactAddresses == null || !contactAddresses.Any())
                {
                    return NotFound("İletişim adresi bilgisi bulunamadı.");
                }
                return Ok(contactAddresses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İletişim adresi listesi getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult ContactAddressDetail(int id)
        {
            try
            {
                var contactAddress = _contactAddressService.TGetbyID(id);
                if (contactAddress == null)
                {
                    return NotFound("Belirtilen ID'ye sahip iletişim adresi bulunamadı.");
                }
                return Ok(contactAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {id} olan iletişim adresi getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContactAddress(int id)
        {
            try
            {
                var contactAddress = _contactAddressService.TGetbyID(id);
                if (contactAddress == null)
                {
                    return NotFound("Belirtilen ID'ye sahip iletişim adresi bulunamadı.");
                }

                _contactAddressService.TDelete(contactAddress);
                return Ok("İletişim adresi başarıyla silindi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {id} olan iletişim adresi silinirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpPost]
        public IActionResult CreateContactAddress(CreateContactAddressDto createContactAddressDto)
        {
            if (createContactAddressDto == null || string.IsNullOrWhiteSpace(createContactAddressDto.Address)
                || string.IsNullOrWhiteSpace(createContactAddressDto.Description)
                || string.IsNullOrWhiteSpace(createContactAddressDto.Email)
                || string.IsNullOrWhiteSpace(createContactAddressDto.Phone)
                || string.IsNullOrWhiteSpace(createContactAddressDto.Title))
            {
                return BadRequest("Geçersiz giriş verileri.");
            }

            try
            {
                var contactAddress = new ContactAddress
                {
                    Address = createContactAddressDto.Address,
                    Description = createContactAddressDto.Description,
                    Email = createContactAddressDto.Email,
                    Phone = createContactAddressDto.Phone,
                    Title = createContactAddressDto.Title
                };

                _contactAddressService.TAdd(contactAddress);
                return StatusCode(StatusCodes.Status201Created, "İletişim adresi başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İletişim adresi eklenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpPut]
        public IActionResult UpdateContactAddress(UpdateContactAddressDto updateContactAddressDto)
        {
            if (updateContactAddressDto == null || string.IsNullOrWhiteSpace(updateContactAddressDto.Address)
                || string.IsNullOrWhiteSpace(updateContactAddressDto.Description)
                || string.IsNullOrWhiteSpace(updateContactAddressDto.Email)
                || string.IsNullOrWhiteSpace(updateContactAddressDto.Phone)
                || string.IsNullOrWhiteSpace(updateContactAddressDto.Title))
            {
                return BadRequest("Geçersiz giriş verileri.");
            }

            try
            {
                var contactAddress = _contactAddressService.TGetbyID(updateContactAddressDto.ContactAddressID);
                if (contactAddress == null)
                {
                    return NotFound("Belirtilen ID'ye sahip iletişim adresi bulunamadı.");
                }

                contactAddress.Address = updateContactAddressDto.Address;
                contactAddress.Description = updateContactAddressDto.Description;
                contactAddress.Email = updateContactAddressDto.Email;
                contactAddress.Phone = updateContactAddressDto.Phone;
                contactAddress.Title = updateContactAddressDto.Title;

                _contactAddressService.TUpdate(contactAddress);
                return Ok("İletişim adresi başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {updateContactAddressDto.ContactAddressID} olan iletişim adresi güncellenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }
    }
}