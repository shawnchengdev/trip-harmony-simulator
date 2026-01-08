using UnityEngine;


public class NovellaChi : Character
{
    public NovellaChi()
    {
        SetCharacterDetails();
        SetCharacterStats();
        SetStartingStats();
    }

    private void SetCharacterStats()
    {
        // Full and Happy BUT needs the Bathroom often

        // Tolerances
        HappinessTolerance = 1.1f;
        EnergyTolerance = 1f;
        FullnessTolerance = 1.05f;
        BathroomUrgeTolerance = 0.95f;
        NoveltyTolerance = 0.75f;

        // Sensitivity
        HappinessSensitivity = 0.95f;
        EnergySensitivity = 1.05f;
        FullnessSensitivity = 0.95f;
        BathroomUrgeSensitivity = 1.05f;
        NoveltySensitivity = 0.9f;
    }

    private void SetCharacterDetails()
    {
        CharacterName = "Novella Chi";

        CharacterDetails["happy"] = new string[] { "Currently jubilant!", "Joyous is how I am feeling." };
        CharacterDetails["okay"] = new string[] { "Mediocrity at its finest.", "A passable experience." };
        CharacterDetails["low energy"] = new string[] {"Soo drained right now.", "I have expended most of my energy, may I rest now?" };
        CharacterDetails["low fullness"] = new string[] { "I require sustenance.", "Something to consume please?" };
        CharacterDetails["high bathroomUrge"] = new string[] { "Lavatory nearby?", "I demand a location with a washroom." };
        CharacterDetails["low novelty"] = new string[] { "I would like to request to go to someplace new.", "I crave for unfamiliarity." };
    }
}