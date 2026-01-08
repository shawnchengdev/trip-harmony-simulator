using UnityEngine;


public class CafiNik : Character
{
    public CafiNik()
    {
        SetCharacterDetails();
        SetCharacterStats();
        SetStartingStats();
    }

    private void SetCharacterStats()
    {
        // Full and Low BathroomUrge But not Happy and low Energy

        // Tolerances
        HappinessTolerance = 0.9f;
        EnergyTolerance = 0.85f;
        FullnessTolerance = 1.20f;
        BathroomUrgeTolerance = 1.15f;
        NoveltyTolerance = 0.8f;

        // Sensitivity
        HappinessSensitivity = 1;
        EnergySensitivity = 0.5f;
        FullnessSensitivity = 1.15f;
        BathroomUrgeSensitivity = 0.9f;
        NoveltySensitivity = 0.9f;
    }

    private void SetCharacterDetails()
    {
        CharacterName = "Cafi Nik";

        CharacterDetails["happy"] = new string[] { "I'M SO HAPPY!?!", "FEELING GREAT!" };
        CharacterDetails["okay"] = new string[] { "I'M DOING GREAT!", "JUST OKAY!" };
        CharacterDetails["low energy"] = new string[] { "huh...  what...", "tired..." };
        CharacterDetails["low fullness"] = new string[] { "SOSOSOO HUNGRY!", "NOW HUNGRY!" };
        CharacterDetails["high bathroomUrge"] = new string[] { "I DRANK TOOOO MUCH!", "RESTROOM QUICKLY!!!" };
        CharacterDetails["low novelty"] = new string[] { "I'M SOSOSO BOOOORED!", "VERY BORED?" };
    }
}
