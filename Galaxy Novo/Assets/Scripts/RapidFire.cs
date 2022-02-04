using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFire : MonoBehaviour
{
    [SerializeField] private int _fireType; //0 = Double, 1 = Triple, 2 = Quad
    [SerializeField] private float _speed = 3.5f;

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
            PlayerShots pl = other.GetComponent<PlayerShots>();
            pl.multiShotON = true;

            switch (_fireType)
            {
                case 0:
                    pl.canDoubleShot = true;
                    Destroy(this.gameObject);
                    break;
                case 1:
                    pl.canTripleShot = true;
                    Destroy(this.gameObject);
                    break;
                case 2:
                    pl.canQuadShot = true;
                    Destroy(this.gameObject);
                    break;
            }
        }
    }
}
