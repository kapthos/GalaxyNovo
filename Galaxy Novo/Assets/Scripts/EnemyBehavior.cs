using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _lives;
    public int points;

    Player pl;
    Animator _animExplosion;
    [SerializeField] GameObject _coin;

    void Start()
    {
        pl = GameObject.Find("Player").GetComponent<Player>();
        _animExplosion = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        Movement();
    }
    public void Movement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -6)
        {
            float xRand = Random.Range(-8.4f, 8.4f);
            transform.position = new Vector3(xRand, 7, 0);
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player pl = other.GetComponent<Player>();

            pl.Damage();
            _animExplosion.SetTrigger("OnEnemyDeath");
            Death();
        }
        else if (other.tag == "Laser")
        {
            Damage();
            Destroy(other.gameObject);
            Death();
        }
        else if (other.tag == "HeavyShot")
        {
            _lives = _lives - 3;
            Destroy(other.gameObject);
            Death();
        }
    }
    public void Damage()
    {
        _lives--;
    }
    public void Death()
    {
        if (_lives < 1)
        {
            pl.AddPoints(10);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            DropGoldChance();
            _speed = 0;
            _animExplosion.SetTrigger("OnEnemyDeath");         
            Destroy(this.gameObject, 1.7f);
        }
    }
    public void DropGoldChance()
    {
        int chance = Random.Range(0, 10);
        if(chance > 7)
        {
            Instantiate(_coin, transform.position, Quaternion.identity);
        }
    }
}
