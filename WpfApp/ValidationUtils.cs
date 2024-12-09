using System;
using System.Linq;
using FluentValidation.Results;

namespace WpfApp
{
    public static class ValidationUtils
    {
        public static string ValidateResult(ValidationResult result)
        {
            return result.IsValid ? string.Empty : 
                string.Join(Environment.NewLine, result.Errors.Select(error => error.ErrorMessage));
        }
    }
}