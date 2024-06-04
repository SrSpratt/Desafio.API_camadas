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
        public async Task<ActionResult<List<ProductDTO>>> GetAll()
        {
            try
            {
                List<ProductDTO> dtolist = await _service.ReadAll(); 
                return dtolist == null ? NoContent() : Ok(dtolist); // use this to make conversions
            } catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            try
            {
                ProductDTO product = await _service.Read(id);
                return product == null ? NoContent() : Ok(product); //pode ser nulo
            } catch (Exception ex)
            {
                
                throw new ArgumentException(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Post([FromBody]ProductDTO ProductDTO)
        {
            try
            {
                var productid = await _service.Create(ProductDTO);
                return CreatedAtAction(nameof(Get), new { id = productid }, null);
            } catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }

            return BadRequest();
            
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Put(int id, [FromBody]ProductDTO ProductDTO)
        {
            try
            {
                await _service.Update(id, ProductDTO);
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
            { //TODO: verificar se encontra antes, para gerar erro (?)
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
