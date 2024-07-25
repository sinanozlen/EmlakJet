using BusinessLayer.Abstract;
using DtoLayer.TestimonialDtos;
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
    public class TestimonialsController : ControllerBase
    {
        private readonly ITestimonialService _testimonialService;
        private readonly ILogger<TestimonialsController> _logger;

        public TestimonialsController(ITestimonialService testimonialService, ILogger<TestimonialsController> logger)
        {
            _testimonialService = testimonialService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult TestimonialList()
        {
            try
            {
                var testimonials = _testimonialService.TGetListAll();
                if (testimonials == null || !testimonials.Any())
                {
                    return NotFound("Referans bulunamadı.");
                }
                return Ok(testimonials);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Referans listesi getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult TestimonialDetail(int id)
        {
            try
            {
                var testimonial = _testimonialService.TGetbyID(id);
                if (testimonial == null)
                {
                    return NotFound("Belirtilen ID'ye sahip referans bulunamadı.");
                }
                return Ok(testimonial);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {id} olan referans getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTestimonial(int id)
        {
            try
            {
                var testimonial = _testimonialService.TGetbyID(id);
                if (testimonial == null)
                {
                    return NotFound("Belirtilen ID'ye sahip referans bulunamadı.");
                }

                _testimonialService.TDelete(testimonial);
                return Ok("Referans başarıyla silindi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {id} olan referans silinirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpPost]
        public IActionResult CreateTestimonial(CreateTestimonialDto createTestimonialDto)
        {
            if (createTestimonialDto == null ||
                string.IsNullOrWhiteSpace(createTestimonialDto.Comment) ||
                string.IsNullOrWhiteSpace(createTestimonialDto.ImageUrl) ||
                string.IsNullOrWhiteSpace(createTestimonialDto.Name))
            {
                return BadRequest("Geçersiz giriş verileri.");
            }

            try
            {
                var testimonial = new Testimonial
                {
                    Comment = createTestimonialDto.Comment,
                    ImageUrl = createTestimonialDto.ImageUrl,
                    Name = createTestimonialDto.Name
                };

                _testimonialService.TAdd(testimonial);
                return StatusCode(StatusCodes.Status201Created, "Referans başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Referans eklenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpPut]
        public IActionResult UpdateTestimonial(UpdateTestimonialDto updateTestimonialDto)
        {
            if (updateTestimonialDto == null ||
                string.IsNullOrWhiteSpace(updateTestimonialDto.Comment) ||
                string.IsNullOrWhiteSpace(updateTestimonialDto.ImageUrl) ||
                string.IsNullOrWhiteSpace(updateTestimonialDto.Name))
            {
                return BadRequest("Geçersiz giriş verileri.");
            }

            try
            {
                var testimonial = _testimonialService.TGetbyID(updateTestimonialDto.TestimonialID);
                if (testimonial == null)
                {
                    return NotFound("Belirtilen ID'ye sahip referans bulunamadı.");
                }

                testimonial.Comment = updateTestimonialDto.Comment;
                testimonial.ImageUrl = updateTestimonialDto.ImageUrl;
                testimonial.Name = updateTestimonialDto.Name;

                _testimonialService.TUpdate(testimonial);
                return Ok("Referans başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {updateTestimonialDto.TestimonialID} olan referans güncellenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }
    }
}