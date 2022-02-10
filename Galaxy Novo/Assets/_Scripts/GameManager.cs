using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool _isGameOver = false;
    public bool timeOver;
    public bool levelComplete;

    private UIManager _ui;
    private SpawnManager _spm;

    public void Start()
    {
        _ui = GameObject.Find("UIManager").GetComponent<UIManager>();
        _spm = GameObject.Find("SpawManager").GetComponent<SpawnManager>();
        timeOver = false;
        levelComplete = false;
}
    void Update()
    {
        RestartLevel();
        TimeOver();
        CheckLevelComplete();
    }
    public void RestartLevel()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(0);
        }
    }
    public void GameOver()
    {
        _isGameOver = true;
    }
    public void TimeOver()
    {
        if (_ui.timeBar.value >= _ui.timeBar.maxValue)
        {
            timeOver = true;
        }
    }
    public void CheckLevelComplete()
    {
        if (GameObject.FindGameObjectWithTag("Nave") == null)
        {
            levelComplete = true;
            _ui.SpawnLoja();
        }
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
