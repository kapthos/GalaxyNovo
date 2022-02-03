using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _lives;

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
            other.transform.GetComponent<Player>().Damage();
            Destroy(this.gameObject);
        }
        else if (other.tag == "Laser")
        {
            Damage();
            Destroy(other.gameObject);
        }
        else if (other.tag == "HeavyShot")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }

    public void Damage()
    {
        _lives--;

        if (_lives < 1)
        {
            Destroy(this.gameObject);
        }
    }
}
