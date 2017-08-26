using System.Linq;
using PMT.Entities;
using System.Collections.Generic;

namespace PMT.BusinessLayer
{
    public interface IIconsEngine
    {
        IEnumerable<Icon> GetAll();
    }
}