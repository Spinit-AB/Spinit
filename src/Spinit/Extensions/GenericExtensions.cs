using System;

namespace Spinit.Extensions
{
    public static class GenericExtensions
    {
        public static TResult With<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator)
            where TResult : class where TInput : class
        {
            return o == null ? null : evaluator(o);
        }

        public static TResult? With<TInput, TResult>(this TInput o, Func<TInput, TResult?> evaluator)
            where TResult : struct where TInput : class
        {
            return o == null ? null : evaluator(o);
        }

        public static TResult Return<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator, TResult failureValue = default(TResult))
            where TInput : class
        {
            return o == null ? failureValue : evaluator(o);
        }

        public static TResult Return<TInput, TResult>(this TInput? o, Func<TInput?, TResult> evaluator, TResult failureValue = default(TResult))
            where TInput : struct
        {
            return o == null ? failureValue : evaluator(o);
        }

        public static string ParseName<TModel>(this TModel model, Func<TModel, string> firstNameSelector, Func<TModel, string> lastNameSelector)
        {
            var firstName = firstNameSelector.Invoke(model);
            var lastName = lastNameSelector.Invoke(model);
            if (string.IsNullOrEmpty(firstName) == false && string.IsNullOrEmpty(lastName) == false)
            {
                return $"{firstName} {lastName}";
            }

            if (string.IsNullOrEmpty(firstName) == false)
            {
                return firstName;
            }

            return string.IsNullOrEmpty(lastName) == false ? lastName : string.Empty;
        }
    }
}