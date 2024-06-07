using Desafio.Domain.Dtos;
using Desafio.Domain.Setup;
using Desafio.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Infrastructure.Repository
{
    public class OperationRepository : IOperationRepository
    {
        private readonly IContext _context;
        private readonly IApiConfig _config;
        public OperationRepository(IApiConfig config)
        {
            _config = config;
            _context = new SqlContext(_config);
        }

        public async Task<List<OperationDTO>> GetAllOperations(int id)
        {
            return await _context.GetAllOperations(id);
        }
    }
}
