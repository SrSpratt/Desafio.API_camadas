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
        public async Task<List<CategoryDTO>> GetAll()
        {
            return await _service.GetAll();
        }

        [HttpGet("{id:int}")]
        public async Task<CategoryDTO> Get(int id)
        {
            return await _service.Get(id);
        }


        //TODO: daqui para baixo a implementar
        [HttpPost]
        public async Task<CategoryDTO> Create([FromBody]CategoryDTO category)
        {
            return await _service.Create(category);
        }

        [HttpPut("{id:int}")]
        public async Task<CategoryDTO> Update(int id,[FromBody] CategoryDTO category)
        {
            return await _service.Update(id, category);
        }

        [HttpDelete("{id:int}")]
        public async Task<CategoryDTO> Delete(int id)
        {
            return await _service.Delete(id);
        }
    }
}
