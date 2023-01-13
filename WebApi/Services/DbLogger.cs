using WebApi.Services;

class DbLogger : ILoggerService
{
    public void Write(string message)
    {
        Console.WriteLine("[DBLogger] " + message);
    }
}
