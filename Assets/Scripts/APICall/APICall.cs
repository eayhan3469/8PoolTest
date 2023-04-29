using System.Threading.Tasks;

public static class APICall
{
    public static async Task<SunInfo> RequestSunInfo()
    {
        var httpClient = new HttpClient(new JsonSerializationOption());
        //TODO: Coordinates will taken from real location.
        var locationData = await LocationService.GetLocationData();
        
        var url = $"https://api.sunrise-sunset.org/json?lat={locationData.Latitude}&lng={locationData.Longitude}";
        var result = await httpClient.Get<SunInfo>(url);
        return result;
    }
}
