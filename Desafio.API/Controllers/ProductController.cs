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
        private readonly IService _service;
        private readonly IApiConfig _config;

        public ProductController(IService service, IApiConfig config)
        {
            _service = service;
            _config = config;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAll()
        {
            try
            {
                List<Product> list = await _service.ReadAll();
                List<ProductDto> dtolist = list != null ? Product.ToDtoList(list) : null;
                return dtolist; // use this to make conversions
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            try
            {
                Product product = await _service.Read(id);
                return product == null ? NoContent() : product.ToDto(); //pode ser nulo
            } catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
            }

            return null;
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<string>> GetCategory(int id)
        {
            try
            {
                string product = await _service.ReadCategory(id);
                return product == null ? NoContent() : product; //pode ser nulo
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
            }

            return null;
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Post([Bind("Description, SaleValue, Name, Supplier, Value, Category, ExpirationDate")]ProductDto productdto)
        {
            try
            {
                Product product = productdto.ToEntity();
                var productid = await _service.Create(product);
                var response = CreatedAtAction(nameof(Get), new { id = productid }, null);
                return response; // Colocar um created at action aqui
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return BadRequest();
            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDto>> Put(int id, [FromBody]ProductDto productdto)
        {
            try
            {
                Product product = productdto.ToEntity();
                await _service.Update(id, product);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
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
