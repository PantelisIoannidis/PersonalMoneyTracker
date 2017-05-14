using System.Web;

namespace PMT.Web.Helpers
{
    public interface IUserPreferences
    {
        string GetLastUsedMoneyAccount(HttpContextBase httpContext);
        string GetTransactionPreferences(HttpContextBase httpContext);
        void SetLastUsedMoneyAccount(HttpContextBase httpContext, string moneyAccountObj);
        void SetTransactionPreferences(HttpContextBase httpContext, string transactionFilterPreferences);
    }
}