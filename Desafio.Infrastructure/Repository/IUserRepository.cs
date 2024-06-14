using Desafio.Domain.Daos;
using Desafio.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Infrastructure.Repository
{
    public interface IUserRepository
    {
        Task<int> Create(UserDTO user);

        Task Update(int id, UserDTO user);
        Task<List<UserDTO>> ReadAll();
        Task<UserDTO> Read(int id);

        Task Delete(int id);
        //-----------------------------
        //ROLE
        Task<List<RoleDAO>> GetAllRoles();

        Task<RoleDAO> GetRole(int id);
        Task<RoleDAO> PlaceRole(RoleDAO userPiece);

        Task<RoleDAO> ReplaceRole(RoleDAO userPiece);

        Task<RoleDAO> RemoveRole(int id);

        //-----------------------------
        //ROLE
        Task<List<UserDAO>> GetAllUsers();

        Task<UserDAO> GetUser(int id);
        Task<UserDAO> PlaceUser(RoleDAO userPiece);

        Task<UserDAO> ReplaceUser(RoleDAO userPiece);

        Task<UserDAO> RemoveUser(int id);

        //----------------------------------------
        Task<UserDTO> Login(string name);
    }
}
