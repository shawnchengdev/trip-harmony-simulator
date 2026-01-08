using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Zoom : MonoBehaviour
{
    private Vector2 _startingScale = new Vector2(1, 1);
    private float _smallestScale = 0.5f;
    private float _largestScale = 1.5f;
    private Vector2 _zoomValue;
    private float _zoomScrollSensitivity = 0.05f;

    [SerializeField] private Transform _mapTransform;
    [SerializeField] private RectTransform _mapRectTransform;
    [SerializeField] private InputActionReference _zoomActionReference;
    [SerializeField] private TimeCounter _timeCounter;

    void Start()
    {
        _mapTransform.localScale = _startingScale;
    }

    private void Update()
    {
        _zoomValue = _zoomActionReference.action.ReadValue<Vector2>();
        if ((!_timeCounter.GameIsPaused) && _mapTransform.localScale.y < _largestScale && _zoomValue.y >= 1)
        {
            ZoomInOrOut(1 + _zoomScrollSensitivity);
        }
        else if (!_timeCounter.GameIsPaused && _mapTransform.localScale.y > _smallestScale && _zoomValue.y <= -1)
        {
            ZoomInOrOut(1 - _zoomScrollSensitivity);
        }
            
    }

    private void ZoomInOrOut(float scalePercentage)
    {
        _mapTransform.localScale = _mapTransform.localScale * scalePercentage;
        _mapRectTransform.anchoredPosition = new Vector2(_mapRectTransform.anchoredPosition.x * scalePercentage, _mapRectTransform.anchoredPosition.y * scalePercentage);
    }
}
