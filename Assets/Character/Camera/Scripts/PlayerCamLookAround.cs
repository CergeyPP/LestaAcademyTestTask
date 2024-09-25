using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamLookAround : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;

    private void Start()
    {
        Vector3 euler = transform.rotation.eulerAngles;
        _input.CameraPitch = euler.x;
        _input.CameraLookAround = euler.y;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.rotation = Quaternion.Euler(_input.CameraPitch, _input.CameraLookAround, 0);
    }
}
