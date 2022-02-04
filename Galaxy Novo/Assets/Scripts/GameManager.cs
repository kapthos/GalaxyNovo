using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    private Player _pl;
    public bool _isGameOver = false;


    public void Start()
    {
        _pl = GameObject.Find("Player").GetComponent<Player>();
    }
    void Update()
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
}
