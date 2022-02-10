using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float _powerUpSpawnRate;
    [SerializeField] private float _rapidSpawnRate;

    [SerializeField] private float _nextPowerUp;
    [SerializeField] private float _nextRapidShot;

    [SerializeField] private GameObject _enemyShip;
    [SerializeField] private GameObject[] _powerUps;
    [SerializeField] private GameObject[] _rapidShots;

    private GameObject[] allships;

    public bool _stopAllSpawns = false;

    private GameManager _gm;
    private EnemyBehavior _enemy;

    public void Start()
    {
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        _enemy = _enemyShip.GetComponent<EnemyBehavior>();
        StartCoroutine(SpawnEnemy());
    }

    void Update()
    {
        PowerUpSpawn();
        RapidShotSpawn();
        StopAllSpawns();
        CheckTimeOver();

    }
    public IEnumerator SpawnEnemy()
    {
        while (_stopAllSpawns == false)
        {
            float xRand = Random.Range(-10f, 10f);
            GameObject newShip = Instantiate(_enemyShip, new Vector3(xRand, 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(3);
        }   
    }
    public void PowerUpSpawn()
    {
        if (!_stopAllSpawns)
        {
            if (Time.time >= _nextPowerUp)
            {
                int RandomPU = Random.Range(0, 3);
                float xRand = Random.Range(-10f, 10f);
                Instantiate(_powerUps[RandomPU], new Vector3(xRand, 7, 0), Quaternion.identity);
                _nextPowerUp = Time.time + _powerUpSpawnRate;
            }
        }

    }
    public void RapidShotSpawn()
    {
        if (!_stopAllSpawns)
        {
            if (Time.time >= _nextRapidShot)
            {
                int RandomPU = Random.Range(0, 3);
                float xRand = Random.Range(-10f, 10f);
                Instantiate(_rapidShots[RandomPU], new Vector3(xRand, 7, 0), Quaternion.identity);
                _nextRapidShot = Time.time + _rapidSpawnRate;
            }
        }

    }
    public void StopAllSpawns()
    {
        if (_gm._isGameOver == true)
        {
            _stopAllSpawns = true;
            Destroy(this.gameObject);
        }
    }
    public void CheckTimeOver()
    {
        if(_gm.timeOver == true)
        {
            _stopAllSpawns = true;  
        }
    }
}
