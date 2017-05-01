namespace PMT.BusinessLayer
{
    public interface IIdentityEngine
    {
        string GetUserAccountId(string userName);
        void InitializeNewUser(string userName);
    }
}