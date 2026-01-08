using UnityEngine;

public class SettingButton : MonoBehaviour
{
    [SerializeField] private GameObject _settingMenu;
    private bool _settingsMenuOpened = false;

    public void SettingsButtonPressed()
    {
        if (_settingsMenuOpened)
        {
            _settingsMenuOpened = false;
            _settingMenu.SetActive(false);
        }
        else
        {
            _settingsMenuOpened = true;
            _settingMenu.SetActive(true);
        }
    }
}
