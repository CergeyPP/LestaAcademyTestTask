using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlow : MonoBehaviour
{
    [SerializeField] private Health _player;
    [SerializeField] private Timer _timer;
    [SerializeField] private float _yKill = -30;
    [SerializeField] private string _sceneName = "Main";
    [SerializeField] private Canvas _gameOverCanvas;
    [SerializeField] private Canvas _endGameCanvas;
    [SerializeField] private Canvas _HUDCanvas;

    private void Start()
    {
        Unpause();
        HideCursor();
        _timer.enabled = false;
        _gameOverCanvas.gameObject.SetActive(false);
        _endGameCanvas.gameObject.SetActive(false);

    }

    private void Update()
    {
        if (Time.timeScale > 0.001f &&_player.transform.position.y < _yKill)
        {
            GameOver();
        }
    }

    public void EndGame()
    {
        Pause();
        ShowCursor();
        _endGameCanvas.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        Pause();
        ShowCursor();
        _gameOverCanvas.gameObject.SetActive(true);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        _timer.enabled = false;
        _HUDCanvas.gameObject.SetActive(false);
    }
    public void Unpause()
    {
        Time.timeScale = 1;
        _timer.enabled = true;
        _HUDCanvas.gameObject.SetActive(true);
    }

    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void StartTimer()
    {
        _timer.enabled = true;
        _timer.StartTimer();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(_sceneName, LoadSceneMode.Single);
    }
}
