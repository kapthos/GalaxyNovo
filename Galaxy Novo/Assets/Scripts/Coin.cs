using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Player _pl;
    
    void Start()
    {
        _pl = GameObject.Find("Player").GetComponent<Player>();
        
    }


    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            int goldRandom = Random.Range(10, 25);
            _pl.AddGold(goldRandom);
            Destroy(this.gameObject);
        }
    }
}
