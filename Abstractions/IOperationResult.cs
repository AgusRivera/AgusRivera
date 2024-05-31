namespace AMVTravels.Abstractions
{
    public interface IOperationResult
    {
        string? Message { get; set; }
        bool IsSuccess { get; set; }
        IEnumerable<string> Errors { get; set; }
    }

    public interface IOperationResult<TObject> : IOperationResult
    {
        TObject? Value { get; set; }
    }
}
