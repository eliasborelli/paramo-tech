using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace Sat.Recruitment.Infraestructure.Commons
{
    /// <summary>
    /// Represent a Operation Result Pattern
    /// </summary>
    public class Result
    {
        #region Factory
        /// <summary>
        /// Create a failed <see cref="Result"/> with the given error messages
        /// </summary>
        /// <param name="errors"></param>
        /// <returns>Failed Result</returns>
        public static Result Failed(params string[] errors) =>
            new Result(false, errors);

        /// <summary>
        /// Create a failed <see cref="Result"/> with the given error messages
        /// </summary>
        /// <param name="ex">Current <seealso cref="Exception"/></param>
        /// <returns>Failed Result</returns>
        public static Result Failed(Exception ex) =>
            Failed(ex.Message);

        /// <summary>
        /// Create a failed <see cref="Result{TResult}"/> with the given error message
        /// </summary>
        /// <typeparam name="TResult">Type of value</typeparam>
        /// <param name="errors">Errors</param>
        /// <returns>Failed Result</returns>
        public static Result<TResult> Failed<TResult>(params string[] errors) =>
            new Result<TResult>(errors);

        /// <summary>
        /// Create a success <see cref="Result"/>
        /// </summary>
        /// <returns>Success Result</returns>
        public static Result Success() =>
            new Result(true, new string[] { });

        /// <summary>
        /// Create a success <see cref="Result"/> containing the given value
        /// </summary>
        /// <typeparam name="TResult">Tipo de <paramref name="value"/>Type of value</typeparam>
        /// <param name="value">Actual value</param>
        /// <returns>Success Result</returns>
        public static Result<TResult> Success<TResult>(TResult value) =>
            new Result<TResult>(value);

        /// <summary>
        /// Combines several results(and any error messages) into a single result.
        /// The returned result will be a failure if any of the input <paramref name="results"/> are failures.</summary>
        /// </summary>
        /// <param name="results">The Results to be combined</param>
        /// <returns>Resultado de la operacion</returns>
        public static Result Combine(params Result[] results)
        {
            foreach (var result in results)
            {
                if (!result.Succeeded)
                    return result;
            }

            return Success();
        }


        #endregion

        /// <summary>
        /// True if the <see cref="Result"/> was successfull, otherwise false
        /// </summary>
        public bool Succeeded { get; private set; }

        /// <summary>
        /// The error list from <see cref="Result"/>
        /// </summary>
        public IEnumerable<string> Errors { get; private set; }

        /// <summary>
        /// Single Error
        /// </summary>
        public string Error => Errors.Any() ? this.Errors.Single() : string.Empty;

        protected Result(bool succeeded, IEnumerable<string> errors)
        {
            this.Succeeded = succeeded;
            this.Errors = errors;
        }
    }

    /// <summary>
    /// Represent a Operation Result Pattern containing the given value
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class Result<TResult> : Result
    {
        /// <summary>
        /// Actual Value
        /// </summary>
        [JsonIgnore]
        public TResult Value { get; }

        protected internal Result(IEnumerable<string> errors)
            : base(false, errors)
        {
        }

        protected internal Result(TResult result)
            : base(true, new string[] { })
        {
            Value = result;
        }
    }
}
