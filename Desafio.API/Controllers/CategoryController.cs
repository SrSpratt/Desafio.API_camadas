using Desafio.Domain.Dtos;
using Desafio.Domain.Setup;
using Desafio.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly IApiConfig _config;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<List<CategoryDTO>> GetAllCategories()
        {
            return await _service.GetAllCategories();
        }

        [HttpGet("{id:int}")]
        public async Task<CategoryDTO> GetCategoy(int id)
        {
            return await _service.GetCategoy(id);
        }


        //TODO: daqui para baixo a implementar
        [HttpPost]
        public async Task<CategoryDTO> CreateCategory([FromBody]CategoryDTO category)
        {
            return await _service.CreateCategory(category);
        }

        [HttpPut("{id:int}")]
        public async Task<CategoryDTO> UpdateCategory(int id,[FromBody] CategoryDTO category)
        {
            return await _service.UpdateCategory(id, category);
        }

        [HttpDelete("{id:int}")]
        public async Task<CategoryDTO> DeleteCategory(int id)
        {
            return await _service.DeleteCategory(id);
        }
    }
}
