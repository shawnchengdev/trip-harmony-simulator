using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class Location : MonoBehaviour
{
    // Stats Changes
    protected int HappinessChange;
    protected int EnergyChange;
    protected int FullnessChange;
    protected int BathroomUrgeChange;
    protected int NoveltyBonus; // Limit to (0 to 20)

    // Current Stat Changes
    private int _noveltyChangeNow;
    protected int NoveltyStarBonus;

    // Go Button
    private bool _goButtonDisabled = false;

    // Location Information
    protected int ActivityCostPerPerson;
    protected (int day, int hour, int minute) TimeRequired;
    protected (int openHour, int closeHour) OpenTimes;
    protected bool AlwaysOpen;
    protected int StarAmount;
    protected bool HasRestroom;
    private int _beenTimes = 0;
    private const float _goLocationWaitTime = 1;
    private const int _startNoveltyRatio = 10;

    [Header("Script References")]
    [SerializeField] private CharacterManager _characterManager;
    [SerializeField] private TimeCounter _timeCounter;
    [SerializeField] private BudgetMoney _budgetMoney;
    [SerializeField] private AudioManager _audioManager;

    [Header("Object References")]
    [SerializeField] private TextMeshProUGUI _beenBeforeText;

    // Go Button Information
    [SerializeField] private Image _goButtonImage;
    [SerializeField] private Button _goButton;


    #region Methods
    public void CheckEnoughMoney()
    {
        if (!_budgetMoney.CanSpendBudgetPerPerson(ActivityCostPerPerson))
        {
            DisableGoButton();
        }
    }

    public void GoButtonPressed()
    {
        if (!_timeCounter.GoButtonPressed)
        {
            StartCoroutine(WaitPerformButtonPress());
            _audioManager.PlaySFX(_audioManager.TimeSkip);
        }
    }

    IEnumerator WaitPerformButtonPress()
    {
        _timeCounter.GoButtonPressed = true;
        _timeCounter.TimeSkip(TimeRequired.day, TimeRequired.hour, TimeRequired.minute);
        
        yield return new WaitForSeconds(_goLocationWaitTime + 0.25f);
        
        _timeCounter.GoButtonPressed = false;
        _budgetMoney.SpendBudgetPerPerson(ActivityCostPerPerson);
        CheckBeenBefore();
        UpdateCharacterStats();
        _characterManager.UpdateAverageStatsUI();
    }

    private void CheckBeenBefore()
    {
        if (_beenTimes == 0)
        {
            _beenBeforeText.text = "*Been To";
            _noveltyChangeNow = NoveltyBonus + StarAmount * _startNoveltyRatio;
        }
        _noveltyChangeNow = NoveltyBonus + StarAmount * _startNoveltyRatio / (_beenTimes * 2 + 1);
        _beenTimes++;
    }

    private void UpdateCharacterStats()
    {
        if (HasRestroom)
        {
            _characterManager.AllCharacterResetRestroomUrge();
        }

        _characterManager.ChangeAllCharacterStats("happiness", HappinessChange);
        _characterManager.ChangeAllCharacterStats("energy", EnergyChange);
        _characterManager.ChangeAllCharacterStats("fullness", FullnessChange);
        _characterManager.ChangeAllCharacterStats("bathroomUrge", BathroomUrgeChange);
        _characterManager.ChangeAllCharacterStats("novelty", _noveltyChangeNow);
    }

    public void CheckOpenStatus(int currentHour)
    {
        if (!AlwaysOpen)
        {
            if (currentHour >= OpenTimes.openHour && currentHour < OpenTimes.closeHour)
            {
                if (_goButtonDisabled)
                {
                    EnableGoButton();
                }
            }
            else
            {
                if (!_goButtonDisabled)
                {
                    DisableGoButton();
                }
            }
        }
    }

    private void DisableGoButton()
    {
        _goButtonImage.color = Color.red;
        _goButton.interactable = false;
        _goButtonDisabled = true;
    }

    private void EnableGoButton()
    {
        _goButtonImage.color = Color.green;
        _goButton.interactable = true;
        _goButtonDisabled = false;
    }
    #endregion
}