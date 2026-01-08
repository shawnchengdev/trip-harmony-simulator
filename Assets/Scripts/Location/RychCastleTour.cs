using UnityEngine;

public class RychCastleTour : Location
{
    private void Start()
    {
        SetLocationStats();
    }

    private void SetLocationStats()
    {
        HappinessChange = 20; // (0 to 10)
        EnergyChange = -10;
        FullnessChange = -5; 
        BathroomUrgeChange = 0;
        NoveltyBonus = 10; // Limit to (0 to 20)

        ActivityCostPerPerson = 150;
        TimeRequired = (0, 3, 30);
        AlwaysOpen = true;
        OpenTimes = (10, 21);
        StarAmount = 4;
        HasRestroom = true;
    }
}
