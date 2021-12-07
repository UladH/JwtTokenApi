namespace AppConfiguration
{
    public interface IAppConfiguration
    {
        string Get(string key);
        T Get<T>(string key);
    }
}
