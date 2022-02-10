using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFire : MonoBehaviour
{
    [SerializeField] private int _fireType; //0 = Double, 1 = Triple, 2 = Quad
    [SerializeField] private float _speed = 3.2f;
    AudioSource multiShotSound;

    private void Start()
    {
        multiShotSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -6.0f)
        {
            Destroy(this.gameObject);
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerShots ps = other.GetComponent<PlayerShots>();
            Player pl = other.GetComponent<Player>();

            switch (_fireType)
            {
                case 0:
                    ps.multiShotON = true;
                    ps.numShotType = 2;
                    multiShotSound.Play();
                    Destroy(this.gameObject, 0.2f);
                    if (pl.mustAddGold == true)
                    {
                        pl.AddGold(3);
                        multiShotSound.Play();
                        Destroy(this.gameObject, 0.2f);
                    }
                    break;
                case 1:
                    ps.multiShotON = true;
                    ps.numShotType = 3;
                    multiShotSound.Play();
                    Destroy(this.gameObject, 0.2f);
                    if (pl.mustAddGold == true)
                    {
                        pl.AddGold(4);
                        multiShotSound.Play();
                        Destroy(this.gameObject, 0.2f);
                    }
                    break;
                case 2:
                    ps.multiShotON = true;
                    ps.numShotType = 4;
                    multiShotSound.Play();
                    Destroy(this.gameObject, 0.2f);
                    if (pl.mustAddGold == true)
                    {
                        pl.AddGold(5);
                        multiShotSound.Play();
                        Destroy(this.gameObject, 0.2f);
                    }
                    break;
            }
        }
    }
}