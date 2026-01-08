using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DisplayAvgStats : MonoBehaviour
{
    [Header("Average Stats References")]
    [SerializeField] private TextMeshProUGUI _avgHappinessText;
    [SerializeField] private TextMeshProUGUI _avgEnergyText;
    [SerializeField] private TextMeshProUGUI _avgFullnessText;
    [SerializeField] private TextMeshProUGUI _avgBathroomUrgeText;

    [SerializeField] private Image _happinessWarning;
    [SerializeField] private Image _energyWarning;
    [SerializeField] private Image _fullnessWarning;
    [SerializeField] private Image _bathroomUrgeWarning;

    [Header("Change Stats References")]
    [SerializeField] private TextMeshProUGUI _changeHappinessText;
    [SerializeField] private Animator _changeHappinessAnimator;
    [SerializeField] private TextMeshProUGUI _changeEnergyText;
    [SerializeField] private Animator _changeEnergyAnimator;
    [SerializeField] private TextMeshProUGUI _changeFullnessText;
    [SerializeField] private Animator _changeFullnessAnimator;
    [SerializeField] private TextMeshProUGUI _changeBathroomUrgeText;
    [SerializeField] private Animator _changeBathroomUrgeAnimator;

    [SerializeField] private Color _positiveColor;
    [SerializeField] private Color _negativeColor;

    [SerializeField] private AudioManager _audioManager;


    private void Start()
    {
        _happinessWarning.enabled = false;
        _energyWarning.enabled = false;
        _fullnessWarning.enabled = false;
        _bathroomUrgeWarning.enabled = false;

        _changeHappinessText.enabled = false;
        _changeEnergyText.enabled = false;
        _changeFullnessText.enabled = false;
        _changeBathroomUrgeText.enabled = false;
    }

    public void ChangeStatAnimation(int happinessDifference, int energyDifference, int fullnessDifference, int bathroomUrgeDifference)
    {
        _audioManager.PlaySFX(_audioManager.StatsProgression);
        int[] changeStats = { happinessDifference, energyDifference, fullnessDifference, bathroomUrgeDifference };
        TextMeshProUGUI[] changeText = { _changeHappinessText, _changeEnergyText, _changeFullnessText, _changeBathroomUrgeText };
        Animator[] textAnimator = { _changeHappinessAnimator, _changeEnergyAnimator, _changeFullnessAnimator, _changeBathroomUrgeAnimator };

        for (int index = 0; index < changeStats.Length; index++)
        {
            if ((index != 3 && changeStats[index] > 0) || (index == 3 && changeStats[index] < 0))
            {
                changeText[index].color = _positiveColor;
            }
            else
            {
                changeText[index].color = _negativeColor;
            }

            if (changeStats[index] != 0)
            {
                if (changeStats[index] > 0)
                {
                    changeText[index].text = "+" + changeStats[index].ToString();
                }
                else
                {
                    changeText[index].text = changeStats[index].ToString();
                }

                changeText[index].enabled = true;
                textAnimator[index].SetTrigger("change");
            }
        }
    }

    public void UpdateStatTexts(int avgHappiness, int avgEnergy, int avgFullness, int avgBathroomUrge)
    {
        _avgHappinessText.text = avgHappiness.ToString();
        _avgEnergyText.text = avgEnergy.ToString();
        _avgFullnessText.text = avgFullness.ToString();
        _avgBathroomUrgeText.text = avgBathroomUrge.ToString();
    }

    public void DisplayWarning(string statName, bool enable)
    {
        switch (statName)
        {
            case "happiness":
                _happinessWarning.enabled = enable;
                break;

            case "energy":
                _energyWarning.enabled = enable;
                break;

            case "fullness":
                _fullnessWarning.enabled = enable;
                break;

            case "bathroomUrge":
                _bathroomUrgeWarning.enabled = enable;
                break;

            default:
                Debug.Log("ERROR, STAT NAME NOT FOUND WHEN DISPLAYING WARNINGS.");
                break;
        }
    }
}
