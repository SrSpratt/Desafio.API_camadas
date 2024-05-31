using Desafio.Domain.Dtos;
using Desafio.Domain.Entities;
using Desafio.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Services.Services
{
    public interface IService
    {
        public Task<UserDTO> Login(string username);
        public Task<List<ProductDto>> ReadAll();

        public Task<ProductDto> Read(int id);

        public Task Update(int id, ProductDto product);
        public Task<int> Create(ProductDto product);

        public Task Delete(int id);
    }
}
