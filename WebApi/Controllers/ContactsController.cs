using BusinessLayer.Abstract;
using DtoLayer.ContactDtos;
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
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly ILogger<ContactsController> _logger;

        public ContactsController(IContactService contactService, ILogger<ContactsController> logger)
        {
            _contactService = contactService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult ContactList()
        {
            try
            {
                var contacts = _contactService.TGetListAll();
                if (contacts == null || !contacts.Any())
                {
                    return NotFound("İletişim bilgisi bulunamadı.");
                }
                return Ok(contacts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İletişim listesi getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetContact(int id)
        {
            try
            {
                var contact = _contactService.TGetbyID(id);
                if (contact == null)
                {
                    return NotFound("Belirtilen ID'ye sahip iletişim bilgisi bulunamadı.");
                }
                return Ok(contact);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {id} olan iletişim bilgisi getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContact(int id)
        {
            try
            {
                var contact = _contactService.TGetbyID(id);
                if (contact == null)
                {
                    return NotFound("Belirtilen ID'ye sahip iletişim bilgisi bulunamadı.");
                }

                _contactService.TDelete(contact);
                return Ok("İletişim bilgisi başarıyla silindi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {id} olan iletişim bilgisi silinirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpPost]
        public IActionResult CreateContact(CreateContactDto createContactDto)
        {
            if (createContactDto == null || string.IsNullOrWhiteSpace(createContactDto.Email)
                || string.IsNullOrWhiteSpace(createContactDto.Message)
                || string.IsNullOrWhiteSpace(createContactDto.NameSurname)
                || string.IsNullOrWhiteSpace(createContactDto.Subject))
            {
                return BadRequest("Geçersiz giriş verileri.");
            }

            try
            {
                var contact = new Contact
                {
                    Email = createContactDto.Email,
                    Message = createContactDto.Message,
                    SendDate = DateTime.Now, // Otomatik olarak şimdiki tarihi ekleyin
                    NameSurname = createContactDto.NameSurname,
                    Subject = createContactDto.Subject,
                    IsRead = false
                };

                _contactService.TAdd(contact);
                return StatusCode(StatusCodes.Status201Created, "İletişim bilgisi başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İletişim bilgisi eklenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpPut]
        public IActionResult UpdateContact(UpdateContactDto updateContactDto)
        {
            if (updateContactDto == null || string.IsNullOrWhiteSpace(updateContactDto.Email)
                || string.IsNullOrWhiteSpace(updateContactDto.Message)
                || string.IsNullOrWhiteSpace(updateContactDto.NameSurname)
                || string.IsNullOrWhiteSpace(updateContactDto.Subject))
            {
                return BadRequest("Geçersiz giriş verileri.");
            }

            try
            {
                var contact = _contactService.TGetbyID(updateContactDto.ContactId);
                if (contact == null)
                {
                    return NotFound("Belirtilen ID'ye sahip iletişim bilgisi bulunamadı.");
                }

                contact.Email = updateContactDto.Email;
                contact.Message = updateContactDto.Message;
                contact.SendDate = updateContactDto.SendDate;
                contact.NameSurname = updateContactDto.NameSurname;
                contact.Subject = updateContactDto.Subject;

                _contactService.TUpdate(contact);
                return Ok("İletişim bilgisi başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {updateContactDto.ContactId} olan iletişim bilgisi güncellenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }
    }
}