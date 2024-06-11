using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Desafio.Consumer.Validations.ForProduct
{
    public class ValidateValuesAttribute : ValidationAttribute, IClientModelValidator
    {
        public readonly float _min;
        public readonly float _max;
        private object?[] _fields;

        public ValidateValuesAttribute(float min, float max)
        {
            _min = min;
            _max = max;
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

        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-validatevalues", "The field is not a valid number!");
            MergeAttribute(context.Attributes, "data-val-validatevalues-min", _min.ToString());
            MergeAttribute(context.Attributes, "data-val-validatevalues-max", _max.ToString());

        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
                return new ValidationResult($" The {validationContext.DisplayName} is required!");

            if (float.TryParse(value.ToString(), out float result))
            {
                if (result < _min || result > _max)
                    return new ValidationResult(string.Format("The value must be between {0} and {1}!", _min, _max));
                return ValidationResult.Success;
            }
            return new ValidationResult($"The field is not a valid number!");
        }



    }


}
