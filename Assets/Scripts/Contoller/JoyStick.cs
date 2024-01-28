using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField]
    private RectTransform _joyStickBackground;
    [SerializeField]
    private RectTransform _joystickHandle;
    [SerializeField]
    private float _maxRadius = 20;
    [SerializeField]
    private float _defaultSpeed = 20;
    [SerializeField]
    private float _accelerationValue = 3;

    private bool _isActive;
    private Vector2 _previousPosition;
    private Vector2 _delta;

    public Action<Vector2> JoyStickMove;

    private void Update()
    {
        if (_isActive)
        {
            JoyStickMove.Invoke(_delta);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isActive)
        {
            Vector3 screenPosition = eventData.position;
            screenPosition.z = _joyStickBackground.position.z;
            var offset = screenPosition - _joyStickBackground.position;

            float distanceFromCenter = offset.magnitude;
            float acceleration = Mathf.Lerp(0f, _accelerationValue, distanceFromCenter / _maxRadius );

            _joystickHandle.position = _joyStickBackground.position + Vector3.ClampMagnitude(offset, _maxRadius);

            var offsetDelta = (Vector2)_joyStickBackground.position - _previousPosition;
            _delta = Vector2.ClampMagnitude(offsetDelta, _defaultSpeed) * acceleration;
            JoyStickMove.Invoke(_delta);

            _previousPosition = _joystickHandle.position;
            Debug.Log(_delta);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == _joystickHandle.gameObject)
        {
            _isActive = true;
            _previousPosition = _joystickHandle.position;
            OnDrag(eventData);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _joystickHandle.position = _joyStickBackground.position;
        _isActive = false;
    }
}
