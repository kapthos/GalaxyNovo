using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerBehavior : MonoBehaviour
{
    [SerializeField] private float _enemySpawnRate;
    private float _timer;

    [SerializeField] private GameObject _enemyShip;

    void Start()
    {
        GameObject newShip = Instantiate(_enemyShip);
        float xRand = Random.Range(-8.4f, 8.4f);
        newShip.transform.position = new Vector3(xRand, 7, 0);
    }


    void Update()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        if (_timer >= _enemySpawnRate)
        {
            GameObject newShip = Instantiate(_enemyShip);
            float xRand = Random.Range(-8.4f, 8.4f);
            newShip.transform.position = new Vector3(xRand, 7, 0);
            _timer = 0.0f;
        }
        _timer += Time.deltaTime;
    }
}
