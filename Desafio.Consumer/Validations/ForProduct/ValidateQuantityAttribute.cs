using System.ComponentModel.DataAnnotations;

namespace Desafio.Consumer.Validations.ForProduct
{
    public class ValidateQuantityAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (int.TryParse(value.ToString(), out int parsedInt))
            {
                if (parsedInt >= 0)
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult("Quantity must be positive!");
        }
    }

}
