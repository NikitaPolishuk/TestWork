using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum TachPadState
{
    Red,
    Green,
    Gray
}

public class TachPadController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Action<TachPadState> TachActive;

    [SerializeField]
    private GameObject _tackhPad;
    [SerializeField]
    private CameraController _cameraController;

    private Vector2 _previousTouchPosition;
    private bool _isDrag;

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == _tackhPad)
        {
            Vector2 deltaPosition = eventData.position - _previousTouchPosition;
            var active = _cameraController.RotationCameraByDelta(deltaPosition);
            _previousTouchPosition = eventData.position;

            if (active)
            {
                _isDrag = true;
                TachActive?.Invoke(TachPadState.Green);
            }
            else
            {
                _isDrag = false;
                TachActive?.Invoke(TachPadState.Gray);
            }
        }
        else if (_isDrag)
        {
            _isDrag = false;
            TachActive?.Invoke(TachPadState.Red);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == _tackhPad)
        {
            _previousTouchPosition = eventData.position;
        }
        else
        {
            TachActive?.Invoke(TachPadState.Gray);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        TachActive?.Invoke(TachPadState.Gray);
        _isDrag = false;
    }
}
