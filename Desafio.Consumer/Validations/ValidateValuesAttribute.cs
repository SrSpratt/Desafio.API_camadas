using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Desafio.Consumer.Validations
{
    public class ValidateValuesAttribute : ValidationAttribute
    {
        public readonly float _min;
        public readonly float _max;
        private object?[] _fields;

        public ValidateValuesAttribute(float min, float max)
        {
            _min = min;
            _max = max;
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

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ValidationType="validatevalues",
                ErrorMessage= "The field is not valid!"
            };
            rule.ValidationParameters.Add("fields", string.Join(",", _fields));
            yield return rule;
        }
    }


}
