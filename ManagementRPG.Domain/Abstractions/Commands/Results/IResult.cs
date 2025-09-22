namespace ManagementRPG.Domain.Abstractions.Commands.Results
{
    public interface IResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    public interface IResult<T> : IResult
    {
        public T Data { get; set; }
    }
}
