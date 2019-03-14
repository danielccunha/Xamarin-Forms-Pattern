using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MyProject.Utility
{
    public static class ValidationHelper
    {
        public static string[] GetErrors(List<ValidationResult> results, string memberName)
        {
            return results
                .Where(result => result.MemberNames.Contains(memberName))
                .Select(result => result.ErrorMessage)
                .ToArray();
        }

        public static bool Validate(object instance, List<ValidationResult> results, bool validateAllProperties)
        {
            return Validator.TryValidateObject(instance, new ValidationContext(instance), results, validateAllProperties);
        }

        public static bool Validate(object instance, ValidationContext context, List<ValidationResult> results, bool validateAllProperties)
        {
            return Validator.TryValidateObject(instance, context, results, validateAllProperties);
        }
    }
}
