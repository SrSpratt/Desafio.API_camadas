
namespace Desafio.Domain.Dtos
{
    public class LoginResponse
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public bool Verified {  get; set; }

        public string Role { get; set; }
    }
}
