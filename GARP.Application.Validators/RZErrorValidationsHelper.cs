using FluentValidation.Results;
using System.Collections.Generic;

namespace GARP.Application.Validators
{
    public static class RZErrorValidationsHelper
    {
        public static IEnumerable<string> getErrorMessages(IList<ValidationFailure> validationResult)
        {
            int size = validationResult.Count;
            List<string> errors = new(size);

            for (int i = 0; i < size; i++)
            {
                errors.Add(validationResult[i].ErrorMessage);
            }

            return errors;
        }
    }
}
