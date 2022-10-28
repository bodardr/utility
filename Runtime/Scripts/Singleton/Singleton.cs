public class Singleton<T> where T : new()
{
    private static T instance;

    public static T Instance
    {
        get => instance != null ? instance : instance = new T();
        private set => instance = value;
    }
}