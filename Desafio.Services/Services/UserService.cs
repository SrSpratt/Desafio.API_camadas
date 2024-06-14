using Desafio.Domain.Dtos;
using Desafio.Infrastructure.Repository;
using Desafio.Services.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository repository, IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }
        public async Task<UserDTO> Read(int id)
        {
            return await _repository.Read(id);
        }

        public async Task<List<UserDTO>> ReadAll()
        {
            return await _repository.ReadAll();
        }

        public async Task<int> Create(UserDTO user)
        {
            var passwordHash = _passwordHasher.Hash(user.Password);
            user.Password = passwordHash;
            return await _repository.Create(user);
        }

        public async Task Update(int id, UserDTO user)
        {
            await _repository.Update(id, user);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<LoginResponse> Login(string name, string Password)
        {
            UserDTO user = await _repository.Login(name);
            LoginResponse login = new LoginResponse()
            {
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                Verified = _passwordHasher.Verify(user.Password, Password)
            };
            return login;
        }
    }
}
