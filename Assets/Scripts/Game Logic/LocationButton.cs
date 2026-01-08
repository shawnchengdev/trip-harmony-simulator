using UnityEngine;
using UnityEngine.UI;

public class LocationButton : MonoBehaviour
{
    [SerializeField] private Image _locationIcon;
    [SerializeField] private GameObject _informationContainer;
    [SerializeField] private AudioManager _audioManager;
    private bool _informationDisplayed = false;

    public void LocationPressed()
    {
        if (!_informationDisplayed)
        {
            _locationIcon.enabled = false;
            _informationContainer.SetActive(true);
            _informationDisplayed = true;
            _audioManager.PlaySFX(_audioManager.OpenLocationPin);
        }
        else
        {
            _locationIcon.enabled = true;
            _informationContainer.SetActive(false);
            _informationDisplayed = false;
            _audioManager.PlaySFX(_audioManager.CloseLocationPin);
        }
    }
}
