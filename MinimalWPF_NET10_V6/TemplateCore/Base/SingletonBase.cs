namespace System.Windows
{
    using System;

    public abstract class SingletonBase<T> where T : class
    {
        private static readonly Lazy<T> _instance =
            new Lazy<T>(() =>
            {
                var type = typeof(T);

                // Erstellt Instanz über privaten/protected Konstruktor
                var obj = (T)Activator.CreateInstance(type, nonPublic: true)!;

                // Falls Initialisierung unterstützt wird
                if (obj is ISingletonInitializable init)
                {
                    try
                    {
                        init.Initialize();
                    }
                    catch (Exception ex)
                    {
                        string errorText = $"Fehler bei der Initialisierung der Singleton-Instanz vom Typ {type.FullName}: {ex.Message}";
                        throw;
                    }
                }

                return obj;
            },System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

#pragma warning disable CA1000 // Statische Member nicht in generischen Typen deklarieren
        public static T Instance => _instance.Value;
#pragma warning restore CA1000 // Statische Member nicht in generischen Typen deklarieren
    }

    public interface ISingletonInitializable
    {
        void Initialize();
    }
}
