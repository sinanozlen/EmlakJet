using BusinessLayer.Abstract;
using DtoLayer.PropertyAgentDtos;
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
    public class PropertyAgentsController : ControllerBase
    {
        private readonly IPropertyAgentService _propertyAgentService;
        private readonly ILogger<PropertyAgentsController> _logger;

        public PropertyAgentsController(IPropertyAgentService propertyAgentService, ILogger<PropertyAgentsController> logger)
        {
            _propertyAgentService = propertyAgentService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult PropertyAgentList()
        {
            try
            {
                var propertyAgents = _propertyAgentService.TGetListAll();
                if (propertyAgents == null || !propertyAgents.Any())
                {
                    return NotFound("Emlak danışmanı bulunamadı.");
                }
                return Ok(propertyAgents);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Emlak danışmanı listesi getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult PropertyAgentDetail(int id)
        {
            try
            {
                var propertyAgent = _propertyAgentService.TGetbyID(id);
                if (propertyAgent == null)
                {
                    return NotFound("Belirtilen ID'ye sahip emlak danışmanı bulunamadı.");
                }
                return Ok(propertyAgent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {id} olan emlak danışmanı getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePropertyAgent(int id)
        {
            try
            {
                var propertyAgent = _propertyAgentService.TGetbyID(id);
                if (propertyAgent == null)
                {
                    return NotFound("Belirtilen ID'ye sahip emlak danışmanı bulunamadı.");
                }

                _propertyAgentService.TDelete(propertyAgent);
                return Ok("Emlak danışmanı başarıyla silindi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {id} olan emlak danışmanı silinirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpPost]
        public IActionResult CreatePropertyAgent(CreatePropertyAgentDto createPropertyAgentDto)
        {
            if (createPropertyAgentDto == null ||
                string.IsNullOrWhiteSpace(createPropertyAgentDto.FullName) ||
                string.IsNullOrWhiteSpace(createPropertyAgentDto.ImageUrl) ||
                string.IsNullOrWhiteSpace(createPropertyAgentDto.Role)
)
            {
                return BadRequest("Geçersiz giriş verileri.");
            }

            try
            {
                var propertyAgent = new PropertyAgent
                {
                    FacebookUrl = createPropertyAgentDto.FacebookUrl,
                    InstagramUrl = createPropertyAgentDto.InstagramUrl,
                    FullName = createPropertyAgentDto.FullName,
                    ImageUrl = createPropertyAgentDto.ImageUrl,
                    Role = createPropertyAgentDto.Role,
                    TwitterUrl = createPropertyAgentDto.TwitterUrl
                };

                _propertyAgentService.TAdd(propertyAgent);
                return StatusCode(StatusCodes.Status201Created, "Emlak danışmanı başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Emlak danışmanı eklenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpPut]
        public IActionResult UpdatePropertyAgent(UpdatePropertyAgentDto updatePropertyAgentDto)
        {
            if (updatePropertyAgentDto == null ||

                string.IsNullOrWhiteSpace(updatePropertyAgentDto.FullName) ||
                string.IsNullOrWhiteSpace(updatePropertyAgentDto.ImageUrl) ||
                string.IsNullOrWhiteSpace(updatePropertyAgentDto.Role))
            {
                return BadRequest("Geçersiz giriş verileri.");
            }

            try
            {
                var propertyAgent = _propertyAgentService.TGetbyID(updatePropertyAgentDto.PropertyAgentID);
                if (propertyAgent == null)
                {
                    return NotFound("Belirtilen ID'ye sahip emlak danışmanı bulunamadı.");
                }

                propertyAgent.FacebookUrl = updatePropertyAgentDto.FacebookUrl;
                propertyAgent.InstagramUrl = updatePropertyAgentDto.InstagramUrl;
                propertyAgent.FullName = updatePropertyAgentDto.FullName;
                propertyAgent.ImageUrl = updatePropertyAgentDto.ImageUrl;
                propertyAgent.Role = updatePropertyAgentDto.Role;
                propertyAgent.TwitterUrl = updatePropertyAgentDto.TwitterUrl;

                _propertyAgentService.TUpdate(propertyAgent);
                return Ok("Emlak danışmanı başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {updatePropertyAgentDto.PropertyAgentID} olan emlak danışmanı güncellenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }
    }
}