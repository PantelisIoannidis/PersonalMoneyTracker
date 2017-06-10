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
            themes.Add("cerulean", "cerulean");
            themes.Add("cosmo", "cosmo");
            themes.Add("cyborg", "cyborg");
            themes.Add("darkly", "darkly");
            themes.Add("flatly", "flatly");
            themes.Add("journal", "journal");
            themes.Add("lumen", "lumen");
            themes.Add("paper", "paper");
            themes.Add("readable", "readable");
            themes.Add("sandstone", "sandstone");
            themes.Add("simplex", "simplex");
            themes.Add("slate", "slate");
            themes.Add("spacelab", "spacelab");
            themes.Add("superhero", "superhero");
            themes.Add("united", "united");
            themes.Add("yeti", "yeti");
            return themes;
        }
    }
}
