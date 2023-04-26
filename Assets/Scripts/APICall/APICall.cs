using System.Threading.Tasks;

public static class APICall
{
    public static async Task<SunInfo> RequestSunInfo()
    {
        var httpClient = new HttpClient(new JsonSerializationOption());
        //TODO: Coordinates will taken from real location.
        var url = "https://api.sunrise-sunset.org/json?lat=36.7201600&lng=-4.4203400";
        var result = await httpClient.Get<SunInfo>(url);
        return result;
        //UIManager.Instance.SetWelcomeMessageText(result.results.sunrise, result.results.sunset);
    }
}
