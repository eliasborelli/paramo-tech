using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sat.Recruitment.Infraestructure.Commons
{
    public static class ModelStateExtension
    {
        public static IEnumerable<string> AllErrors(this ModelStateDictionary modelState)
        {
            return modelState.Keys.SelectMany(key => modelState[key].Errors.Select(x => x.ErrorMessage))
                .ToList();
        }

        public static IEnumerable<ValidationFailed> AllErrorsWithKey(this ModelStateDictionary modelState)
        {
            return modelState.Keys.SelectMany(key => modelState[key].Errors.Select(x => new ValidationFailed(key, x.ErrorMessage)))
                .ToList();
        }

        public static ValidationFailed FirstError(this ModelStateDictionary modelState)
        {
            return modelState.Keys.SelectMany(key => modelState[key].Errors.Select(x => new ValidationFailed(key, x.ErrorMessage)))
                .FirstOrDefault();
        }
    }

    public class ValidationFailed
    {
        public ValidationFailed(string key, string errorMessage)
        {
            this.Key = key;
            this.Message = errorMessage;
        }

        public string Key { get; set; }
        public string Message { get; set; }
    }
}
