using Desafio.Domain.Dtos;
using Desafio.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Services.Services
{
    public class OperationService : IOperationService
    {
        private readonly IOperationRepository _repository;
        public OperationService(IOperationRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<OperationDTO>> GetAll(int id)
        {
            return await _repository.GetAll(id);
        }
    }
}
