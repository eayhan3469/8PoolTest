using System.Threading.Tasks;
using UnityEngine;

public static class LocationService
{
    private static LocationData defaultLocationData = new LocationData(41.0122f, -28.976f);

    public static async Task<LocationData> GetLocationData()
    {
        Input.location.Start();

        if (!Input.location.isEnabledByUser)
            return defaultLocationData;

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            await Task.Delay(1000);
            maxWait--;
        }

        if (maxWait < 1)
        {
            return defaultLocationData;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            return defaultLocationData;
        }
        else
        {
            var locationInfo = new LocationData(Input.location.lastData.latitude, Input.location.lastData.longitude);
            Input.location.Stop();
            return locationInfo;
        }

    }
}

public class LocationData
{
    public float Latitude;
    public float Longitude;

    public LocationData(float latitude, float longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}
