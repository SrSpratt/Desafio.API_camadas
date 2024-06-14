using Desafio.Domain.Daos;
using Desafio.Domain.Dtos;
using Desafio.Infrastructure.Repository;
using Desafio.Services.Authentication;

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
            
            UserDAO user = await _repository.GetUser(id);
            List<UserNameDAO> userNames = await _repository.GetMany(id);
            string realName = "";
            for (int i = 0; i < userNames.Count; i++)
                if (i > 0)
                    realName += " " + userNames[i].UserName;
                else
                    realName += userNames[i].UserName;
            user.RealName = realName;
            RoleDAO role = await _repository.GetRole(user.Role);
            return new UserDTO(user, role);
        }

        public async Task<List<UserDTO>> ReadAll()
        {
            List<UserDAO> users = await _repository.GetAllUsers();
            List<UserDTO> results = new List<UserDTO>();
            foreach (UserDAO user in users)
            {
                List<UserNameDAO> userNames = await _repository.GetMany(user.ID);
                string realName = "";
                for (int i = 0; i < userNames.Count; i++)
                    if (i > 0)
                        realName += " " + userNames[i].UserName;
                    else
                        realName += userNames[i].UserName;
                user.RealName = realName;
                RoleDAO role = await _repository.GetRole(user.Role);
                results.Add(new UserDTO(user, role));
            }
            return results;
        }

        public async Task<int> Create(UserDTO user)
        {
            var passwordHash = _passwordHasher.Hash(user.Password);
            user.Password = passwordHash;
            UserDAO userDAO = new UserDAO
            {
                ID = user.ID,
                Name = user.Name,
                RealName = user.RealName,
                DateRegistered = user.DateRegistered,
                UserRegistered = user.UserRegistered,
                Password = user.Password,
                Role = (await _repository.GetRoleByName(user.Role)).ID,
                Email = user.Email,
            };
            UserDAO daoResult = await _repository.PlaceUser(userDAO);
            var splitName = user.RealName.Split(" ");
            List<UserNameDAO> names = new List<UserNameDAO>();
            foreach (string name in splitName)
            {
                names.Add( await _repository.PlaceName(
                        new UserNameDAO
                        {
                            UserID = daoResult.ID,
                            UserName = name,
                        }
                    )
                    );
            }
            string realName = "";
            for (int i = 0; i < names.Count; i++)
                if (i > 0)
                    realName += " " + names[i].UserName;
                else
                    realName += names[i].UserName;
            daoResult.RealName = realName;
            return daoResult.ID;
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
