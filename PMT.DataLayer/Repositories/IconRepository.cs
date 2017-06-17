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
    public class IconRepository : IIconRepository
    {
        ILogger logger;
        IActionStatus actionStatus;
        MainDb db;
        public IconRepository(MainDb db,ILoggerFactory logger, IActionStatus actionStatus) 
        {
            this.db = db;
            this.actionStatus = actionStatus;
            this.logger = logger.CreateLogger<IconRepository>();
        }

        public IEnumerable<Icon> GetAll()
        {
            IEnumerable<Icon> icons = new List<Icon>();
            try
            {
                icons = db.Icons;
            }
            catch(Exception ex)
            {
                logger.LogError(LoggingEvents.GET_ITEM, ex, "Couldn't get icon data from database ");
            }
            return icons;
        }
    }
}
