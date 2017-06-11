using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.DataLayer.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        MainDb db;
        public IdentityRepository()
        {
            db = new MainDb();
        }
        public string GetUserId(string userName)
        {
            return db.Users.Where(o => o.UserName == userName).FirstOrDefault().Id;
        }
    }
}
