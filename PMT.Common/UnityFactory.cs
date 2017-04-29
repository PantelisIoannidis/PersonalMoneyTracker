using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Common
{
    public class UnityFactory : IUnityFactory
    {
        IUnityContainer container;
        public UnityFactory(IUnityContainer container)
        {
            this.container = container;
        }
        T IUnityFactory.GetObject<T>()
        {
            return container.Resolve<T>();
        }
    }
}
