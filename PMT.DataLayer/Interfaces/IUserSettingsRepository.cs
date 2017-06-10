using PMT.Common;
using PMT.Entities;

namespace PMT.DataLayer.Repositories
{
    public interface IUserSettingsRepository
    {
        UserSettings GetSettings(string userId);
        IActionStatus StoreSettings(UserSettings userSettings);
    }
}