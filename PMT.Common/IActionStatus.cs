namespace PMT.Common
{
    public interface IActionStatus
    {
        string ExceptionInnerMessage { get; set; }
        string ExceptionInnerStackTrace { get; set; }
        string ExceptionMessage { get; set; }
        string ExceptionStackTrace { get; set; }
        string Message { get; set; }
        bool Status { get; set; }
    }
}