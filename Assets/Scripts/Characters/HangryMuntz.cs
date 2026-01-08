using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class HangryMuntz: Character
{
    public HangryMuntz()
    {
        SetCharacterDetails();
        SetCharacterStats();
        SetStartingStats();
    }

    private void SetCharacterStats()
    {
        // Happy and Energetic But very Hungry

        // Tolerances
        HappinessTolerance = 1.1f;
        EnergyTolerance = 1.15f;
        FullnessTolerance = 0.75f;
        BathroomUrgeTolerance = 0.9f;
        NoveltyTolerance = 0.9f;

        // Sensitivity
        HappinessSensitivity = 1;
        EnergySensitivity = 1.1f;
        FullnessSensitivity = 0.8f;
        BathroomUrgeSensitivity = 1.1f;
        NoveltySensitivity = 1.15f;
    }

    private void SetCharacterDetails()
    {
        CharacterName = "Hangry Muntz";

        CharacterDetails["happy"] = new string[] { "Me Happy.", "Muntz Glimmers With Joy." };
        CharacterDetails["okay"] = new string[] { "Me Just Okay.", "Muntz Releases A Big Sigh." };
        CharacterDetails["low energy"] = new string[] { "Me Tired", "Muntz Is Conserving Energy By Laying On The Ground." };
        CharacterDetails["low fullness"] = new string[] { "Me Hungry!", "Muntz Desperately Searches For Nearby Restaurants." };
        CharacterDetails["high bathroomUrge"] = new string[] { "Me Needs A Restroom.", "Muntz Questions Passerby For A Nearby Restroom." };
        CharacterDetails["low novelty"] = new string[] { "Me Bored.", "Muntz Frantically Looks Around For Anything Interesting." };
    }
}
