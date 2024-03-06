namespace UnityServiceLocator
{
    public interface IServiceLocator
    {
        public static IServiceLocator? Instance { get; }

        public void Register<TService>(TService serviceInstance, System.Action? onRegistered = null);
        public TService Get<TService>();
    }
}
