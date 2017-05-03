using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Common
{
    public class ActionStatus : IActionStatus
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionStackTrace { get; set; }
        public string ExceptionInnerMessage { get; set; }
        public string ExceptionInnerStackTrace { get; set; }

        public static ActionStatus CreateFromException(string message, Exception ex)
        {
            ActionStatus operationStatus = new ActionStatus
            {
                Status = false,
                Message = message,
            };

            if (ex != null)
            {
                operationStatus.ExceptionMessage = ex.Message;
                operationStatus.ExceptionStackTrace = ex.StackTrace;
                operationStatus.ExceptionInnerMessage = (ex.InnerException == null) ? null : ex.InnerException.Message;
                operationStatus.ExceptionInnerStackTrace = (ex.InnerException == null) ? null : ex.InnerException.StackTrace;
            }
            return operationStatus;
        }
    }
}
