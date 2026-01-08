using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BudgetMoney : MonoBehaviour
{
    private const int _budgetPerPersonPerDay = 325;
    private int _currentBudget;

    [Header("Script Reference")]
    [SerializeField] private TimeCounter _timeCounter;
    [SerializeField] private CharacterManager _characterManager;
    [SerializeField] private LocationManager _locationManager;

    [Header("Object Reference")]
    [SerializeField] private TextMeshProUGUI _budgetText;
    [SerializeField] private Image _BudgetWarningImage;
    [SerializeField] private TextMeshProUGUI _spendText;
    [SerializeField] private Animator _spendTextAnimator;


    void Start()
    {
        _currentBudget = _budgetPerPersonPerDay * _timeCounter.TotalDaysAmount * _characterManager._characterAmount;
        _BudgetWarningImage.enabled = false;
        _spendText.enabled = false;
        UpdateBudgetUI();
    }

    public bool CanSpendBudgetPerPerson(int spendAmountPerPerson)
    {
        return _currentBudget - spendAmountPerPerson * _characterManager._characterAmount >= 0;
    }

    public void SpendBudgetPerPerson(int spendAmountPerPerson)
    {
        if (CanSpendBudgetPerPerson(spendAmountPerPerson))
        {
            int spendMoneyAmount = spendAmountPerPerson * _characterManager._characterAmount;
            _currentBudget -= spendMoneyAmount;

            SpendBudgetUI(spendMoneyAmount);
            UpdateBudgetUI();
            CheckBudgetWarning();
            _locationManager.CheckEnoughMoneyForLocations();
        }
    }

    private void SpendBudgetUI(int spendMoney)
    {
        _spendText.enabled = true;
        _spendText.text = $"-${spendMoney}";
        _spendTextAnimator.SetTrigger("change");
    }

    private void UpdateBudgetUI()
    {
        _budgetText.text = $"${_currentBudget}";
    }

    private void CheckBudgetWarning()
    {
        if (_currentBudget < _budgetPerPersonPerDay * (_timeCounter.TotalDaysAmount - (_timeCounter.Day + 1.5)) * _characterManager._characterAmount)
        {
            _BudgetWarningImage.enabled = true;
        }
        else
        {
            _BudgetWarningImage.enabled = false;
        }
    }
}
