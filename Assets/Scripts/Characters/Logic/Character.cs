using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;


public abstract class Character
{
    #region Character Stats
    // Stats
    public int _happiness { get; private set; }
    public int _energy { get; private set; }
    public int _fullness { get; private set; }
    public int _bathroomUrge { get; private set; }
    public int _novelty { get; private set; }
    // MAYBE LATER ADD A NOVELTY STAT WHICH ENCOURAGES PLAYERS TO GO TO NEW PLACES

    // Tolerance, between (0, 2), CUSTOMIZABLE
    // High Tolerance means you less likely to be negatively impacted
    protected float HappinessTolerance;
    protected float EnergyTolerance;
    protected float FullnessTolerance;
    protected float BathroomUrgeTolerance;
    protected float NoveltyTolerance;

    // Sensitivity, between (0, 2), CUSTOMIZABLE
    // High Sensitivity means you more likely to be positively influenced
    protected float HappinessSensitivity;
    protected float EnergySensitivity;
    protected float FullnessSensitivity;
    protected float BathroomUrgeSensitivity;
    protected float NoveltySensitivity;

    // Happy Bounds
    private (int lowerBound, int upperBound) _energyHappyBounds = (25, 100);
    private (int lowerBound, int upperBound) _fullnessHappyBounds = (20, 100);
    private (int lowerBound, int upperBound) _bathroomUrgeHappyBounds = (0, 85);
    private (int lowerBound, int upperBound) _noveltyHappyBounds = (35, 100);

    // Randomness
    private const int ChangeHappinessRandomness = 1;
    private const int RecoverHappinessRandomness = 3;
    private const int ChangeEnergyRandomness = 2;
    private const int RecoverEnergyRandomness = 5;
    private const int ChangeFullnessRandomness = 2;
    private const int ChangeRestroomUrgeRandomness = 3;
    private const int AfterRestroomUrgeRandomness = 5;
    private const int ChangeNoveltyRandomness = 5;

    // Stats sleep recovery, CUSTOMIZABLE
    protected int SleepHappinessRecovery;
    protected int SleepEnergyRecovery;

    // After Restroom Urge
    private const int AfterRestroomUrge = 10;

    // Stats drain with time
    private const int LowEnergyDrain = 0;
    private const int HighEnergyDrain = -3;
    private const int LowFullnessDrain = -1;
    private const int HighFullnessDrain = -5;
    private const int LowBathroomUrgeIncrease = 3;
    private const int HighBathroomUrgeIncrease = 7;
    private const int LowNoveltyDrain = -15;
    private const int HighNoveltyDrain = -25;

    // Stats impact on happiness
    public int _energyHappinessImpact { get; private set; }
    public int _fullnessHappinessImpact { get; private set; }
    public int _bathroomUrgeHappinessImpact { get; private set; }
    public int _noveltyHappinessImpact { get; private set; }

    // Stats potency to affect happiness, how much of a stat difference is needed for a 1 unit of change in happiness
    private const float EnergyPotency = 5;
    private const float FullnessPotency = 3;
    private const float BathroomUrgePotency = 1.5f;
    private const float NoveltyPotency = 4;

    // Boost happiness
    private const int NoImpactHappinessBoost = 1;
    #endregion


    #region Character Information
    public string CharacterName { get; protected set; }
    public Dictionary<string, string[]> CharacterDetails { get; protected set; } = new Dictionary<string, string[]>
    {
        { "happy", null},
        { "okay", null },
        { "low energy", null },
        { "low fullness", null },
        { "high bathroomUrge", null },
        { "low novelty", null }
    };

    public string GetCharacterDetails(string characterStatus)
    {
        int randomIndex = UnityEngine.Random.Range(0, CharacterDetails[characterStatus].Length);
        return CharacterDetails[characterStatus][randomIndex];
    }
    #endregion


    #region General Methods
    public void SetStartingStats()
    {
        SetStartingHappiness();
        SetStartingEnergy();
        SetStartingFullness();
        SetStartingRestroomUrge();
        SetStartingNovelty();
    }

    public void ProgressStats()
    {
        TimeEnergyDrain();
        TimeFullnessDrain();
        TimeRestroomUrgeDrain();
        TimeNoveltyDrain();
        UpdateHappiness();
    }

