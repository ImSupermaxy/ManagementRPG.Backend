using ManagementRPG.Domain.Abstractions.Commands.Results;
using ManagementRPG.Domain.Abstractions.Errors;
using ManagementRPG.Domain.Shared.ApiConfig;
using System.Diagnostics.CodeAnalysis;

namespace ManagementRPG.Domain.Shared.Commands
{
    public class Result
    {
        public Result(bool isSuccess, Error error, string exception = default!)
        {
            if (isSuccess && error != Error.None)
            {
                throw new InvalidOperationException();
            }

            if (!isSuccess && error == Error.None)
            {
                throw new InvalidOperationException();
            }

            IsSuccess = isSuccess;
            Error = new Error(error.Code, error.Name, RunMode.IsDev() ? [exception] : null);
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }
        public static Result Success() => new(true, Error.None);
        public static Result Failure(Error error) => new(false, error);
        public static Result Failure(Error error, string exception) => new(false, error, exception);
        public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);
        public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);
        public static Result<TValue> Failure<TValue>(Error error, string exception) => new(default, false, error, exception);
        public static Result<TValue> Create<TValue>(TValue? value) =>
            value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
    }

    public sealed class Result<TValue> : Result
    {
        private readonly TValue? _value;

        public Result(TValue? value, bool isSuccess, Error error, string exception = default!)
            : base(isSuccess, error, exception)
        {
            _value = value;
        }

        [NotNull]
        public TValue Value => IsSuccess
            ? _value!
            : default!;

        public static implicit operator Result<TValue>(TValue? value) => Create(value);
    }
}
