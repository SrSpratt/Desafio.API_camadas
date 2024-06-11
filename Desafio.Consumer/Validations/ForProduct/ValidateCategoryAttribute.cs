using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Desafio.Consumer.Validations.ForProduct
{
    public class ValidateCategoryAttribute : ValidationAttribute, IClientModelValidator
    {
        private string _word;


        public ValidateCategoryAttribute(string word)
        {
            _word = word;
        }

        private bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
                return false;

            attributes.Add(key, value);
            return true;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-validatecategory", "Choose a valid category!");
            MergeAttribute(context.Attributes, "data-val-validatecategory-word", _word);
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (!string.Equals(value.ToString(), _word, StringComparison.OrdinalIgnoreCase))
                return ValidationResult.Success;
            return new ValidationResult("Select a valid category!");
        }
    }
}
