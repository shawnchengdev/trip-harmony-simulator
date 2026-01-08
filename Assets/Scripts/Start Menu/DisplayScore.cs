using TMPro;
using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _highestScoreText;

    private void Start()
    {
        string savedScore = PlayerPrefs.GetString("Highest Score");

        if (savedScore == null)
        {
            savedScore = "?";
        }
        _highestScoreText.text = savedScore;
    }
}
