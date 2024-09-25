using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    private float _health;
    public float HP => _health;
    public float MaxHP => _maxHealth;

    public UnityEvent OnDamageTaken;
    public UnityEvent OnDied;

    private void Awake()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        OnDamageTaken?.Invoke();
        if (_health <= 0.0f)
            OnDied?.Invoke();
    }
}