    private void ChangeStat(string statName, int changeAmount, int randomness)
    {
        int randomChange = UnityEngine.Random.Range(-randomness, randomness);

        switch (statName) 
        {
            case "happiness":
                if (changeAmount > 0)
                {
                    _happiness = (int)math.clamp(_happiness + randomChange + changeAmount * HappinessSensitivity, 0, 100);
                }
                else
                {
                    _happiness = (int)math.clamp(_happiness + randomChange + changeAmount / HappinessTolerance, 0, 100);
                }
                break;

            case "energy":
                if (changeAmount > 0)
                {
                    _energy = (int)math.clamp(_energy + randomChange + changeAmount * EnergySensitivity, 0, 100);
                }
                else
                {
                    _energy = (int)math.clamp(_energy + randomChange + changeAmount / EnergyTolerance, 0, 100);
                }
                UpdateEnergyImpact();
                break;

            case "fullness":
                if (changeAmount > 0)
                {
                    _fullness = (int)math.clamp(_fullness + randomChange + changeAmount * FullnessSensitivity, 0, 100);
                }
                else
                {
                    _fullness = (int)math.clamp(_fullness + randomChange + changeAmount / FullnessTolerance, 0, 100);
                }
                UpdateFullnessImpact();
                break;

            case "bathroomUrge":
                if (changeAmount > 0)
                {
                    _bathroomUrge = (int)math.clamp(_bathroomUrge + randomChange + changeAmount / BathroomUrgeTolerance, 0, 100);
                }
                else
                {
                    _bathroomUrge = (int)math.clamp(_bathroomUrge + randomChange + changeAmount * BathroomUrgeSensitivity, 0, 100);
                }
                UpdateBathroomUrgeImpact();
                break;

            case "novelty":
                if (changeAmount > 0)
                {
                    _novelty = (int)math.clamp(_novelty + randomChange + changeAmount * NoveltySensitivity, 0, 100);
                }
                else
                {
                    _novelty = (int)math.clamp(_novelty + randomChange + changeAmount / NoveltyTolerance, 0, 100);
                }
                UpdateNoveltyImpact();
                break;

            default:
                Debug.Log("ERROR, STAT NAME NOT FOUND WHEN CHANGING STATS.");
                break;
        }
    }

    private void SetStat(string statName, int newValue, int randomness)
    {
        int randomChange = UnityEngine.Random.Range(-randomness, randomness);
        switch (statName)
        {
            case "happiness":
                _happiness = math.clamp(newValue + randomChange, 0, 100);
                break;

            case "energy":
                _energy = math.clamp(newValue + randomChange, 0, 100);
                UpdateEnergyImpact();
                break;

            case "fullness":
                _fullness = math.clamp(newValue + randomChange, 0, 100);
                UpdateFullnessImpact();
                break;

            case "bathroomUrge":
                _bathroomUrge = math.clamp(newValue + randomChange, 0, 100);
                UpdateBathroomUrgeImpact();
                break;

            case "novelty":
                _novelty = math.clamp(newValue + randomChange, 0, 100);
                UpdateNoveltyImpact();
                break;

            default:
                Debug.Log("ERROR, STAT NAME NOT FOUND WHEN SETTING STATS.");
                break;
        }
    }

    private void DrainStat(string statName, float tolerance, int lowDrainAmount, int highDrainAmount)
    {
        int randomEnergyDrain;
        if (tolerance > 1)
        {
            if (tolerance - 1 > UnityEngine.Random.value)
            {
                randomEnergyDrain = lowDrainAmount;
            }
            else
            {
                randomEnergyDrain = UnityEngine.Random.Range(highDrainAmount, lowDrainAmount);
            }
        }
        else if (tolerance < 1)
        {
            if (tolerance < UnityEngine.Random.value)
            {
                randomEnergyDrain = highDrainAmount;
            }
            else
            {
                randomEnergyDrain = UnityEngine.Random.Range(highDrainAmount, lowDrainAmount);
            }
        }
        else
        {
            randomEnergyDrain = UnityEngine.Random.Range(highDrainAmount, lowDrainAmount);
        }

        ChangeStat(statName, randomEnergyDrain, 0);
    }

    private void ImpactHappiness(string statName, int statValue, int lowerBound, int upperBound, float statPotency)
    {
        int newImpactValue = 0;
        if (statValue < lowerBound)
        {
            newImpactValue = -(int)(math.abs(lowerBound - statValue) / statPotency);
        }
        else if (statValue > upperBound)
        {
            newImpactValue = -(int)(math.abs(upperBound - statValue) / statPotency);
        }

        switch (statName)
        {
            case "energy":
                _energyHappinessImpact = newImpactValue;
                break;

            case "fullness":
                _fullnessHappinessImpact = newImpactValue;
                break;

            case "bathroomUrge":
                _bathroomUrgeHappinessImpact = newImpactValue;
                break;

            case "novelty":
                _noveltyHappinessImpact = newImpactValue;
                break;

            default:
                Debug.Log("ERROR, STAT NAME NOT FOUND WHEN IMPACTING STATS.");
                break;
        }
    }
    #endregion


