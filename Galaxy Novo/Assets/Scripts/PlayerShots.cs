using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShots : MonoBehaviour
{
    private float _nextFire;

    [SerializeField] private float _singleShotCD;
    [SerializeField] private float _heavyShotCD;
    [SerializeField] private float _multiShotCD;

    public bool canSingleShot = true;
    public bool canHeavyShot = false;

    public bool canDoubleShot;
    public bool canTripleShot;
    public bool canQuadShot;

    public bool multiShotON;

    private int numShotType;

    // References
    [SerializeField] private GameObject _laser;
    [SerializeField] private GameObject _heavy;

    void Update()
    {
        HeavyShot();
        StartCoroutine(SingleShot());
        StartCoroutine(MultiFire());
    }
    public void SpawnShot()
    {
        Instantiate(_laser, transform.position, transform.rotation);
    }
    IEnumerator SingleShot()
    {
        if (canSingleShot == true)
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= _nextFire)
            {
                for (int i = 0; i < 1; i++)
                {
                    SpawnShot();
                    yield return new WaitForSeconds(0.15f);
                    _nextFire = Time.time + _singleShotCD;
                }
            }
        }
    }
    public IEnumerator MultiFire()
    {
        if (multiShotON == true)
        {
            ShotType();
            if (Input.GetButtonDown("Fire1") && Time.time >= _nextFire)
            {
                canSingleShot = false;
                for (int i = 0; i < numShotType; i++)
                {
                    SpawnShot();
                    yield return new WaitForSeconds(0.15f);
                    _nextFire = Time.time + _multiShotCD;
                }
            }
            yield return new WaitForSeconds(5);
            multiShotON = false;
            canDoubleShot = false;
            canTripleShot = false;
            canQuadShot = false;
            canSingleShot = true;
        }
    }
    public void HeavyShot()
    {
        if (canHeavyShot == true)
        {
            canSingleShot = false;
            if (Input.GetButtonDown("Fire1") && Time.time >= _nextFire)
            {
                Instantiate(_heavy, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                _nextFire = Time.time + _heavyShotCD;
            }
            StartCoroutine(HeavyShotDownRoutine());
        }
    }
    IEnumerator HeavyShotDownRoutine()
    {
        yield return new WaitForSeconds(5);
        canHeavyShot = false;
        canSingleShot = true;
    }
    public void ShotType()
    {
        if(canDoubleShot == true)
        {
            numShotType = 2;
        }
        else if (canTripleShot == true)
        {
            numShotType = 3;
        }
        else if (canQuadShot == true)
        {
            numShotType = 4;
        }
    }
}