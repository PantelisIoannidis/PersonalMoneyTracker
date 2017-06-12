using System.Linq;
using PMT.Entities;

namespace PMT.BusinessLayer
{
    public interface IIconsEngine
    {
        IQueryable<Icon> GetAll();
    }
}