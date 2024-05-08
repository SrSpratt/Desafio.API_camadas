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

        public ProductController(IService service)
        {
            _service = service;
        }

        [HttpGet]
        public List<ProductDto> Get()
        {
            try
            {
                List<Product> list = _service.ReadAll();
                List<ProductDto> dtolist = list != null ?Product.ToDtoList(list) : null;
                return dtolist;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
