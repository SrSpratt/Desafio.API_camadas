using Desafio.Domain.Setup;
using Desafio.Domain.Dtos;
using Desafio.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IApiConfig _config;

        public ProductController(IProductService service, IApiConfig config)
        {
            _service = service;
            _config = config;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetAll()
        {
                List<ProductDTO> dtolist = await _service.ReadAll(); 
                return dtolist == null ? NoContent() : Ok(dtolist); // use this to make conversions
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
                ProductDTO product = await _service.Read(id);
                return product == null ? NoContent() : Ok(product); //pode ser nulo
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Post([FromBody]ProductDTO ProductDTO)
        {
                var productid = await _service.Create(ProductDTO);
                return CreatedAtAction(nameof(Get), new { id = productid }, null);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Put(int id, [FromBody]ProductDTO ProductDTO)
        {
                if (await _service.Read(id) is null)
                    return NotFound();
                await _service.Update(id, ProductDTO);
                return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
                if (await _service.Read(id) is null)
                    return NotFound();
                await _service.Delete(id);
                return await _service.Read(id) == null ? NoContent() : throw new ArgumentException("Something happened!");
        }

    }
}
