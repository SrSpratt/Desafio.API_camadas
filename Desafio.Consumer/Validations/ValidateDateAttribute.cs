using System.ComponentModel.DataAnnotations;

namespace Desafio.Consumer.Validations
{
    public class ValidateDateAttribute : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (DateTime.TryParse(value.ToString(), out DateTime dateTime))
            {
                if (dateTime.Ticks > DateTime.Now.Ticks)
                    return ValidationResult.Success;
            }
            return new ValidationResult("Can't register an expired product!");
        }
    }
}
