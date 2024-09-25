using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ImpactHandler : MonoBehaviour
{
    private CharacterController _cc;
    private void Awake()
    {
        _cc = GetComponent<CharacterController>();
    }
    public void ResetImpact()
    {
        _impact = Vector3.zero;
    }
    public void HandleImpact(Vector3 velocity)
    {
        _impact += velocity;
    }

    public Vector3 GetAndResetImpact()
    {
        Vector3 impact = _impact;
        ResetImpact();
        return impact;
    }

    private Vector3 _impact;
    public Vector3 Impact;
}
