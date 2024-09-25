using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartLine : MonoBehaviour
{
    [SerializeField] private UnityEvent _onReach;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MovementController>() != null)
        {
            _onReach?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
