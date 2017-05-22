using Microsoft.Extensions.Logging;
using PMT.Common;
using PMT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.DataLayer.Repositories
{
    public class IconRepository : RepositoryBase<Icon>, IIconRepository
    {
        ILogger logger;
        IActionStatus actionStatus;
        public IconRepository(ILoggerFactory logger, IActionStatus actionStatus) 
            :base(new MainDb())
        {
            this.actionStatus = actionStatus;
            this.logger = logger.CreateLogger<IconRepository>();
        }
}
}
