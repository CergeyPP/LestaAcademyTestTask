using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTrop : MonoBehaviour
{
    [SerializeField] private Transform _tropTransform;
    [SerializeField] private float _applyingVelocity;
    [SerializeField] private Quaternion _rotateByTimer;
    [SerializeField] private float _rotateTime;

    private Vector3 ApplyVelocity => _tropTransform.forward * _applyingVelocity;

    private void Start()
    {
        StartCoroutine(ChangeForceVector());
    }

    private IEnumerator ChangeForceVector()
    {
        while (true)
        {
            _tropTransform.rotation = _rotateByTimer * _tropTransform.rotation;
            yield return new WaitForSeconds(_rotateTime);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        ImpactHandler handler = other.GetComponent<ImpactHandler>();
        if (handler != null)
        {
            handler.HandleImpact(ApplyVelocity * Time.fixedDeltaTime);
        }
    }
}
