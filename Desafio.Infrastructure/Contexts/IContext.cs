using Desafio.Domain.Dtos;

namespace Desafio.Infrastructure.Contexts
{
    public interface IContext
    {
        //User
        Task<int> CreateUser(UserDTO user);

        Task UpdateUser(int id, UserDTO user);
        Task<List<UserDTO>> GetAllUsers();
        Task<UserDTO> GetUser(int id);

        Task DeleteUser(int id);

        Task<UserDTO> Login(string name);


        //Product
        Task<List<ProductDTO>> GetAll();

        Task<ProductDTO> Get(int id);

        Task<string> GetCategory(int name);

        Task Update(int id, ProductDTO product);

        Task<int> Create(ProductDTO product);

        Task Delete(int id);

        Task<List<OperationDTO>> GetAllOperations(int id);


        //Category
        Task<List<CategoryDTO>> GetAllCategories();

        Task<CategoryDTO> GetCategoy(int id);

        Task<CategoryDTO> CreateCategory(CategoryDTO category);

        Task<CategoryDTO> UpdateCategory(int id, CategoryDTO category);

        Task<CategoryDTO> DeleteCategory(int id); 

    }
}
