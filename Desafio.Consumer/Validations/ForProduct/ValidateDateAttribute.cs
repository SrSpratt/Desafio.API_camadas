using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Desafio.Consumer.Validations.ForProduct
{
    public class ValidateDateAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly DateTime _date;

        public ValidateDateAttribute() 
        {
            _date = DateTime.Now;
        }
        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttributes(context.Attributes, "data-val", "true");
            MergeAttributes(context.Attributes, "data-val-validatedate", "Choose a valid date!");
            MergeAttributes(context.Attributes, "data-val-validatedate-date", _date.ToString());
        }

        private bool MergeAttributes(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
                return false;

            attributes.Add(key, value);
            return true;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (DateTime.TryParse(value.ToString(), out DateTime dateTime))
            {
                if (dateTime.Ticks > _date.Ticks)
                    return ValidationResult.Success;
            }
            return new ValidationResult("Can't register an expired product!");
        }
    }
}
