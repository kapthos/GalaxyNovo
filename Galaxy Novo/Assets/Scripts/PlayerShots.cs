using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShots : MonoBehaviour
{
    private float _nextFire;

    [SerializeField] private float _singleShotCD;
    [SerializeField] private float _heavyShotCD;
    [SerializeField] private float _multiShotCD;

    public bool _canSingleShot = true;
    public bool _canHeavyShot = false;

    public bool _doubleFire;
    public bool _tripleFire;
    public bool _quadFire;

    // References
    [SerializeField] private GameObject _laser;
    [SerializeField] private GameObject _heavy;

    void Update()
    {
        Shooting();
        HeavyShot();

        DoubleFireOn();
        TripleFireOn();
        QuadFireOn();

        StartCoroutine(Shooting());
        StartCoroutine(DoubleFireOn());
        StartCoroutine(TripleFireOn());
        StartCoroutine(QuadFireOn());
    }

    public void SpawnShot()
    {
        Instantiate(_laser, transform.position, transform.rotation);
    }

    IEnumerator Shooting()
    {
        if (_canSingleShot == true)
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

    IEnumerator DoubleFireOn()
    {
        if (_doubleFire == true)
        {
            _canSingleShot = false;
            if (Input.GetButtonDown("Fire1") && Time.time >= _nextFire)
            {
                for (int i = 0; i < 2; i++)
                {
                    SpawnShot();
                    yield return new WaitForSeconds(0.15f);
                    _nextFire = Time.time + _multiShotCD;
                }
                StartCoroutine(DoubleFireOff());
            }
        }
    }
    IEnumerator TripleFireOn()
    {
        if (_tripleFire == true)
        {
            _canSingleShot = false;
            if (Input.GetButtonDown("Fire1") && Time.time >= _nextFire)
            {
                for (int i = 0; i < 3; i++)
                {
                    SpawnShot();
                    yield return new WaitForSeconds(0.15f);
                    _nextFire = Time.time + _multiShotCD;
                }
                StartCoroutine(TripleFireOff());
            }
        }
    }
    IEnumerator QuadFireOn()
    {
        if (_quadFire == true)
        {
            _canSingleShot = false;
            if (Input.GetButtonDown("Fire1") && Time.time >= _nextFire)
            {
                for (int i = 0; i < 4; i++)
                {
                    SpawnShot();
                    yield return new WaitForSeconds(0.15f);
                    _nextFire = Time.time + _multiShotCD;
                }
                StartCoroutine(QuadFireOff());
            }
        }
    }

    IEnumerator DoubleFireOff()
    {
        yield return new WaitForSeconds(5);
        _doubleFire = false;
        _canSingleShot = true;
    }
    IEnumerator TripleFireOff()
    {
        yield return new WaitForSeconds(5);
        _tripleFire = false;
        _canSingleShot = true;
    }
    IEnumerator QuadFireOff()
    {
        yield return new WaitForSeconds(5);
        _quadFire = false;
        _canSingleShot = true;
    }


    public void HeavyShot()
    {
        if (_canHeavyShot == true)
        {
            _canSingleShot = false;
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
        _canHeavyShot = false;
        _canSingleShot = true;
    }
}