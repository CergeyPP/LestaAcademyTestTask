using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float _time = 0;

    public void StartTimer()
    {
        _time = 0;
    }

    public void Update()
    {
        _time += Time.deltaTime;
    }

    public float Value => _time;
}
