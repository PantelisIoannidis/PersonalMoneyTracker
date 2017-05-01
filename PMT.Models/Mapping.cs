using PMT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Models
{
    public class Mapping
    {
        public UserAccount UserAccountCreateNewMV_ToUserAccount(UserAccountCreateNewMV source)
        {
            return new UserAccount() {
                UserId = source.UserId,
                UserAccountId = source.UserAccountId,
                Name=source.Name
            };
        }
    }
}
