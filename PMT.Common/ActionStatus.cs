using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Common
{
    public class ActionStatus : IActionStatus
    {
        public bool Status { get; set; } = true;

        public bool NotExceptionFalse { get; set; } = false;
        public string Message { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionStackTrace { get; set; }
        public string ExceptionInnerMessage { get; set; }
        public string ExceptionInnerStackTrace { get; set; }

        public static ActionStatus CreateFromConditions(string message)
        {
            ActionStatus actionStatus = new ActionStatus
            {
                Status = false,
                Message = message,
                NotExceptionFalse=true
            };

            return actionStatus;
        }

        public static ActionStatus CreateFromException(string message, Exception ex)
        {
            ActionStatus actionStatus = new ActionStatus
            {
                Status = false,
                Message = message,
            };

            if (ex != null)
            {
                actionStatus.ExceptionMessage = ex.Message;
                actionStatus.ExceptionStackTrace = ex.StackTrace;
                actionStatus.ExceptionInnerMessage = (ex.InnerException == null) ? null : ex.InnerException.Message;
                actionStatus.ExceptionInnerStackTrace = (ex.InnerException == null) ? null : ex.InnerException.StackTrace;
            }
            return actionStatus;
        }
    }
}
