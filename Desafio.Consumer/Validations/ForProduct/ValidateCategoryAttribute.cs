using System.ComponentModel.DataAnnotations;

namespace Desafio.Consumer.Validations.ForProduct
{
    public class ValidateCategoryAttribute : ValidationAttribute
    {
        private string _word;

        public ValidateCategoryAttribute(string word)
        {
            _word = word;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (!string.Equals(value.ToString(), _word, StringComparison.OrdinalIgnoreCase))
                return ValidationResult.Success;
            return new ValidationResult("Select a valid category!");
        }
    }
}
