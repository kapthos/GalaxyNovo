using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField] private int _powerUpType; //0 = Triple, 1 = Speed, 2 = Shield
    [SerializeField] private float _speed;

    AudioSource powerUpSound;
    CircleCollider2D bc;
    Renderer rd;

    public void Start()
    {
        powerUpSound = GetComponent<AudioSource>();
        bc = GetComponent<CircleCollider2D>();
        rd = this.GetComponent<Renderer>();
    }

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
            powerUpSound.Play();

            if (_powerUpType == 0)
            {
                if (pl.mustAddGold0 == true)
                {
                    pl.AddGold(3);
                    OnCollected();
                }
                else
                {
                    ps.HeavyShotsAdded();
                    OnCollected();
                }
            }

            else if (_powerUpType == 1)
            {
                if (pl.mustAddGold1 == true)
                {
                    pl.AddGold(3);
                    OnCollected();
                }
                else
                {
                    pl.SpeedLivesAdded();
                    OnCollected();
                }
            }

            else if (_powerUpType == 2)
            {
                if (pl.mustAddGold2 == true)
                {
                    pl.AddGold(2);
                    OnCollected();
                }
                else
                {
                    pl.ShieldOn();
                    OnCollected();
                }
            }
        }
    }
    private void OnCollected()
    {
        bc.enabled = false;
        rd.enabled = false;
        Destroy(this.gameObject, 0.8f);
    }
}
