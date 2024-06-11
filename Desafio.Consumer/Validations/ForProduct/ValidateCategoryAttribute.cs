using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Desafio.Consumer.Validations.ForProduct
{
    public class ValidateCategoryAttribute : ValidationAttribute, IClientValidatable
    {
        private string _word;


        public ValidateCategoryAttribute(string word)
        {
            _word = word;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage= FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = "validatecategory"
            };

            rule.ValidationParameters["word"] = _word;
            return new[] { rule };
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (!string.Equals(value.ToString(), _word, StringComparison.OrdinalIgnoreCase))
                return ValidationResult.Success;
            return new ValidationResult("Select a valid category!");
        }
    }
}
