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
        public async Task<UserDTO> ReadUser(int id)
        {
            return await _repository.ReadUser(id);
        }

        public async Task<List<UserDTO>> ReadUsers()
        {
            return await _repository.ReadUsers();
        }

        public async Task<int> CreateUser(UserDTO user)
        {
            var passwordHash = _passwordHasher.Hash(user.Password);
            user.Password = passwordHash;
            return await _repository.CreateUser(user);
        }

        public async Task UpdateUser(int id, UserDTO user)
        {
            await _repository.UpdateUser(id, user);
        }

        public async Task DeleteUser(int id)
        {
            await _repository.DeleteUser(id);
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
