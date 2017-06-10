using PMT.Common;
using PMT.Entities;
using System.Collections.Generic;

namespace PMT.BusinessLayer
{
    public interface IUserSettingsEngine
    {
        Dictionary<string, string> GetThemes();
        UserSettings GetUserSettings(string userId);
        ActionStatus StoreUserSettings(UserSettings userSettings);
    }
}