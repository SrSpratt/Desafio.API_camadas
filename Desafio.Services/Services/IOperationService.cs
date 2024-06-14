using Desafio.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Services.Services
{
    public interface IOperationService
    {
        Task<List<OperationDTO>> GetAll(int id);
    }
}
