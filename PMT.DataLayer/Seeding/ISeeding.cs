using System.Collections.Generic;
using PMT.Entities;

namespace PMT.DataLayer.Seeding
{
    public interface ISeeding
    {
        UserAccount GetDefaultAccountForNewUser(string userId);
        List<Category> GetMainCategories();
        List<Repeat> GetRepeats();
        List<SubCategory> GetSubCategries();
    }
}