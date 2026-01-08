using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameReview : MonoBehaviour
{
    private int _happinessSum = 0;
    private int _energySum = 0;
    private int _fullnessSum = 0;
    private int _bathroomUrgeSum = 0;

    private int _tempPeriodMinutes = 0;
    private int _characterAmount;
    public bool _endedEarly = false;

    [SerializeField] private GameObject _gameMenu;
    [SerializeField] private GameObject _endGameMenu;
    // Text References
    [SerializeField] private TextMeshProUGUI _happinessText;
    [SerializeField] private TextMeshProUGUI _energyText;
    [SerializeField] private TextMeshProUGUI _fullnessText;
    [SerializeField] private TextMeshProUGUI _bathroomUrgeText;
    [SerializeField] private TextMeshProUGUI _finalScoreText;
    [SerializeField] private CharacterManager _characterManager;
    // Music
    [SerializeField] private AudioManager _audioManager;


    public void EndGame()
    {
        _gameMenu.SetActive(false);
        _endGameMenu.SetActive(true);
        SetReviewStats();
        SetFinalScore();
        _audioManager.PlayMusic(_audioManager.ReviewSong);
    }

    public void AddToSum(int happiness, int energy, int fullness, int bathroomUrge, int periodMinutes)
    {
        _tempPeriodMinutes += periodMinutes;

        _happinessSum += happiness * periodMinutes;
        _energySum += energy * periodMinutes;
        _fullnessSum += fullness * periodMinutes;
        _bathroomUrgeSum += bathroomUrge * periodMinutes;
    }

    private void SetReviewStats()
    {
        _characterAmount = _characterManager._characterAmount;

        _happinessText.text = (_happinessSum / (_tempPeriodMinutes * _characterAmount)).ToString();
        _energyText.text = (_energySum / (_tempPeriodMinutes * _characterAmount)).ToString();
        _fullnessText.text = (_fullnessSum / (_tempPeriodMinutes * _characterAmount)).ToString();
        _bathroomUrgeText.text = (_bathroomUrgeSum / (_tempPeriodMinutes * _characterAmount)).ToString();
    }

    private void SetFinalScore()
    {
        string finalScore;
        if (!_endedEarly)
        {
            finalScore = GetFinalScore(_happinessSum / (_tempPeriodMinutes * _characterAmount));
        }
        else
        {
            finalScore = "F-";
        }
        _finalScoreText.text = finalScore;

        print($"Period: {_tempPeriodMinutes / _characterAmount}");
        PlayerPrefs.SetString("Highest Score", finalScore);
    }

    private string GetFinalScore(int averageHappiness)
    {
        string scoreText = "";
        if (averageHappiness >= 95)
        {
            scoreText = "S";
        }
        else if (averageHappiness >= 90)
        {
            scoreText = "A";
        }
        else if (averageHappiness >= 80)
        {
            scoreText = "B";
        }
        else if (averageHappiness >= 70)
        {
            scoreText = "C";
        }
        else if (averageHappiness >= 60)
        {
            scoreText = "D";
        }
        else
        {
            scoreText = "F";
        }

        scoreText += AddScorePlusMinus(averageHappiness);
        return scoreText;
    }

    private string AddScorePlusMinus(int averageHappiness)
    {
        int onesDigit = averageHappiness % 10;
        if (averageHappiness < 90 && averageHappiness >= 60)
        {
            if (onesDigit <= 2)
            {
                return "-";
            }
            else if (onesDigit >= 8)
            {
                return "+";
            }
            return "";
        }

        if (averageHappiness < 95 && averageHappiness >= 90)
        { 
            if (onesDigit == 1)
            {
                return "-";
            } else if (onesDigit == 4)
            {
                return "+";
            }
            return "";
        }

        if (averageHappiness >= 95)
        {
            return new string('+', averageHappiness - 95);
        }
        return "";
    }

    public void RestartGame()
    {
        print("Restart Game");
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
