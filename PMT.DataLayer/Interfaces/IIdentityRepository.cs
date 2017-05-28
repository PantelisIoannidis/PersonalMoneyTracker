namespace PMT.DataLayer.Repositories
{
    public interface IIdentityRepository
    {
        string GetUserId(string userName);
    }
}