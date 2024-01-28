using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 0.125f;
    [SerializeField]
    private float _rotationSpeed = 5f;
    [SerializeField]
    private float _minXRotation = -90f;
    [SerializeField]
    private float _maxXRotation = 90f;
    [SerializeField]
    private float _zoomAcceleration = 0.05f;
    [SerializeField]
    private float _stopDistance = 10.0f;
    [SerializeField]
    private float _stopAngle = 10F;
    [SerializeField]
    private float _positionY = 2f;

    private Transform _target = null;
    private Coroutine _coroutine;

    public void SmoothZoom(Transform target)
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(SmoothZoomCoroutine(target));
        }
        else
        {
            _target = null;
            StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(SmoothZoomCoroutine(target));
        }
    }

    public bool RotationCameraByDelta(Vector2 delta)
    {
        if (_target != null)
        {
            var roteteAxisX = -delta.y * _rotationSpeed * Time.deltaTime;
            var roteteAxisY = delta.x * _rotationSpeed * Time.deltaTime;

            float currentAngleX = transform.localEulerAngles.x;
            float newAngleX = currentAngleX + roteteAxisX;
            newAngleX = Mathf.Clamp(newAngleX, _minXRotation, _maxXRotation);
            float deltaAngleX = newAngleX - currentAngleX;

            transform.RotateAround(_target.position, transform.right, deltaAngleX);
            transform.RotateAround(_target.position, Vector3.up, roteteAxisY);

            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator SmoothZoomCoroutine(Transform target)
    {
        var acceleration = 0f;
        var frame = 0;
        while (target != null)
        {
            Vector3 position = target.position;
            var lerpPosition = new Vector3(position.x, _positionY, position.z);

            float distance = Vector3.Distance(transform.position, lerpPosition);
            var stodDistanceOffset = 0.5;
            if (distance > _stopDistance)
            {
                Vector3 movePosition = Vector3.Lerp(transform.position, lerpPosition, _speed * Time.deltaTime);
                transform.position = movePosition;
            }
            else if (_stopDistance - distance > stodDistanceOffset)
            {
                var offset = _stopDistance - distance;
                Vector3 movePosition = Vector3.Lerp(transform.position, lerpPosition * _stopDistance, _speed * Time.deltaTime);
                transform.position = movePosition;
            }

            Quaternion targetRotate = Quaternion.LookRotation(position - transform.position);
            float angle = Quaternion.Angle(transform.rotation, targetRotate);
            if (angle > _stopAngle)
            {
                var rotateSpeed = _speed + acceleration;
                Quaternion moveRotation = Quaternion.Slerp(transform.rotation, targetRotate, rotateSpeed * Time.deltaTime);
                if (frame > 10)
                {
                    acceleration += _zoomAcceleration;
                    frame = 0;
                }
                transform.rotation = moveRotation;
            }


            if (distance <= _stopDistance && angle <= _stopAngle)
            {
                _target = target;
                Debug.Log($"The camera stopped at the object {target.name}");
                break;
            }

            frame++;
            yield return null;
        }
    }
}
