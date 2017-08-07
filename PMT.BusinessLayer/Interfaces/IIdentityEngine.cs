namespace PMT.BusinessLayer
{
    public interface IIdentityEngine
    {
        string GetUserId(string userName);
        void InitializeNewUser(string userName,bool demoData);
    }
}