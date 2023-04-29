using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Panels")]
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private GameObject gameplayPanel;
    [SerializeField] private GameObject gameFinishPanel;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI welcomeMessageText;
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Image loadingImage;
    [SerializeField] private Button startButton;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI scoreFinishText;

    void OnEnable()
    {
        GameManager.Instance.OnScoreUpdated += UpdateScoreTexts;
        GameManager.Instance.OnGameFinished += UpdateUIElements;
    }

    void OnDisable()
    {
        GameManager.Instance.OnScoreUpdated -= UpdateScoreTexts;
        GameManager.Instance.OnGameFinished -= UpdateUIElements;
    }

    public void OnStartButtonClicked()
    {
        startButton.gameObject.SetActive(false);
        loadingImage.gameObject.SetActive(true);
        UpdateWelcomeText();
    }

    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene(0);
    }

    private async void UpdateWelcomeText()
    {
        var sunInfo = await APICall.RequestSunInfo();
        //TODO: Should be tested later.
        var currentTime = DateTime.UtcNow;
        var sunriseTime = DateTime.Parse(sunInfo.results.sunrise);
        var sunsetTime = DateTime.Parse(sunInfo.results.sunset);
        string currentDayPeriod;
        string nextSunCondition;
        string nextSunConditionTime;

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

        await Task.Delay(1000);
        GameManager.Instance.OnGameStarted?.Invoke();
        UpdateUIElements();
    }

    private void UpdateUIElements()
    {
        mainMenuCanvas.SetActive(GameManager.Instance.CurrentStatus == GameStatus.NotStarted);
        gameCanvas.SetActive(GameManager.Instance.CurrentStatus != GameStatus.NotStarted);
        gameplayPanel.SetActive(GameManager.Instance.CurrentStatus == GameStatus.Started);
        gameFinishPanel.SetActive(GameManager.Instance.CurrentStatus == GameStatus.Finished);
    }

    private void UpdateScoreTexts(int score)
    {
        scoreText.text = String.Format("Score: {0}", score / 2);
        scoreFinishText.text = String.Format("Your Score Is : {0}", score / 2);
    }
}
