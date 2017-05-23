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
        public string Message { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionStackTrace { get; set; }
        public string ExceptionInnerMessage { get; set; }
        public string ExceptionInnerStackTrace { get; set; }

        public static ActionStatus CreateFromException(string message, Exception ex)
        {
            ActionStatus actionStatux = new ActionStatus
            {
                Status = false,
                Message = message,
            };

            if (ex != null)
            {
                actionStatux.ExceptionMessage = ex.Message;
                actionStatux.ExceptionStackTrace = ex.StackTrace;
                actionStatux.ExceptionInnerMessage = (ex.InnerException == null) ? null : ex.InnerException.Message;
                actionStatux.ExceptionInnerStackTrace = (ex.InnerException == null) ? null : ex.InnerException.StackTrace;
            }
            return actionStatux;
        }
    }
}
