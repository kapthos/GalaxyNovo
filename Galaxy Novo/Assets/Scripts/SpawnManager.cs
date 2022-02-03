using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private bool _canSpawnPowerUp = false;
    [SerializeField] private float _enemySpawnRate;
    [SerializeField] private float _powerUpSpawnRate;
    [SerializeField] private float _rapidSpawnRate;

    [SerializeField] private float _nextPowerUp;
    [SerializeField] private float _nextEnemy;
    [SerializeField] private float _nextRapidShot;

    [SerializeField] private GameObject _enemyShip;
    [SerializeField] private GameObject[] _powerUps;
    [SerializeField] private GameObject[] _rapidShots;

    void Start()
    {

    }

    void Update()
    {
        SpawnEnemy();
        PowerUpSpawn();
        RapidShotSpawn();
    }

    public void SpawnEnemy()
    {
        if (Time.time >= _nextEnemy)
        {
            GameObject newShip = Instantiate(_enemyShip);
            float xRand = Random.Range(-8.4f, 8.4f);
            newShip.transform.position = new Vector3(xRand, 7, 0);
            _nextEnemy = Time.time + _enemySpawnRate;
        }
    }

    public void PowerUpSpawn()
    {
        if (Time.time >= _nextPowerUp)
        {
            int RandomPU = Random.Range(0, 3);
            float xRand = Random.Range(-8.4f, 8.4f);
            Instantiate(_powerUps[RandomPU], new Vector3(xRand, 7, 0), Quaternion.identity);
            _nextPowerUp = Time.time + _powerUpSpawnRate;
        }
    }

    public void RapidShotSpawn()
    {
        if (Time.time >= _nextRapidShot)
        {
            int RandomPU = Random.Range(0, 3);
            float xRand = Random.Range(-8.4f, 8.4f);
            Instantiate(_rapidShots[RandomPU], new Vector3(xRand, 7, 0), Quaternion.identity);
            _nextRapidShot = Time.time + _rapidSpawnRate;
        }
    }
}
