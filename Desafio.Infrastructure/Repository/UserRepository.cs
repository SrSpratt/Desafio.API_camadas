using Desafio.Domain.Daos;
using Desafio.Domain.Dtos;
using Desafio.Domain.Setup;
using Desafio.Infrastructure.Contexts;

namespace Desafio.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlContext<UserDTO> _context;
        private readonly SqlContext<UserDAO> _userContext;
        private readonly SqlContext<RoleDAO> _roleContext;
        private readonly SqlContext<UserNameDAO> _userNameContext;
        private readonly IApiConfig _apiConfig;
        public UserRepository(IApiConfig apiConfig)
        {
            _apiConfig = apiConfig;
            _context = new SqlContext<UserDTO>(_apiConfig);
            _userContext = new SqlContext<UserDAO>(_apiConfig);
            _roleContext = new SqlContext<RoleDAO>(_apiConfig);
            _userNameContext = new SqlContext<UserNameDAO>(_apiConfig);
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

        public Task<List<RoleDAO>> GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public async Task<RoleDAO> GetRole(int id)
        {
            return await _roleContext.Read(id);
        }

        public async Task<RoleDAO> GetRoleByName(string name)
        {
            return await _roleContext.ReadByName(name);
        }

        public async Task<RoleDAO> PlaceRole(RoleDAO userPiece)
        {
            return await _roleContext.Place(userPiece);
        }

        public Task<RoleDAO> ReplaceRole(RoleDAO userPiece)
        {
            throw new NotImplementedException();
        }

        public Task<RoleDAO> RemoveRole(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserDAO>> GetAllUsers()
        {
            return _userContext.ReadAll();
        }

        public async Task<UserDAO> GetUser(int id)
        {
            return await _userContext.Read(id);
        }

        public async Task<UserDAO> PlaceUser(UserDAO userPiece)
        {
            return await _userContext.Place(userPiece);
        }

        public Task<UserDAO> ReplaceUser(UserDAO userPiece)
        {
            throw new NotImplementedException();
        }

        public Task<UserDAO> RemoveUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserNameDAO>> GetMany(int id)
        {
            return await _userNameContext.ReadMany(id);
        }

        public async Task<UserNameDAO> PlaceName(UserNameDAO userPiece)
        {
            return await _userNameContext.Place(userPiece);
        }

        public Task<UserNameDAO> DeleteName(UserNameDAO userPiece)
        {
            throw new NotImplementedException();
        }

        public Task<UserNameDAO> UpdateName(UserNameDAO userPiece)
        {
            throw new NotImplementedException();
        }
    }
}
