using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraseTrop : MonoBehaviour
{
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private Collider _collider;
    [SerializeField] private AnimationCurve _colorChangeCurve;
    [SerializeField] private Color _endColor;
    [SerializeField] private float _eraseTimer;
    [SerializeField] private float _recoverTime;

    private bool _isReady = true;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isReady && other.GetComponent<MovementController>() != null)
        {
            StartCoroutine(ActivateTrop());
        }
    }

    private IEnumerator ActivateTrop()
    {
        _isReady = false;
        float time = 0;
        Color startColor = _mesh.material.color;
        while (time < _eraseTimer)
        {
            float lerpT = time / _eraseTimer;
            _mesh.material.color = Color.Lerp(startColor, _endColor, _colorChangeCurve.Evaluate(lerpT));
            time += Time.deltaTime;
            yield return null;
        }
        _mesh.material.color = _endColor;
        _collider.enabled = false;
        yield return new WaitForSeconds(_recoverTime);
        _mesh.material.color = startColor;
        _collider.enabled = true;
        _isReady = true;
    }

}
