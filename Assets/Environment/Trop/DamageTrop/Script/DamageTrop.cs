using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrop : MonoBehaviour
{
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private float _damage;
    [SerializeField] private float _prepareTime;
    [SerializeField] private float _damageTime;
    [SerializeField] private float _reloadTime;
    [SerializeField] private Color _prepareColor;
    [SerializeField] private Color _damageColor;
    [SerializeField] private Color _reloadColor;

    private Color _defaultColor;

    private List<Health> healths;

    private void Awake()
    {
        _defaultColor = _mesh.material.color;
        healths = new List<Health>();
    }

    private void OnTriggerEnter(Collider hit)
    {
        Health hpComponent = hit.gameObject.GetComponent<Health>();
        if (hpComponent != null)
        {
            healths.Add(hpComponent);
            if (_isReady)
                StartCoroutine(ActivateTrap());
            if (_isDamaging)
                hpComponent.TakeDamage(_damage);
        }
    }

    private void OnTriggerExit(Collider hit)
    {
        Health hpComponent = hit.gameObject.GetComponent<Health>();
        if (hpComponent != null)
        {
            healths.Remove(hpComponent);
        }
    }

    private bool _isReady = true;
    private bool _isDamaging = false;
    private IEnumerator ActivateTrap()
    {
        _isReady = false;
        _mesh.material.color = _prepareColor;
        yield return new WaitForSeconds(_prepareTime);
        _isDamaging = true;
        _mesh.material.color = _damageColor;
        foreach (var item in healths)
        {
            item.TakeDamage(_damage);
        }
        yield return new WaitForSeconds(_damageTime);
        _isDamaging = false;
        _mesh.material.color = _reloadColor;
        yield return new WaitForSeconds(_reloadTime);
        _mesh.material.color = _defaultColor;
        _isReady = true;
    }
}
