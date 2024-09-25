using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class MovementController : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private ImpactHandler _impactHandler;
    [Header("Movement Settings")]
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _lookAroundAngular = 360;
    private CharacterController _cc;
    private float JumpYVelocity => Mathf.Sqrt(2 * -Physics.gravity.y * _jumpHeight);

    private bool _shouldJumpNextFrame = false;

    private Quaternion _targetRotation;

    private void Awake()
    {
        _cc = GetComponent<CharacterController>();
        _targetRotation = transform.rotation;
    }

    private void Update()
    {
        //calc
        // end calc
        transform.rotation = GetRotation();
        _cc.Move(GetMovementVelocity() + _impactHandler.GetAndResetImpact());

        _shouldJumpNextFrame = false;
    }

    private void OnEnable()
    {
        _input.OnPressJump += OnJumpPressed;
    }
    private void OnDisable()
    {
        _input.OnPressJump -= OnJumpPressed;
    }

    private Vector3 GetMovementVelocity()
    {
        Vector2 movementInput = _input.MovementInput;
        float viewAngle = _input.CameraLookAround;
        Vector3 forwardViewDirection = Quaternion.AngleAxis(viewAngle, Vector3.up) * Vector3.forward;
        Vector3 rightViewDirection = Quaternion.AngleAxis(viewAngle, Vector3.up) * Vector3.right;
        Vector3 movementDirection = forwardViewDirection * movementInput.y 
            + rightViewDirection * movementInput.x;

        Vector3 movementVelocity = movementDirection * _maxSpeed * Time.deltaTime;
        Vector3 prevVelocity = _cc.velocity;
        prevVelocity.y = 0;
        movementVelocity = Vector3.MoveTowards(prevVelocity * Time.deltaTime, movementVelocity, 3 * Time.deltaTime);
        if (_shouldJumpNextFrame)
        {
            movementVelocity.y = JumpYVelocity * Time.deltaTime;
        }
        else
        {
            if (_cc.isGrounded)
            {
                movementVelocity.y = -0.05f;
            }
            else
            {
                float yVelocity = _cc.velocity.y;
                yVelocity += Physics.gravity.y * Time.deltaTime;
                movementVelocity.y = yVelocity * Time.deltaTime;
            } 
        }

        return movementVelocity;
    }

    private Quaternion GetRotation()
    {
        if (_input.MovementInput != Vector2.zero)
        {
            Vector3 characterLocalDirection = Vector3.forward * _input.MovementInput.y + Vector3.right * _input.MovementInput.x;
            Vector3 characterGlobalDirection = Quaternion.AngleAxis(_input.CameraLookAround, Vector3.up) * characterLocalDirection;

            _targetRotation = Quaternion.LookRotation(characterGlobalDirection, Vector3.up);
        }

        return Quaternion.RotateTowards(transform.rotation, _targetRotation, _lookAroundAngular * Time.deltaTime);
    }

    private void OnJumpPressed()
    {
        if (_cc.isGrounded)
            _shouldJumpNextFrame = true;
    }
}
