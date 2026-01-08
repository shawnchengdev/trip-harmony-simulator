using UnityEngine;
using UnityEngine.UI;

public class PaperStyle : MonoBehaviour
{
    private float _lastUpdateTime = 0;
    private float _currentTime = 0;
    [SerializeField] private float _paperCycleTime = 0.4f;

    private int _currentCycleIndex = 0;
    [SerializeField] private Sprite[] _paperTextureArray;
    [SerializeField] private Image _paperTextureImage;

    private void Update()
    {
        _currentTime = Time.realtimeSinceStartup;
        if (_currentTime - _lastUpdateTime > _paperCycleTime)
        {
            _lastUpdateTime = _currentTime;
            UpdatePaperTexture();
        }
    }

    private void UpdatePaperTexture()
    {
        if (_currentCycleIndex >= _paperTextureArray.Length - 1)
        {
            _currentCycleIndex = 0;
        }
        else
        {
            _currentCycleIndex++;
        }
        _paperTextureImage.sprite = _paperTextureArray[_currentCycleIndex];
    }
}
