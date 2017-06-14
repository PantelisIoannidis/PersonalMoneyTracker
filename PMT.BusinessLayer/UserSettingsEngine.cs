using Microsoft.Extensions.Logging;
using PMT.Common;
using PMT.DataLayer.Repositories;
using PMT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.BusinessLayer
{
    public class UserSettingsEngine : IUserSettingsEngine
    {
        ILogger logger;
        IActionStatus actionStatus;
        IUserSettingsRepository userSettingsRepository;
        public UserSettingsEngine(ILoggerFactory logger,
                                    IActionStatus actionStatus,
                                    IUserSettingsRepository userSettingsRepository)
        {
            this.actionStatus = actionStatus;
            this.logger = logger.CreateLogger<UserSettingsEngine>();
            this.userSettingsRepository = userSettingsRepository;
        }

        public UserSettings GetUserSettings(string userId)
        {
            return userSettingsRepository.GetSettings(userId);
        } 

        public ActionStatus StoreUserSettings(UserSettings userSettings)
        {
            return (ActionStatus)userSettingsRepository.StoreSettings(userSettings);
        }

        public Dictionary<string,string> GetThemes()
        {
            Dictionary<string, string> themes = new Dictionary<string, string>();
            themes.Add("", "Default");
            themes.Add("cerulean", "Cerulean");
            themes.Add("cosmo", "Cosmo");
            themes.Add("cyborg", "Cyborg");
            themes.Add("darkly", "Darkly");
            themes.Add("flatly", "Flatly");
            themes.Add("journal", "Journal");
            themes.Add("lumen", "Lumen");
            themes.Add("paper", "Paper");
            themes.Add("readable", "Readable");
            themes.Add("sandstone", "Sandstone");
            themes.Add("simplex", "Simplex");
            themes.Add("slate", "Slate");
            themes.Add("spacelab", "Spacelab");
            themes.Add("superhero", "Superhero");
            themes.Add("united", "United");
            themes.Add("yeti", "Yeti");
            return themes;
        }
    }
}
