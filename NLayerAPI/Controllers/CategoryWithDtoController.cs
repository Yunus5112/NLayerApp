using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayerCore.DTOs;
using NLayerCore.Models;
using NLayerCore.Services;

namespace NLayerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryWithDtoController : CustomBaseController
    {
        private readonly IServiceWithDto<Category, CategoryDto> _categoryServiceWİthDto;
        public CategoryWithDtoController(IServiceWithDto<Category, CategoryDto> categoryServiceWİthDto)
        {
            _categoryServiceWİthDto = categoryServiceWİthDto;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return CreateActionResult(await _categoryServiceWİthDto.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Get(CategoryDto category)
        {
            return CreateActionResult(await _categoryServiceWİthDto.AddAsync(category));
        }
    }
}
