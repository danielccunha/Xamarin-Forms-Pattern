using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MyProject.Validations
{
    public class HttpUriAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is string url)
            {
                string[] validSchemes = new string[] { Uri.UriSchemeHttp, Uri.UriSchemeHttps };
                return Uri.TryCreate(url.Trim(), UriKind.Absolute, out Uri uriResult) && validSchemes.Contains(uriResult.Scheme);
            }
            else
                throw new ValidationException($"Value must be a string. Type: {value.GetType().AssemblyQualifiedName}");
        }
    }
}
