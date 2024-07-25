using BusinessLayer.Abstract;
using DtoLayer.CategoryDtos;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult CategoryList()
        {
            try
            {
                var categories = _categoryService.TGetListAll();
                if (categories == null || !categories.Any())
                {
                    return NotFound("Kategori bilgisi bulunamadı.");
                }
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategori listesi getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult CategoryDetail(int id)
        {
            try
            {
                var category = _categoryService.TGetbyID(id);
                if (category == null)
                {
                    return NotFound("Belirtilen ID'ye sahip kategori bulunamadı.");
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {id} olan kategori getirilirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                var category = _categoryService.TGetbyID(id);
                if (category == null)
                {
                    return NotFound("Belirtilen ID'ye sahip kategori bulunamadı.");
                }

                _categoryService.TDelete(category);
                return Ok("Kategori başarıyla silindi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {id} olan kategori silinirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpPost]
        public IActionResult CreateCategory(CreateCategoryDto createCategoryDto)
        {
            if (createCategoryDto == null || string.IsNullOrWhiteSpace(createCategoryDto.CategoryName) || string.IsNullOrWhiteSpace(createCategoryDto.ImageUrl))
            {
                return BadRequest("Geçersiz giriş verileri.");
            }

            try
            {
                var category = new Category
                {
                    CategoryName = createCategoryDto.CategoryName,
                    ImageUrl = createCategoryDto.ImageUrl
                };

                _categoryService.TAdd(category);
                return StatusCode(StatusCodes.Status201Created, "Kategori başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategori eklenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }

        [HttpPut]
        public IActionResult UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            if (updateCategoryDto == null || string.IsNullOrWhiteSpace(updateCategoryDto.CategoryName) || string.IsNullOrWhiteSpace(updateCategoryDto.ImageUrl))
            {
                return BadRequest("Geçersiz giriş verileri.");
            }

            try
            {
                var category = _categoryService.TGetbyID(updateCategoryDto.CategoryId);
                if (category == null)
                {
                    return NotFound("Belirtilen ID'ye sahip kategori bulunamadı.");
                }

                category.CategoryName = updateCategoryDto.CategoryName;
                category.ImageUrl = updateCategoryDto.ImageUrl;

                _categoryService.TUpdate(category);
                return Ok("Kategori başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"ID'si {updateCategoryDto.CategoryId} olan kategori güncellenirken bir hata oluştu.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası.");
            }
        }
    }
}