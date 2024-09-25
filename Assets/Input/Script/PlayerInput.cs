using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    [Range(0.1F, 10)] private float _verticalSensivity;
    [SerializeField]
    [Range(0.1F, 10)] private float _horizontalSensivity;
    [SerializeField] 
    [Range(0, 89)] private float _maxCameraPitch = 45;

    private float _cameraLookAround;
    public float CameraLookAround { 
        get
        {
            return _cameraLookAround;
        }
        set
        {
            _cameraLookAround = Mathf.Repeat(value, 360);
        }
    }

    private float _cameraPitch;
    public float CameraPitch
    {
        get 
        { 
            return _cameraPitch; 
        }
        set
        {
            _cameraPitch = Mathf.Clamp(value, -_maxCameraPitch, _maxCameraPitch);
        }
    }

    public Vector2 MovementInput { get; private set; }
    public Action OnPressJump;

    public void Look(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        CameraLookAround += value.x * _horizontalSensivity * Time.deltaTime;
        CameraPitch -= value.y * _verticalSensivity * Time.deltaTime;
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        MovementInput = value;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
            OnPressJump?.Invoke();
    }
}
