using UnityEngine;


public class BlueLu : Character
{
    public BlueLu()
    {
        SetCharacterDetails();
        SetCharacterStats();
        SetStartingStats();
    }

    private void SetCharacterStats()
    {
        // Full and Happy BUT needs the Bathroom often

        // Tolerances
        HappinessTolerance = 0.8f;
        EnergyTolerance = 1.05f;
        FullnessTolerance = 1.05f;
        BathroomUrgeTolerance = 0.95f;
        NoveltyTolerance = 0.9f;

        // Sensitivity
        HappinessSensitivity = 0.85f;
        EnergySensitivity = 1.1f;
        FullnessSensitivity = 1.1f;
        BathroomUrgeSensitivity = 0.85f;
        NoveltySensitivity = 1f;
    }

    private void SetCharacterDetails()
    {
        CharacterName = "Blue Lu";

        CharacterDetails["happy"] = new string[] { "Feeling better than before.", "Woah. I'm happy." };
        CharacterDetails["okay"] = new string[] { "Less joyful then usual.", "The current situation is... tolerable." };
        CharacterDetails["low energy"] = new string[] { "Less energetic now.", "An energy drink or someplace to sleep please?" };
        CharacterDetails["low fullness"] = new string[] { "Feeling a bit hungry now.", "Some local food would be nice?" };
        CharacterDetails["high bathroomUrge"] = new string[] { "I'm not gonna be able to use the restroom aren't I.", "Seriously, the destination better have a restroom." };
        CharacterDetails["low novelty"] = new string[] { "wow... its now the boring tour I expected.", "Did I really have to ask you to bring us to cool places." };
    }
}