using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private TextMeshProUGUI welcomeMessageText;
    [SerializeField] private TMP_InputField nameInputField;

    public void OnStartButtonClicked()
    {
        UpdateWelcomeText();
    }

    private async void UpdateWelcomeText()
    {
        var sunInfo = await APICall.RequestSunInfo();
        //TODO: Should be tested later.
        var currentTime = DateTime.UtcNow;
        var sunriseTime = DateTime.Parse(sunInfo.results.sunrise);
        var sunsetTime = DateTime.Parse(sunInfo.results.sunset);
        var currentDayPeriod = "";
        var nextSunCondition = "";
        var nextSunConditionTime = "";

        if (currentTime.TimeOfDay >= sunsetTime.TimeOfDay || currentTime.TimeOfDay < sunriseTime.TimeOfDay)
        {
            currentDayPeriod = "Night";
            nextSunConditionTime = sunriseTime.ToString("HH:mm");
            nextSunCondition = "Sunrise";
        }
        else
        {
            currentDayPeriod = "Day";
            nextSunConditionTime = sunsetTime.ToString("HH:mm");
            nextSunCondition = "Sunset";
        }

        welcomeMessageText.text = $"Great time {nameInputField.text} to play a {currentDayPeriod} Pinball game! {nextSunCondition} today starts at {nextSunConditionTime}";

        await Task.Delay(2000);
        GameManager.OnGameStarted?.Invoke();
        UpdateUIElements();
    }

    private void UpdateUIElements()
    {
        mainMenuCanvas.SetActive(GameManager.Instance.CurrentStatus == GameStatus.NotStarted);
    }
}
