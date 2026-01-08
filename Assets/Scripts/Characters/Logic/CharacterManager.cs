using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    private List<Type> _possibleCharacters = new List<Type>{
        typeof(HangryMuntz),
        typeof(RuztRum),
        typeof(CafiNik),
        typeof(BlueLu),
        typeof(NovellaChi)
    };
    [Header("MAKE SURE THE ORDER MATCHES _possibleCharacters")]
    [SerializeField] private List<Sprite> AllCharacterImages;
    private List<Sprite> _currentCharacterImages = new List<Sprite>();
    public List<Character> CharacterList { get; private set; }
    private List<Dictionary<string, Transform>> _characterViewObjectList = new List<Dictionary<string, Transform>>();

    private int _averageHappiness;
    private int _averageEnergy;
    private int _averageFullness;
    private int _averageBathroomUrge;

    private const int _endGameHappinessThreshold = 0;
    private const int _warningHappinessThreshold = 15;
    public int _characterAmount { get; private set; } = 3;

    [Header("Object References")]
    [SerializeField] private GameObject _characterViewPrefab;
    [SerializeField] private Transform _scrollableContentTransform;

    [Header("Script References")]
    [SerializeField] private DisplayAvgStats _displayAvgStats;
    [SerializeField] private GameReview _gameReview;
    [SerializeField] private GameObject _startMenu;


    void Start()
    {
        CharacterList = new List<Character>();
        _characterViewObjectList = new List<Dictionary<string, Transform>>();


        for (int index = 0; index < _characterAmount; index++)
        {
            int randomIndex = UnityEngine.Random.Range(0, _possibleCharacters.Count);
            
            _characterViewObjectList.Add(new Dictionary<string, Transform>());
            CharacterList.Add((Character)Activator.CreateInstance(_possibleCharacters[randomIndex]));
            _possibleCharacters.RemoveAt(randomIndex);

            // Ensures the order between CharacterList
            _currentCharacterImages.Add(AllCharacterImages[randomIndex]);
            AllCharacterImages.RemoveAt(randomIndex);
        }
        AddCharacterToMenu();
    }


    #region Methods
    private void GetAverageStats()
    {
        int happinessSum = 0;
        int energySum = 0;
        int fullnessSum = 0;
        int bathroomUrgeSum = 0;

        foreach (Character character in CharacterList)
        {
            happinessSum += character._happiness;
            energySum += character._energy;
            fullnessSum += character._fullness;
            bathroomUrgeSum += character._bathroomUrge;
        }
        if (!_startMenu.activeSelf)
        {
            _displayAvgStats.ChangeStatAnimation(happinessSum / _characterAmount - _averageHappiness, energySum / _characterAmount - _averageEnergy, fullnessSum / _characterAmount - _averageFullness, bathroomUrgeSum / _characterAmount - _averageBathroomUrge);
        }

        _averageHappiness = happinessSum / _characterAmount;
        _averageEnergy = energySum / _characterAmount;
        _averageFullness = fullnessSum / _characterAmount;
        _averageBathroomUrge = bathroomUrgeSum / _characterAmount;   

        if (_averageHappiness <= _endGameHappinessThreshold)
        {
            _gameReview._endedEarly = true;
            _gameReview.EndGame();
        }
    }

    public void UpdateAll()
    {
        ProgressAllCharacterStats();
        UpdateAverageStatsUI();
        UpdateCharacterMenuStats();
    }

    public void ProgressAllCharacterStats()
    {
        foreach (Character character in CharacterList)
        {
            character.ProgressStats();
        }
    }

    public void UpdateAverageStatsUI()
    {
        GetAverageStats();
        _displayAvgStats.UpdateStatTexts(_averageHappiness, _averageEnergy, _averageFullness, _averageBathroomUrge);
        UpdateCharacterMenuStats();
        DisplayStatsWarning();
    }

    private void DisplayStatsWarning()
    {
        int energyImpactSum = 0;
        int fullnessImpactSum = 0;
        int bathroomUrgeImpactSum = 0;

        for (int index = 0; index < CharacterList.Count; index++)
        {
            energyImpactSum += CharacterList[index]._energyHappinessImpact;
            fullnessImpactSum += CharacterList[index]._fullnessHappinessImpact;
            bathroomUrgeImpactSum += CharacterList[index]._bathroomUrgeHappinessImpact;
            UpdateAllCharacterDetails(new string[] { "energy", "fullness", "bathroomUrge", "novelty" }, new int[] { CharacterList[index]._energyHappinessImpact, CharacterList[index]._fullnessHappinessImpact, CharacterList[index]._bathroomUrgeHappinessImpact, CharacterList[index]._noveltyHappinessImpact }, index);
            UpdateAllCharacterWarnings(new string[] { "energy", "fullness", "bathroomUrge"}, new int[] { CharacterList[index]._energyHappinessImpact, CharacterList[index]._fullnessHappinessImpact, CharacterList[index]._bathroomUrgeHappinessImpact }, index);
        }

        _displayAvgStats.DisplayWarning("happiness", _averageHappiness <= _warningHappinessThreshold);
        _displayAvgStats.DisplayWarning("energy", energyImpactSum <= -_characterAmount);
        _displayAvgStats.DisplayWarning("fullness", fullnessImpactSum <= -_characterAmount);
        _displayAvgStats.DisplayWarning("bathroomUrge", bathroomUrgeImpactSum <= -_characterAmount);
    }

    private void AddCharacterMenuObjects(GameObject characterObject, int index)
    {
        // Text
        _characterViewObjectList[index].Add("name", characterObject.transform.Find("Name Text"));
        _characterViewObjectList[index].Add("details", characterObject.transform.Find("Details Text"));
        _characterViewObjectList[index].Add("happiness text", characterObject.transform.Find("Happiness Text"));
        _characterViewObjectList[index].Add("energy text", characterObject.transform.Find("Energy Text"));
        _characterViewObjectList[index].Add("fullness text", characterObject.transform.Find("Fullness Text"));
        _characterViewObjectList[index].Add("bathroom urge text", characterObject.transform.Find("Bathroom Urge Text"));

        // Images
        _characterViewObjectList[index].Add("character image", characterObject.transform.Find("Character Image"));
        _characterViewObjectList[index].Add("happiness image", characterObject.transform.Find("Happiness Image"));
        _characterViewObjectList[index].Add("energy image", characterObject.transform.Find("Energy Image"));
        _characterViewObjectList[index].Add("fullness image", characterObject.transform.Find("Fullness Image"));
        _characterViewObjectList[index].Add("bathroom urge image", characterObject.transform.Find("Bathroom Urge Image"));

        // Warning Images
        _characterViewObjectList[index].Add("happiness warning image", characterObject.transform.Find("Happiness Warning Image"));
        _characterViewObjectList[index].Add("energy warning image", characterObject.transform.Find("Energy Warning Image"));
        _characterViewObjectList[index].Add("fullness warning image", characterObject.transform.Find("Fullness Warning Image"));
        _characterViewObjectList[index].Add("bathroom urge warning image", characterObject.transform.Find("Bathroom Urge Warning Image"));
    }

    private void AddCharacterToMenu()
    {
        for (int index = 0; index < _characterAmount; index++)
        {
            GameObject characterViewObject = Instantiate(_characterViewPrefab);
            characterViewObject.transform.SetParent(_scrollableContentTransform);
            characterViewObject.transform.localScale = Vector3.one;
            AddCharacterMenuObjects(characterViewObject, index);
        }

        SetCharacterDetails();
    }

    private void SetCharacterDetails()
    {
        for (int index = 0; index < _characterAmount; index++)
        {
            _characterViewObjectList[index]["name"].GetComponent<TextMeshProUGUI>().text = CharacterList[index].CharacterName;
            _characterViewObjectList[index]["character image"].GetComponent<Image>().sprite = _currentCharacterImages[index];
        }
        UpdateCharacterMenuStats();
    }

    private void UpdateCharacterMenuStats()
    {
        for (int index = 0; index < _characterAmount; index++)
        {
            _characterViewObjectList[index]["happiness text"].GetComponent<TextMeshProUGUI>().text = CharacterList[index]._happiness.ToString();
            _characterViewObjectList[index]["energy text"].GetComponent<TextMeshProUGUI>().text = CharacterList[index]._energy.ToString();
            _characterViewObjectList[index]["fullness text"].GetComponent<TextMeshProUGUI>().text = CharacterList[index]._fullness.ToString();
            _characterViewObjectList[index]["bathroom urge text"].GetComponent<TextMeshProUGUI>().text = CharacterList[index]._bathroomUrge.ToString();
        }
    }

    public void ChangeAllCharacterStats(string statName, int changeAmount)
    {
        foreach (Character character in CharacterList)
        {
            switch (statName)
            {
                case "happiness":
                    character.ChangeHappiness(changeAmount);
                    break;

                case "energy":
                    character.ChangeEnergy(changeAmount);
                    break;

                case "fullness":
                    character.ChangeFullness(changeAmount);
                    break;

                case "bathroomUrge":
                    character.ChangeRestroomUrge(changeAmount);
                    break;

                case "novelty":
                    character.ChangeNovelty(changeAmount);
                    break;

                default:
                    Debug.Log("ERROR, STAT NAME NOT FOUND WHEN CHANGING ALL CHARACTER STATS.");
                    break;
            }
        }
    }

    public void AllCharacterResetRestroomUrge()
    {
        foreach (Character character in CharacterList)
        {
            character.ResetRestroomUrge();
        }
    }

    private void UpdateAllCharacterWarnings(string[] statNames, int[] impactSums, int index)
    {
        string warningKeyName = "";

        for (int i = 0; i < statNames.Length; i++)
        {
            switch (statNames[i])
            {
                case "energy":
                    warningKeyName = "energy warning image";
                    break;

                case "fullness":
                    warningKeyName = "fullness warning image";
                    break;

                case "bathroomUrge":
                    warningKeyName = "bathroom urge warning image";
                    break;

                default:
                    Debug.Log("ERROR, STAT NAME NOT FOUND WHEN UPDATING ALL CHARACTER STATS WARNING IN MENU.");
                    break;
            }
            _characterViewObjectList[index][warningKeyName].gameObject.SetActive(impactSums[i] < 0);
        }
    }

    private void UpdateAllCharacterDetails(string[] statNames, int[] impactSums, int index)
    {
        int largestImpactIndex = 0;
        string characterStatus = "";

        for (int i = 1; i < statNames.Length; i++)
        {
            if (impactSums[i] > impactSums[largestImpactIndex])
            {
                largestImpactIndex = i;
            }
        }

        if (impactSums[largestImpactIndex] < -2)
        {
            switch (statNames[largestImpactIndex])
            {
                case "energy":
                    characterStatus = "low energy";
                    break;

                case "fullness":
                    characterStatus = "low fullness";
                    break;

                case "bathroomUrge":
                    characterStatus = "high bathroomUrge";
                    break;

                case "novelty":
                    characterStatus = "low novelty";
                    break;

                default:
                    Debug.Log("ERROR, STAT NAME NOT FOUND WHEN UPDATING ALL CHARACTER DETAILS.");
                    break;
            }
        }
        else
        {
            if (CharacterList[largestImpactIndex]._happiness > 50)
            {
                characterStatus = "happy";
            }
            else
            {
                characterStatus = "okay";
            }
        }

        _characterViewObjectList[index]["details"].GetComponent<TextMeshProUGUI>().text = CharacterList[index].GetCharacterDetails(characterStatus);
    }

    public void AddAllCharacterToSum(int periodMinutes)
    {
        foreach (Character character in CharacterList)
        {
            _gameReview.AddToSum(character._happiness, character._energy, character._fullness, character._bathroomUrge, periodMinutes);
        }
    }
    #endregion
}
