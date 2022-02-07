using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float _speedRotation = 15.0f;
    [SerializeField] private float _lives;
    [SerializeField] private GameObject _explosion;

    //Animator _animExplosion;
    GameManager _gm;
    SpawnManager _sm;

    public void Start()
    {
        //_animExplosion = gameObject.GetComponent<Animator>();
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        _sm = GameObject.Find("SpawManager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * _speedRotation * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(other.gameObject);
            Instantiate(_explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject, 0.3f);
            _gm.GameOver();
        }
        if (other.tag == "Laser")
        {
            _lives--;
            Destroy(other.gameObject);

            if (_lives < 1)
            {            
                Instantiate(_explosion, transform.position, Quaternion.identity);
                Destroy(this.gameObject, 0.3f);
            }
        }
    }
}
