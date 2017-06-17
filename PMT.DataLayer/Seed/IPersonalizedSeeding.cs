using PMT.Entities;

namespace PMT.DataLayer.Seed
{
    public interface IPersonalizedSeeding
    {
        void Categories(string userId);
        MoneyAccount GetDefaultAccountForNewUser(string userId);
    }
}