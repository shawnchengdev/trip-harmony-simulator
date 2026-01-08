using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    public int Day { get; private set; } = 0;
    public int Hour { get; private set; } = 0;
    public int Minute { get; private set; } = 0;
    private (int hour, int minute) _startingTime = (8, 0);

    private float _lastUpdatedTime;
    private float _currentTime;
    public int TotalDaysAmount { get; private set; } = 5;
    public bool GoButtonPressed = false;

    private const float SkipIterationTime = 0.05f;
    private bool _isSkippingTime = false;

    // Pause Game Stuff
    public bool GameIsPaused { get; private set; } = false;
    private float _gamePausedTime;

    [Header("Script References")]
    [SerializeField] private CharacterManager _characterManager;
    [SerializeField] private LocationManager _locationManager;
    [SerializeField] private DayNightCycle _dayNightCycle;
    [SerializeField] private GameReview _gameReview;

    [Header("Game Object Reference")]
    [SerializeField] private TextMeshProUGUI _dateText;


    void Start()
    {
        Hour = _startingTime.hour;
        Minute = _startingTime.minute;
        UpdateTimeText();

        _lastUpdatedTime = Time.time;
        _characterManager.UpdateAll();

        _locationManager.CheckAllLocationOpen(Hour);
    }

    void Update()
    {
        _currentTime = Time.time;

        if (!GameIsPaused && !_isSkippingTime && _currentTime - _lastUpdatedTime >= 1)
        {
            _lastUpdatedTime = _currentTime;
            IncrementTime();
            IsGameOver();

            if (!_isSkippingTime && Minute % 15 == 0)
            {
                _characterManager.UpdateAll();
                _characterManager.AddAllCharacterToSum(15);
            }
        }
    }


    #region Time Methods
    private void IncrementTime()
    {
        Minute += 1;
        (Day, Hour, Minute) = FixTimeOverflow(Day, Hour, Minute);
        UpdateTimeText();
    }

    private void UpdateTimeText()
    {
        string displayedMinute;
        int displayedHour = Hour;
        string dayPeriod = "AM";

        if (Hour >= 12)
        {
            displayedHour = Hour - 12;
            dayPeriod = "PM";
        }

        if (Minute == 0)
        {
            _locationManager.CheckAllLocationOpen(Hour);
        }

        if (displayedHour == 0)
        {
            displayedHour = 12;
        }

        if (Minute < 10)
        {
            displayedMinute = $"0{Minute}";
        }
        else
        {
            displayedMinute = Minute.ToString();
        }

        _dayNightCycle.SetMapColor(Hour, Minute);
        _dayNightCycle.CheckDayNightCycle(Hour);
        _dateText.text = $"{displayedHour}:{displayedMinute} {dayPeriod} Day {Day + 1}";
    }

    private void IsGameOver()
    {
        if (Day >= TotalDaysAmount)
        {
            Debug.Log("Game Over, Time Is Up");
            _gameReview.EndGame();
        }
    }

    public void TimeSkip(int day, int hour, int minute)
    {
        StartCoroutine(IterateToNewTime(day, hour, minute));
        IsGameOver();
    }

    private (int, int, int) FixTimeOverflow(int day, int hour, int minute)
    {
        while (minute >= 60)
        {
            minute -= 60;
            hour += 1;
        }

        while (hour >= 24)
        {
            hour -= 24;
            day += 1;
        }
        return (day, hour, minute);
    }

    public void PauseGame()
    {
        GameIsPaused = true;
        _gamePausedTime = _currentTime;
    }

    public void ResumeGame()
    {
        GameIsPaused = false;
        _lastUpdatedTime = _currentTime - (_gamePausedTime - _lastUpdatedTime);
    }

    public void StartGame()
    {
        print($"Game Started");
        GameIsPaused = false;
        _lastUpdatedTime = _currentTime;
    }

    IEnumerator IterateToNewTime(int day, int hour, int minute)
    {
        _isSkippingTime = true;
        int totalMinutes = minute + hour * 60 + day * 1440;
        float minutesPerIteration = totalMinutes * SkipIterationTime;
        float counter = 0;
        float newMinutes;
        float minuteDecimal = 0;

        (int day, int hour, int minute) endTime = FixTimeOverflow(day + Day, hour + Hour, minute + Minute);

        while (counter < 1)
        {
            counter += SkipIterationTime;
            newMinutes = Minute + minutesPerIteration + minuteDecimal;
            minuteDecimal = newMinutes - (int)newMinutes;

            (Day, Hour, Minute) = FixTimeOverflow(Day, Hour, (int)newMinutes);

            if (Day >= 5)
            {
                print("TIME PASSED");
                (Day, Hour, Minute) = (5, 23, 59);
                totalMinutes = (int) (counter * totalMinutes);
                UpdateTimeText();
                break;
            }
            else
            {
                UpdateTimeText();
                yield return new WaitForSeconds(SkipIterationTime);
            }
        }
        _locationManager.CheckAllLocationOpen(Hour);
        _characterManager.AddAllCharacterToSum(totalMinutes);
        ProgessCharacterStatsDuringSkip(totalMinutes);
        _isSkippingTime = false;
    }

    private void ProgessCharacterStatsDuringSkip(int totalMinutes)
    {
        for (int i = 0; i < totalMinutes/15; i++)
        {
            _characterManager.ProgressAllCharacterStats();
        }
    }
    #endregion
}
