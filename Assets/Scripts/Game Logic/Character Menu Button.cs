using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterMenuButton : MonoBehaviour
{
    public bool CharacterMenuOpened { get; private set; } = false;

    [Header("Object References")]
    [SerializeField] private GameObject _characterMenu;
    [SerializeField] private GameObject _pauseMenu;

    [Header("Script References")]
    [SerializeField] private PauseButton _pauseLogic;


    void Start()
    {
        _characterMenu.SetActive(false);
    }

    public void CharacterMenuButtonPressed()
    {
        if (!CharacterMenuOpened)
        {
            OpenCharacterMenu();
        }
    }

    private void OpenCharacterMenu()
    {
        Debug.Log("Character Menu Opened");
        _characterMenu.SetActive(true);
        CharacterMenuOpened = true;
        _pauseLogic.PauseGamePressed();
        _pauseMenu.SetActive(false);
    }

    public void CloseCharacterMenu()
    {
        Debug.Log("Character Menu Closed");
        _characterMenu.SetActive(false);
        CharacterMenuOpened = false;
        _pauseLogic.ResumeGamePressed();
    }
}
