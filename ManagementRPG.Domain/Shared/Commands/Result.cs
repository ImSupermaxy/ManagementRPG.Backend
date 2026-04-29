using ManagementRPG.Domain.Abstractions.Messages.Errors;
using ManagementRPG.Domain.Abstractions.Messages.Successes;
using ManagementRPG.Domain.Shared.ApiConfig;
using System.Diagnostics.CodeAnalysis;

namespace ManagementRPG.Domain.Shared.Commands
{
    public class Result
    {
        public Result(Result result, SuccessTask success, string process)
        {
            if (success == SuccessTask.NullValue)
                throw new InvalidOperationException();

            if (result.IsSuccess && success == SuccessTask.None)
                throw new InvalidOperationException();

            if (result.IsFailure && success != SuccessTask.None)
                throw new InvalidOperationException();

            IsSuccess = result.IsSuccess;
            Error = Error.None;
            Successes = RunMode.IsDev() ? result.Successes.Concat([new SuccessTask(success.Action, success.Message, process)]).ToArray() : [success];
        }

        public Result(bool isSuccess, Error error, string[] exceptions = default!)
        {
            if (isSuccess && error != Error.None)
                throw new InvalidOperationException();

            if (!isSuccess && error == Error.None)
                throw new InvalidOperationException();

            IsSuccess = isSuccess;
            Error = new Error(error.Code, error.Name, RunMode.IsDev() ? (exceptions?.ToList() ?? null) : null);
        }

        public bool IsSuccess { get; protected set; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; protected set; }
        public SuccessTask[] Successes { get; protected set; } = [];
        public static Result Success() => new(true, Error.None);
        public static Result Success(SuccessTask success, string process) => new(Success(), success, process);
        public static Result SuccessChain(Result result, SuccessTask success, string process) => new(result, success, process);
        public static Result Failure(Error error) => new(false, error);
        public static Result Failure(Error error, string[] exception) => new(false, error, exception);
        public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);
        public static Result<TValue> Success<TValue>(TValue value, SuccessTask success, string process) => new(Success(value), success, process);
        public static Result<TValue> SuccessChain<TValue>(Result<TValue> result, SuccessTask success, string process) => new(result, success, process);
        public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);
        public static Result<TValue> Failure<TValue>(Error error, string[] exception) => new(default, false, error, exception);
        public static Result<TValue> Create<TValue>(TValue? value) =>
            value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
        public Result Chain(params Result[] results)
        {
            foreach (var result in results.Reverse())
            {
                IsSuccess = result.IsSuccess && IsSuccess;
                Error = result.IsFailure ? result.Error : Error;
                Successes = result.Successes.Concat(Successes).ToArray();
            }

            return this;
        }
    }

    public sealed class Result<TValue> : Result
    {
        private readonly TValue? _value;

        public Result(TValue? value, bool isSuccess, Error error, string[] exceptions = default!)
            : base(isSuccess, error, exceptions)
        {
            _value = value;
        }

        public Result(Result<TValue> result, SuccessTask successes, string process)
            : base(result, successes, process)
        {
            _value = result.Value;
        }

        public new Result<TValue> Chain(params Result[] results)
        {
            foreach (var result in results.Reverse())
            {
                IsSuccess = result.IsSuccess && IsSuccess;
                Error = result.IsFailure ? result.Error : Error;
                Successes = result.Successes.Concat(Successes).ToArray();
            }

            return this;
        }

        [NotNull]
        public TValue Value => IsSuccess
            ? _value!
            : default!;

        public static implicit operator Result<TValue>(TValue? value) => Create(value);
    }
}
