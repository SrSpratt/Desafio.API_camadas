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
    public class UserRepository : IUserRepository
    {
        private readonly IContext _context;
        private readonly IApiConfig _apiConfig;
        public UserRepository(IApiConfig apiConfig)
        {
            _apiConfig = apiConfig;
            _context = new SqlContext<UserDTO>(_apiConfig);
        }
        public async Task<UserDTO> Read(int id)
        {
            return await _context.GetUser(id);
        }

        public async Task<List<UserDTO>> ReadAll()
        {
            return await _context.GetAllUsers();
        }

        public async Task<int> Create(UserDTO user)
        {
            return await _context.CreateUser(user);
        }

        public async Task Update(int id, UserDTO user)
        {
            await _context.UpdateUser(id, user);
        }

        public async Task Delete(int id)
        {
            await _context.DeleteUser(id);
        }

        public async Task<UserDTO> Login(string name)
        {
            return await _context.Login(name);
        }
    }
}
