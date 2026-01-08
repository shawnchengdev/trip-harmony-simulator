using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour
{
    private bool _isDay = true;
    private const int SunRiseHour = 6;
    private const int SunSetHour = 18;

    private (int startHour, int endHour) StandardDayTime = (8, 15);
    private (int startHour, int endHour) StandardNightTime = (20, 5);

    private int _lastSetMinute = 0;
    private const int _mapTintIterationRequirement = 5;

    [Header("Day and Night References")]
    [SerializeField] private Image _sunImage;
    [SerializeField] private Image _moonImage;
    [SerializeField] private Image _mapImage;

    [SerializeField] private TextMeshProUGUI _dateText;
    [SerializeField] private Color _dayDateTextColor;
    [SerializeField] private Color _nightDateTextColor;
    [SerializeField] private Color _dayStandardColor;
    [SerializeField] private Color _nightStandardColor;

    [SerializeField] private AudioManager _audioManager;


    private void Start()
    {
        _sunImage.enabled = true;
        _moonImage.enabled = false;
        _mapImage.color = Color.white;
    }

    public void CheckDayNightCycle(int currentHour)
    {
        if (currentHour >= SunRiseHour && currentHour < SunSetHour && !_isDay)
        {
            SetToDay();
            _isDay = true;
        }
        else if ((currentHour >= SunSetHour || currentHour < SunRiseHour) && _isDay)
        {
            SetToNight();
            _isDay = false;
        }
    }

    private void SetToNight()
    {
        _sunImage.enabled = false;
        _moonImage.enabled = true;
        _dateText.color = _nightDateTextColor;
        _audioManager.PlayMusic(_audioManager.NightSong);
    }

    private void SetToDay()
    {
        _sunImage.enabled = true;
        _moonImage.enabled = false;
        _dateText.color = _dayDateTextColor;
        _audioManager.PlayMusic(_audioManager.DaySong);
    }

    public void SetMapColor(int currentHour, int currentMinute)
    {
        if (math.abs(currentMinute - _lastSetMinute) >= _mapTintIterationRequirement)
        {
            _lastSetMinute = currentMinute;

            if (currentHour >= StandardDayTime.startHour && currentHour < StandardDayTime.endHour)
            {
                _mapImage.color = _dayStandardColor;
            }
            if (currentHour >= StandardNightTime.startHour && currentHour < StandardNightTime.endHour)
            {
                _mapImage.color = _nightStandardColor;
            }

            if (currentHour >= StandardNightTime.endHour && currentHour < StandardDayTime.startHour)
            {
                float totalMinutes = (StandardDayTime.startHour - StandardNightTime.endHour) * 60;
                float minutesElapsed = currentMinute + (currentHour - StandardNightTime.endHour) * 60;

                float mapRedTint = Mathf.Lerp(_nightStandardColor.r, _dayStandardColor.r, minutesElapsed / totalMinutes);
                float mapGreenTint = Mathf.Lerp(_nightStandardColor.g, _dayStandardColor.g, minutesElapsed / totalMinutes);
                float mapBlueTint = Mathf.Lerp(_nightStandardColor.b, _dayStandardColor.b, minutesElapsed / totalMinutes);

                _mapImage.color = new Color(mapRedTint, mapGreenTint, mapBlueTint);
            }
            if (currentHour >= StandardDayTime.endHour && currentHour < StandardNightTime.startHour)
            {
                float totalMinutes = (StandardNightTime.startHour - StandardDayTime.endHour) * 60;
                float minutesElapsed = currentMinute + (currentHour - StandardDayTime.endHour) * 60;

                float mapRedTint = Mathf.Lerp(_dayStandardColor.r, _nightStandardColor.r, minutesElapsed / totalMinutes);
                float mapGreenTint = Mathf.Lerp(_dayStandardColor.g, _nightStandardColor.g, minutesElapsed / totalMinutes);
                float mapBlueTint = Mathf.Lerp(_dayStandardColor.b, _nightStandardColor.b, minutesElapsed / totalMinutes);

                _mapImage.color = new Color(mapRedTint, mapGreenTint, mapBlueTint);
            }
        }
    }
}
