using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Desafio.Consumer.Validations.ForProduct
{
    public class ValidateQuantityAttribute : ValidationAttribute, IClientModelValidator
    {
        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-validatequantity", "The Amount must be positive!");
            MergeAttribute(context.Attributes, "data-val-validatequantity-rule", 0.ToString());
        }
        private bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }

            attributes.Add(key, value);
            return true;
        }

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
