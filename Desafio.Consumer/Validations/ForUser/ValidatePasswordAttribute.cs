using System.ComponentModel.DataAnnotations;

namespace Desafio.Consumer.Validations.ForUser
{
    public class ValidatePasswordAttribute : ValidationAttribute
    {
        private readonly string _password;

        public ValidatePasswordAttribute(string password)
        {
            _password = password;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value.Equals(_password))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Incorrect password!");
        }
    }
}
