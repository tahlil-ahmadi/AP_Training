using System;
using Framework.Core;
using SimpleInjector;

namespace Framework.SimpleInjector
{
    public class SimpleInjectorAdapter : IServiceLocator
    {
        private readonly Container _container;
        public SimpleInjectorAdapter(Container container)
        {
            _container = container;
        }
        public T Resolve<T>() where T : class 
        {
            return _container.GetInstance<T>();
        }
    }
}
