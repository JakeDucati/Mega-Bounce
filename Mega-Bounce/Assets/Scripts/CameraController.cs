using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Range(0f, 15.0f)] public float PitchSensitivity;
    [SerializeField, Range(0f, 15.0f)] public float YawSensitivity;
    [SerializeField, Range(0.1f, 5f)] public float ZoomSensitivity;
    [SerializeField] public float PitchLowerLimit;
    [SerializeField] public float PitchUpperLimit;
    [SerializeField] public float ZoomLowerLimit;
    [SerializeField] public float ZoomUpperLimit;
    [SerializeField, Range(0.005f, 2.0f)] public float OrbitalAcceleration;
    [SerializeField, Range(0.005f, 2.0f)] public float ZoomAcceleration;
    [SerializeField] public Transform FollowTransform;
    private float _targetZoom = 10;
    private float _targetPitch = 20.0f;
    private float _targetYaw = 0.0f;
    private float _actualZoom = 10.0f;
    private float _actualPitch = 0.0f;
    private float _actualYaw = 0.0f;
    private bool _useOrbit = false;
    private Vector3 offset = new Vector3(0, -0.5f, 0);
    
    void Awake()
    {
    }
    private void Update()
    {
        float p = 0;
        float y = 0;
        float xAxis = 0;
        float yAxis = 0;
        bool mouseDown = false;
        if (!mouseDown && Input.GetKey(KeyCode.Mouse0))
        {
            mouseDown = true;
            xAxis = Input.GetAxis("Mouse X");
            yAxis = Input.GetAxis("Mouse Y");
        }
        if (mouseDown)
        {
            p = yAxis * -PitchSensitivity;
            y = xAxis * YawSensitivity;
        }
        float z = ZoomSensitivity * -Input.mouseScrollDelta.y;
        _targetZoom = Mathf.Clamp(_targetZoom + z, ZoomLowerLimit, ZoomUpperLimit);
        float zoomLerpFactor = Mathf.Clamp(ZoomAcceleration * Time.deltaTime / 0.018f,0.01f,1.0f);
        _actualZoom = Mathf.Lerp(_actualZoom, _targetZoom, zoomLerpFactor);
        _targetPitch = Mathf.Clamp(_targetPitch + p,PitchLowerLimit,PitchUpperLimit);
        _targetYaw += y;
        float orbitLerpFactor = Mathf.Clamp(OrbitalAcceleration * Time.deltaTime / 0.018f,0.01f,1.0f);
        _actualPitch = Mathf.Lerp(_actualPitch, _targetPitch, orbitLerpFactor);
        _actualYaw = Mathf.Lerp(_actualYaw,_targetYaw,orbitLerpFactor);
    }
    void LateUpdate()
    {
        var t = transform;
        t.localPosition = FollowTransform == null ? Vector3.zero : FollowTransform.position + offset;
        t.localRotation = Quaternion.identity;
        var up = t.up;
        t.localRotation = Quaternion.Euler(_actualPitch,0,0);
        t.RotateAround(FollowTransform == null ? Vector3.zero : FollowTransform.position + offset,up,_actualYaw);
        t.localPosition = (up * 0.5f + t.forward * -_actualZoom) + t.localPosition;
    }
}