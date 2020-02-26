using System.ComponentModel.DataAnnotations;
using api.Entities;
using System.Collections.Generic;

namespace api.Models
{
    /// <summary>
    /// Validates that a property is valid as a CaseId. Applies to string or List of string 
    /// </summary> 
    /// <remarks>Insulates clients from changes to the CaseId format</remarks>
    public class CaseIdAttribute : ValidationAttribute
    {
        public CaseIdAttribute() { }

        protected override ValidationResult IsValid(object value,
        ValidationContext validationContext)
        {
            bool isValid = false;
            if (value is string)
            {
                var id = (value as string) ?? "";
                isValid = LegalCase.IsValidCaseId(id);
            }
            else if (value is List<string>)
            {
                var caseList = value as List<string>;

                // empty lists are considered valid
                isValid = true;

                caseList.ForEach(c => isValid = isValid && LegalCase.IsValidCaseId(c));
            }

            if (!isValid)
            {
                return new ValidationResult("Invalid CaseId detected.");
            }

            return ValidationResult.Success;
        }
    }
}