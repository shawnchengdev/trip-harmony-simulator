using UnityEngine;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private GameObject _startMenu;
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private TimeCounter _timeCounter;

    [Header("Music References")]
    [SerializeField] private AudioManager _audioManager;

    private void Start()
    {
        _timeCounter.PauseGame();
        _audioManager.PlayMusic(_audioManager.StartMenuMusic);
    }

    public void PlayButtonPressed()
    {
        _startMenu.SetActive(false);
        _timeCounter.StartGame();
    }
}
