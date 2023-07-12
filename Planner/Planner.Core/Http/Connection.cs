namespace Planner.Core.Http;

public static class Connection
{
    private static HttpClient? _osrmClient;

    public static HttpClient GetOsrmHttpClient()
    {
        if (_osrmClient is null)
        {
            _osrmClient = new HttpClient();
            _osrmClient.BaseAddress = new Uri("http://127.0.0.1:8000/route/v1/driving");

        }

        return _osrmClient;
    }
}