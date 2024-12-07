using System;
using System.Collections.Generic;

namespace Application.Validation
{
    public class ValidationException : Exception
    {
        public List<string> Errors { get; }

        public ValidationException(IEnumerable<string> errors)
            : base("Se encontraron errores de validación.")
        {
            Errors = new List<string>(errors);
        }
    }
}
