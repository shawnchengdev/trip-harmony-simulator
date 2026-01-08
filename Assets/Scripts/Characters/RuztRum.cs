using UnityEngine;


public class RuztRum : Character
{
    public RuztRum()
    {
        SetCharacterDetails();
        SetCharacterStats();
        SetStartingStats();
    }

    private void SetCharacterStats()
    {
        // Full and Happy BUT needs the Bathroom often

        // Tolerances
        HappinessTolerance = 1.05f;
        EnergyTolerance = 1.05f;
        FullnessTolerance = 1.25f;
        BathroomUrgeTolerance = 0.65f;
        NoveltyTolerance = 1.15f;

        // Sensitivity
        HappinessSensitivity = 0.9f;
        EnergySensitivity = 1.1f;
        FullnessSensitivity = 1.2f;
        BathroomUrgeSensitivity = 0.85f;
        NoveltySensitivity = 1.25f;
    }

    private void SetCharacterDetails()
    {
        CharacterName = "Ruzt Rum";

        CharacterDetails["happy"] = new string[] { "I feel great!", "This is amazing!" };
        CharacterDetails["okay"] = new string[] { "I'm content.", "I feel fine." };
        CharacterDetails["low energy"] = new string[] { "I'm getting a bit tired.", "I want to sleep." };
        CharacterDetails["low fullness"] = new string[] { "I'm hungry!", "Can we go eat?" };
        CharacterDetails["high bathroomUrge"] = new string[] { "WHERE IS THE RESTROOM!", "IS THERE A RESTROOM NEARBY?" };
        CharacterDetails["low novelty"] = new string[] { "I want to go someplace new...", "I'm getting bored of the same places." };
    }
}