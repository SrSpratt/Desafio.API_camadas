using Desafio.Domain.Setup;
using Desafio.Domain.Dtos;
using Desafio.Domain.Entities;
using Desafio.Services.Services;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<List<ProductDto>>> GetAll()
        {
            try
            {
                List<ProductDto> dtolist = await _service.ReadAll(); 
                return dtolist == null ? NoContent() : Ok(dtolist); // use this to make conversions
            } catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            try
            {
                ProductDto product = await _service.Read(id);
                return product == null ? NoContent() : Ok(product); //pode ser nulo
            } catch (Exception ex)
            {
                
                throw new ArgumentException(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Post([FromBody]ProductDto productdto)
        {
            try
            {
                var productid = await _service.Create(productdto);
                return CreatedAtAction(nameof(Get), new { id = productid }, null);
            } catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }

            return BadRequest();
            
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProductDto>> Put(int id, [FromBody]ProductDto productdto)
        {
            try
            {
                await _service.Update(id, productdto);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }

            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _service.Delete(id);
                return await _service.Read(id) == null ? Ok() : NotFound();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return BadRequest();
        }



    }
}
