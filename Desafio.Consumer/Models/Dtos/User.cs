﻿using Desafio.Consumer.Validations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Desafio.Consumer.Models.Dtos
{
    public class User
    {
        public int Id { get; set; }

        [DisplayName("User Name")]
        public string Name { get; set; }

        [Required]
        //[ValidatePassword("senha")]  // vou trocar isto por uma variável ou retorno, e também criar um dto apenas para o login
        [Length(3, 6, ErrorMessage ="The password should have between 3 and 6 characters!")]
        public string Password { get; set; }

        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; } //vou deixar anulável só temporariamente para testar a conexão

        [Required]
        public string Role { get; set; } //vou deixar anulável só temporariamente para testar a conexão

        [Required]
        public DateTime DateRegistered { get; set; }

        [Required]
        public string UserRegistered { get; set; }

        [Required]
        [DisplayName("Full Name")]
        public string RealName { get; set; }

    }
}
