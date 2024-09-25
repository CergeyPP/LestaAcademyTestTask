using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Health _player;
    [SerializeField] private Slider _slider;

    private void Awake()
    {
        _player.OnDamageTaken.AddListener(OnDamaged);
    }

    private void Start()
    {
        _slider.maxValue = _player.MaxHP;
        _slider.minValue = 0;
        _slider.value = _player.HP;
    }

    private void OnDestroy()
    {
        _player.OnDamageTaken.RemoveListener(OnDamaged);
    }

    private void OnDamaged()
    {
        _slider.value = _player.HP;
    }
}
