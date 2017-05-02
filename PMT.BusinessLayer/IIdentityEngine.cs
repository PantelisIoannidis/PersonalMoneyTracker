namespace PMT.BusinessLayer
{
    public interface IIdentityEngine
    {
        string GetMoneyAccountId(string userName);
        void InitializeNewUser(string userName);
    }
}