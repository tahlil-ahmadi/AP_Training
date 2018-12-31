using System;

namespace Framework.Core
{
    public interface IServiceLocator
    {
        T Resolve<T>() where T : class;
    }
}
