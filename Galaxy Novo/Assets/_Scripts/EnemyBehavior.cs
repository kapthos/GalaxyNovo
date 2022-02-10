using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _lives;
    public bool stopReuse;

    private bool reverseMovement;
    private bool reversed;
    private bool facingUp;


    Player pl;
    Animator _animExplosion;
    private AudioSource explosionSound;
    [SerializeField] GameObject _coin;

    void Start()
    {
        pl = GameObject.Find("Player").GetComponent<Player>();
        _animExplosion = gameObject.GetComponent<Animator>();
        explosionSound = gameObject.GetComponent<AudioSource>();
        stopReuse = false;
        
    }

    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        if (reverseMovement == true)
        {
            CheckWhereToFace();
            reversed = true;

            transform.Translate(Vector3.up * _speed * Time.deltaTime);

        }
        else if (reverseMovement == false)
        {
            CheckWhereToFace();
            reversed = false;

            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player pl = other.GetComponent<Player>();

            pl.Damage();
            _lives = _lives - 3;
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
        else if (other.tag == "GoDown")
        {
            reverseMovement = false;
        }
        else if (other.tag == "GoUp")
        {
            reverseMovement = true;
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
            _animExplosion.SetTrigger("OnEnemyDeath");
            explosionSound.Play();
            DropGoldChance();
            _speed = 0;
            Destroy(this.gameObject, 1.1f);
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

    public void DestroyRemaining()
    {
        Destroy(GameObject.FindGameObjectWithTag("Nave"));
    }
    public void CheckWhereToFace()
    {
        if(reversed == false)
        {
            facingUp = false;
        }
        else if(reversed == true)
        {
            facingUp = true;
        }
        if ((facingUp && transform.localScale.y > 0) || (!facingUp && transform.localScale.y < 0))
        {
            transform.localScale *= -1;
        }
    }
}
