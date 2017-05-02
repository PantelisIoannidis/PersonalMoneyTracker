using System.Collections.Generic;
using PMT.Entities;

namespace PMT.DataLayer.Seed
{
    public interface ISeedingLists
    {
        MoneyAccount GetDefaultAccountForNewUser(string userId);
        List<Category> GetMainCategories();
        List<Repeat> GetRepeats();
        List<SubCategory> GetSubCategries();
    }
}