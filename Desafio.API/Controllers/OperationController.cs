using Desafio.Domain.Dtos;
using Desafio.Domain.Setup;
using Desafio.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController
    {
        private readonly IOperationService _service;
        private readonly IApiConfig _config;

        public OperationController(IOperationService service)
        {
            _service = service;
        }

        [HttpGet("{id:int}")]
        public async Task<List<OperationDTO>> GetAllOperations(int id)
        {
            return await _service.GetAllOperations(id);
        }
    }
}
