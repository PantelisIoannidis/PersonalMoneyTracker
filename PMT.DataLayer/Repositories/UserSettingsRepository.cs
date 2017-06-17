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
    public class UserSettingsRepository : IUserSettingsRepository
    {
        ILogger logger;
        IActionStatus actionStatus;
        MainDb db;
        public UserSettingsRepository(MainDb db,ILoggerFactory logger, IActionStatus actionStatus)
        {
            this.db = db;
            this.actionStatus = actionStatus;
            this.logger = logger.CreateLogger<UserSettingsRepository>();
        }

        public UserSettings GetSettings(string userId)
        {
            return db.UserSettings.FirstOrDefault(x => x.UserId == userId);
        }

        public IActionStatus StoreSettings(UserSettings userSettings)
        {
            string errorMessage = "";
            try
            {
                var entity = db.UserSettings.FirstOrDefault(x => x.UserId == userSettings.UserId);
                if (entity == null)
                {
                    errorMessage = "Adding user settings in database";
                    db.UserSettings.Add(userSettings);
                }
                else
                {
                    errorMessage = "Updating user settings in database";
                    db.Entry(entity).CurrentValues.SetValues(userSettings);
                }

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                actionStatus = ActionStatus.CreateFromException(errorMessage, ex);
                logger.LogError(LoggingEvents.UPDATE_ITEM, ex, errorMessage);
            }
            return actionStatus;
        }
    }
}
