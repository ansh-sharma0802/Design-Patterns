namespace Creational
{
    public class LazySingleton
    {
        private static LazySingleton? instance = null;

        private LazySingleton() { }

        public static LazySingleton GetInstance()
        {
            if (instance is null)
                instance = new LazySingleton();

            return instance;
        }
    }

    public class ThreadSafeSingleton
    {
        private static ThreadSafeSingleton? instance = null;
        
        // Only supported by .NET 9+
        private static readonly Lock _lock = new(); 

        // For older versions
        //private static readonly object _lock = new();

        private ThreadSafeSingleton() { }

        public static ThreadSafeSingleton GetInstance()
        {
            lock (_lock)
            {
                if(instance is null)
                    instance = new ThreadSafeSingleton();

                return instance;
            }
        }
    }

    public class DoubleCheckedSingleton
    {
        private static DoubleCheckedSingleton? instance = null;
        private static readonly Lock _lock = new();

        private DoubleCheckedSingleton() { }

        public static DoubleCheckedSingleton GetInstance()
        {
            if (instance is null)
            {
                lock (_lock)
                {
                    if (instance is null)
                        instance = new DoubleCheckedSingleton();
                }
            }

            return instance;
        }
    }

    // Creates the Singleton instance during type initialization; GetInstance() returns the existing instance.
    public class EagerSingleton
    {
        private readonly static EagerSingleton instance = new();

        // Runtime has flexibility to initialize the static field earlier than its first access.
        //private EagerSingleton() { }

        // Runtime waits until type initialization is required, then runs the static initialization before allowing that use to proceed.
        // Recommended -> personal choice
        static EagerSingleton() { }

        public static EagerSingleton GetInstance() => instance;
    }

    // Creates the Lazy<T> wrapper during type initialization; the actual Singleton instance is created on the first .Value access.
    public class Singleton
    {
        private static readonly Lazy<Singleton> _lazy = new(() =>  new Singleton());

        private Singleton() { }

        public static Singleton GetInstance() => _lazy.Value;
    }
}