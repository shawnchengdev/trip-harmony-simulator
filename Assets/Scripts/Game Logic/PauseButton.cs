using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    private bool _isGamePaused = false;

    [Header("Object References")]
    [SerializeField] private Image _pauseImage;
    [SerializeField] private Image _exitImage;
    [SerializeField] private GameObject _pauseMenu;

    [Header("Script References")]
    [SerializeField] private CharacterMenuButton _characterMenuButton;
    [SerializeField] private TimeCounter _timeCounter;
    [SerializeField] private AudioManager _audioManager;


    private void Start()
    {
        _pauseMenu.SetActive(false);
    }

    public void PauseButtonPressed()
    {
        if (!_characterMenuButton.CharacterMenuOpened)
        {
            if (!_isGamePaused)
            {
                PauseGamePressed();
                _audioManager.PlaySFX(_audioManager.PauseGame);
            }
            else
            {
                ResumeGamePressed();
                _audioManager.PlaySFX(_audioManager.ResumeGame);
            }
        }
        else
        {
            _characterMenuButton.CloseCharacterMenu();
            _audioManager.PlaySFX(_audioManager.CloseCharacterMenu);
        }
    }

    public void PauseGamePressed()
    {
        Debug.Log("Game is Paused");
        _timeCounter.PauseGame();
        _isGamePaused = true;
        _pauseImage.enabled = false;
        _exitImage.enabled = true;
        _pauseMenu.SetActive(true); 
    }

    public void ResumeGamePressed()
    {
        Debug.Log("Game is Unpaused");
        _timeCounter.ResumeGame();
        _isGamePaused = false;
        _pauseImage.enabled = true;
        _exitImage.enabled = false;
        _pauseMenu.SetActive(false);
    }
}
