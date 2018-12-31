namespace Framework.Core
{
    public static class ServiceLocator
    {
        public static IServiceLocator Current { get; private set; }
        public static void Setup(IServiceLocator serviceLocator)
        {
            Current = serviceLocator;
        }
    }
}