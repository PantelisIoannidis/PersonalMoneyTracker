using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Common
{
    public class CurrentDateTime : ICurrentDateTime
    {
        /// <summary>
        /// To make DateTime.UtcNow more Unit Tests friendly
        /// </summary>
        /// <returns></returns>
        public DateTime DateTimeUtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}
