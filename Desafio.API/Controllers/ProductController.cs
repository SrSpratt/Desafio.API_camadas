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
        public ActionResult<List<ProductDto>> GetAll()
        {
            try
            {
                List<Product> list = _service.ReadAll();
                List<ProductDto> dtolist = list != null ?Product.ToDtoList(list) : null;
                return dtolist; // use this to make conversions
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        [HttpGet("{id}")]
        public ActionResult<ProductDto> Get(int id)
        {
            try
            {
                Product product = _service.Read(id);
                return product == null ? NoContent() : product.ToDto(); //pode ser nulo
            } catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
            }

            return null;
        }

        [HttpGet("name/{name}")]
        public ActionResult<ProductDto> GetName(string name)
        {
            try
            {
                Product product = _service.ReadName(name);
                return product == null ? NoContent() : product.ToDto(); //pode ser nulo
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
            }

            return null;
        }

        [HttpPost]
        public ActionResult<ProductDto> Post([Bind("Description, SaleValue, Name, Supplier, Value, Category, ExpirationDate")]ProductDto productdto)
        {
            try
            {
                Product product = productdto.ToEntity();
                _service.Create(product);
                return Ok();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return BadRequest();
            
        }

        [HttpPut("{id}")]
        public ActionResult<ProductDto> Put(int id, [FromBody]ProductDto productdto)
        {
            try
            {
                Product product = productdto.ToEntity();
                _service.Update(id, product);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return _service.Read(id) == null ? Ok() : NotFound();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return BadRequest();
        }



    }
}
