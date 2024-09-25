using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Timer _timer;

    private void Update()
    {
        if (_timer.enabled)
        {
            _text.text = ConvertToText();
        }
    }

    private void OnEnable()
    {
        _text.text = ConvertToText();
    }

    private string ConvertToText()
    {
        int seconds = (int)Mathf.Floor(_timer.Value);
        float milliseconds = _timer.Value - seconds;
        float minutes = seconds / 60;
        seconds = seconds % 60;

        string text = minutes.ToString() + ":" + seconds.ToString() + milliseconds.ToString(".00");
        return text;
    }
}
