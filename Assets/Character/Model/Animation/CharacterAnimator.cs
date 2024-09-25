using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private CharacterController _cc;
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerInput _input;
    [Header("Crossfade")]
    [SerializeField] private float _pseudoAcceleration;

    private float _characterSpeed = 0;

    private void OnEnable()
    {
        _input.OnPressJump += OnJumpPressed;
    }

    private void OnDisable()
    {
        _input.OnPressJump -= OnJumpPressed;
    }

    private void Update()
    {
        float targetSpeed = new Vector3(_cc.velocity.x, 0, _cc.velocity.z).magnitude;
        _characterSpeed = Mathf.MoveTowards(_characterSpeed, targetSpeed, _pseudoAcceleration * Time.deltaTime);
        _animator.SetFloat("Speed", _characterSpeed);
        _animator.SetBool("Grounded", _cc.isGrounded);
    }

    private void OnJumpPressed()
    {
        if (_cc.isGrounded)
            _animator.SetTrigger("Jump");
    }
}
