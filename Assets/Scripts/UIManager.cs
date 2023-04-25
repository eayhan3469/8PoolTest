using System;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TextMeshProUGUI welcomeMessageText;
    [SerializeField] private TMP_InputField nameInputField;

    public void SetWelcomeMessageText(string sunrise, string sunset)
    {
        var currentTime = DateTime.UtcNow;
        var sunriseTime = DateTime.Parse(sunrise);
        var sunsetTime = DateTime.Parse(sunset);
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
    }
}
