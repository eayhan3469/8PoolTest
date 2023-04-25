using UnityEngine;

public class APICallSunInfo : MonoBehaviour
{
    [ContextMenu("Test Get")]
    public async void OnPlayButtonClicked()
    {
        var httpClient = new HttpClient(new JsonSerializationOption());
        //TODO: Coordinates will taken from real location.
        var url = "https://api.sunrise-sunset.org/json?lat=36.7201600&lng=-4.4203400";
        var result = await httpClient.Get<SunInfo>(url);

        UIManager.Instance.SetWelcomeMessageText(result.results.sunrise, result.results.sunset);
    }
}
