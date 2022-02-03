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
            if (_fireType == 0)
            {
                other.GetComponent<PlayerShots>()._doubleFire = true;
                Destroy(this.gameObject);
            }
            else if (_fireType == 1)
            {
                other.GetComponent<PlayerShots>()._tripleFire = true;
                Destroy(this.gameObject);
            }
            else if (_fireType == 2)
            {
                other.GetComponent<PlayerShots>()._quadFire = true;
                Destroy(this.gameObject);
            }
        }
    }
}
