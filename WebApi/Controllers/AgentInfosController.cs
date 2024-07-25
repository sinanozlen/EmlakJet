using BusinessLayer.Abstract;
using DtoLayer.AgentInfoDtos;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentInfosController : ControllerBase
    {
        private readonly IAgentInfoService _agentInfoService;
        private readonly ILogger<AgentInfosController> _logger;

        public AgentInfosController(IAgentInfoService agentInfoService, ILogger<AgentInfosController> logger)
        {
            _agentInfoService = agentInfoService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAgentInfoList()
        {
            try
            {
                var agentInfos = _agentInfoService.TGetListAll();
                if (agentInfos == null || !agentInfos.Any())
                {
                    return NotFound("Agent bilgisi bulunamadı.");
                }
                return Ok(agentInfos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Agent listesi getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetAgentInfoDetail(int id)
        {
            try
            {
                var agentInfo = _agentInfoService.TGetbyID(id);
                if (agentInfo == null)
                {
                    return NotFound("Belirtilen ID'ye sahip agent bilgisi bulunamadı.");
                }
                return Ok(agentInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {id} olan agent bilgisi getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAgentInfo(int id)
        {
            try
            {
                var agentInfo = _agentInfoService.TGetbyID(id);
                if (agentInfo == null)
                {
                    return NotFound("Belirtilen ID'ye sahip agent bilgisi bulunamadı.");
                }

                _agentInfoService.TDelete(agentInfo);
                return Ok("Agent bilgisi başarıyla silindi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {id} olan agent bilgisi silinirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpPost]
        public IActionResult AddAgentInfo(CreateAgentInfoDto createAgentInfoDto)
        {
            if (createAgentInfoDto == null || string.IsNullOrWhiteSpace(createAgentInfoDto.Description) || string.IsNullOrWhiteSpace(createAgentInfoDto.Title))
            {
                return BadRequest("Geçersiz giriş verileri.");
            }

            try
            {
                var agentInfo = new AgentInfo
                {
                    Description = createAgentInfoDto.Description,
                    ImageUrl = createAgentInfoDto.ImageUrl,
                    Title = createAgentInfoDto.Title
                };
                _agentInfoService.TAdd(agentInfo);
                return StatusCode(StatusCodes.Status201Created, "Agent bilgisi başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Agent bilgisi eklenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpPut]
        public IActionResult UpdateAgentInfo(UpdateAgentInfoDto updateAgentInfoDto)
        {
            if (updateAgentInfoDto == null || string.IsNullOrWhiteSpace(updateAgentInfoDto.Description) || string.IsNullOrWhiteSpace(updateAgentInfoDto.Title))
            {
                return BadRequest("Geçersiz giriş verileri.");
            }

            try
            {
                var agentInfo = _agentInfoService.TGetbyID(updateAgentInfoDto.AgentInfoID);
                if (agentInfo == null)
                {
                    return NotFound("Belirtilen ID'ye sahip agent bilgisi bulunamadı.");
                }

                agentInfo.Description = updateAgentInfoDto.Description;
                agentInfo.ImageUrl = updateAgentInfoDto.ImageUrl;
                agentInfo.Title = updateAgentInfoDto.Title;

                _agentInfoService.TUpdate(agentInfo);
                return Ok("Agent bilgisi başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {updateAgentInfoDto.AgentInfoID} olan agent bilgisi güncellenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }
    }
}