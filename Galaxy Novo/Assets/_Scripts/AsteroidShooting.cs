using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidShooting : MonoBehaviour
{
    public Transform[] rockSpawner;
    public GameObject miniRock;

    public float rockForce;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        for (int i = 0; i < rockSpawner.Length; i++)
        {
            GameObject rock = Instantiate(miniRock, rockSpawner[i].position, rockSpawner[i].rotation);
            Rigidbody2D rb = rock.GetComponent<Rigidbody2D>();
            rb.AddForce(rockSpawner[i].up * rockForce);
            Destroy(rock.gameObject, 5.0f);
        }
    }
}
