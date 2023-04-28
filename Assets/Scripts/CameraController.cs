using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float xRotSpeed = 250f;
    [SerializeField] private float yRotSpeed = 120f;

    private Vector3 _offset;
    private float _x;
    private float _y;

    private Camera _camera;

    private Transform _initialTransform;
    
    private void Start()
    {
        TryGetComponent(out _camera);
        _initialTransform = transform;
        //setup offset and angles
        var position = _target.position;
        var transform1 = transform;
        _offset = transform1.position - position;
        var angles = transform1.eulerAngles;
        _x = angles.y;
        _y = angles.x;

        //set initial transform position and rotation
        Quaternion rotation = Quaternion.Euler(_y, _x, 0);
        transform.position = position + rotation * _offset;
        transform.LookAt(position);
    }

    private void LateUpdate()
    {
        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            var camFOV = _camera.fieldOfView + -Input.mouseScrollDelta.y * 2;

            var clampedVal = Mathf.Clamp(camFOV, 1, 55);
            _camera.fieldOfView = clampedVal;
        }
        
        //rotate around target with right mouse button
        var rotation = Quaternion.Euler(_y, _x, 0);
        if (Input.GetMouseButton(0))
        {
            _x += Input.GetAxis("Mouse X") * xRotSpeed * 0.02f;
            _y -= Input.GetAxis("Mouse Y") * yRotSpeed * 0.02f;
            rotation = Quaternion.Euler(_y, _x, 0);
            var position = _target.position;
            transform.position = position + rotation * _offset;
            transform.LookAt(position);
        }

        // Smoothly follow target
        var desiredPosition = _target.position + rotation * _offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, 0.1f);
    }
}
