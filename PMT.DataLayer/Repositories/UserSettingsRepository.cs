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
    public class UserSettingsRepository : RepositoryBase<UserSettings>, IUserSettingsRepository
    {
        ILogger logger;
        IActionStatus actionStatus;
        public UserSettingsRepository(ILoggerFactory logger, IActionStatus actionStatus) 
            :base(new MainDb())
        {
            this.actionStatus = actionStatus;
            this.logger = logger.CreateLogger<UserSettingsRepository>();
        }

        public UserSettings GetSettings(string userId)
        {
            return db.UserSettings.FirstOrDefault(x => x.UserId == userId);
        }

        public IActionStatus StoreSettings(UserSettings userSettings)
        {
            var entity = db.UserSettings.FirstOrDefault(x => x.UserId == userSettings.UserId);
            if (entity == null)
            {
                db.UserSettings.Add(userSettings);
            }
            else
            {
                db.Entry(entity).CurrentValues.SetValues(userSettings);
            }
            
            db.SaveChanges();
            return actionStatus;
        }
    }
}
