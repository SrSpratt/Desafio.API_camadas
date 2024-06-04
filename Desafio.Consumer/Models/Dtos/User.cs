using Desafio.Consumer.Validations;

namespace Desafio.Consumer.Models.Dtos
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //[ValidatePassword("senha")]  // vou trocar isto por uma variável ou retorno, e também criar um dto apenas para o login
        public string Password { get; set; }

        public string? Email { get; set; } //vou deixar anulável só temporariamente para testar a conexão

        public string? Role { get; set; } //vou deixar anulável só temporariamente para testar a conexão


    }
}
