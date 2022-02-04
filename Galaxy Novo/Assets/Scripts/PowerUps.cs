using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField] private int _powerUpType; //0 = Triple, 1 = Speed, 2 = Shield
    [SerializeField] private float _speed;

    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y <= -6.0f)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player pl = other.GetComponent<Player>();
            PlayerShots ps = other.GetComponent<PlayerShots>();

            if (_powerUpType == 0)
            {
                ps.canHeavyShot = true;

                if (ps != null)
                {
                    Destroy(this.gameObject);
                }
            }

            else if (_powerUpType == 1)
            {
                pl.SpeedLivesAdded();

                if (pl != null)
                {
                    Destroy(this.gameObject);
                }
            }

            else if (_powerUpType == 2)
            {
                pl.ShieldOn();

                if (pl != null)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
