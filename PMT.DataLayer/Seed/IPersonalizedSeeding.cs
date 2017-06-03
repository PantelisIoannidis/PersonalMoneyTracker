using PMT.Entities;

namespace PMT.DataLayer.Seed
{
    public interface IPersonalizedSeeding
    {
        void Categories(MainDb context, string userId);
        MoneyAccount GetDefaultAccountForNewUser(MainDb context,string userId);
    }
}