    #region Happiness Methods
    private void SetStartingHappiness()
    {
        int startingHappiness = (int)(85 * (0.5 * HappinessTolerance + 0.5));
        SetStat("happiness", startingHappiness, ChangeHappinessRandomness);
    }

    private void SleepRecoverHappiness()
    {
        ChangeStat("happiness", (int)(SleepHappinessRecovery * HappinessTolerance), RecoverHappinessRandomness);
    }

    public void ChangeHappiness(int happinessAmount)
    {
        ChangeStat("happiness", happinessAmount, ChangeHappinessRandomness);
    }

    private void UpdateHappiness()
    {
        int negativeImpacts = _energyHappinessImpact + _fullnessHappinessImpact + _bathroomUrgeHappinessImpact + _noveltyHappinessImpact;

        if (negativeImpacts < 0)
        {
            ChangeStat("happiness", (int)(negativeImpacts / HappinessTolerance), ChangeHappinessRandomness);
        }
        else
        {
            ChangeStat("happiness", (int)(NoImpactHappinessBoost * HappinessTolerance), ChangeHappinessRandomness);
        }
    }
    #endregion


    #region Energy Methods
    private void SetStartingEnergy()
    {
        int startingEnergy = (int)(100 * (0.5 * EnergyTolerance + 0.5));
        SetStat("energy", startingEnergy, ChangeEnergyRandomness);
    }

    private void RestRecoverEnergy()
    {
        ChangeStat("energy", SleepEnergyRecovery, RecoverEnergyRandomness);
    }

    public void ChangeEnergy(int energyAmount)
    {
        ChangeStat("energy", energyAmount, ChangeEnergyRandomness);
    }

    private void TimeEnergyDrain()
    {
        DrainStat("energy", EnergyTolerance, LowEnergyDrain, HighEnergyDrain);
    }

    private void UpdateEnergyImpact()
    {
        ImpactHappiness("energy", _energy, _energyHappyBounds.lowerBound, _energyHappyBounds.upperBound, EnergyPotency);
    }
    #endregion


    #region Fullness Methods
    private void SetStartingFullness()
    {
        int startingFullness = (int)(100 * (0.5 * FullnessTolerance + 0.5));
        SetStat("fullness", startingFullness, ChangeFullnessRandomness);
    }

    public void ChangeFullness(int fullnessAmount)
    {
        ChangeStat("fullness", fullnessAmount, ChangeFullnessRandomness);
    }

    private void TimeFullnessDrain()
    {
        DrainStat("fullness", FullnessTolerance, LowFullnessDrain, HighFullnessDrain);
    }

    private void UpdateFullnessImpact()
    {
        ImpactHappiness("fullness", _fullness, _fullnessHappyBounds.lowerBound, _fullnessHappyBounds.upperBound, FullnessPotency);
    }
    #endregion


    #region BathroomUrge Methods
    private void SetStartingRestroomUrge()
    {
        int startingRestroomUrge = (int)(25 * (1.5 - 0.5 * BathroomUrgeTolerance));
        SetStat("bathroomUrge", startingRestroomUrge, ChangeRestroomUrgeRandomness);
    }

    public void ChangeRestroomUrge(int urgeAmount) 
    {
        ChangeStat("bathroomUrge", urgeAmount, ChangeRestroomUrgeRandomness);
    }

    public void ResetRestroomUrge()
    {
        SetStat("bathroomUrge", AfterRestroomUrge, AfterRestroomUrgeRandomness);
    }

    private void TimeRestroomUrgeDrain()
    {
        DrainStat("bathroomUrge", BathroomUrgeTolerance, LowBathroomUrgeIncrease, HighBathroomUrgeIncrease);
    }

    private void UpdateBathroomUrgeImpact()
    {
        ImpactHappiness("bathroomUrge", _bathroomUrge, _bathroomUrgeHappyBounds.lowerBound, _bathroomUrgeHappyBounds.upperBound, BathroomUrgePotency);
    }
    #endregion


    #region Novelty Methods
    private void SetStartingNovelty()
    {
        int startingNovelty = (int)(100 * (0.5 * NoveltyTolerance + 0.5));
        SetStat("novelty", startingNovelty, ChangeNoveltyRandomness);
    }

    public void ChangeNovelty(int noveltyAmount)
    {
        ChangeStat("novelty", noveltyAmount, ChangeNoveltyRandomness);
    }

    private void TimeNoveltyDrain()
    {
        DrainStat("novelty", NoveltyTolerance, LowNoveltyDrain, HighNoveltyDrain);
    }

    private void UpdateNoveltyImpact()
    {
        ImpactHappiness("novelty", _novelty, _noveltyHappyBounds.lowerBound, _noveltyHappyBounds.upperBound, NoveltyPotency);
    }
    #endregion
}
