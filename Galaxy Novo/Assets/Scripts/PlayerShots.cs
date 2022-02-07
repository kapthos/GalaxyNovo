using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShots : MonoBehaviour
{
    private float _nextFire;

    [SerializeField] private float _singleShotCD;
    [SerializeField] private float _multiShotCD;

    public bool canSingleShot = true;
    public bool canHeavyShot = false;

    public bool canDoubleShot;
    public bool canTripleShot;
    public bool canQuadShot;

    public bool multiShotON;
    private int numShotType;

    private float _timeOnPressed;
    private float _timeOnReleased;
    public int _heavyShotsCount;

    // References
    [SerializeField] private GameObject _laser;
    [SerializeField] private GameObject _heavy;
    private UIManager _ui;

    public void Start()
    {
        _ui = GameObject.Find("UIManager").GetComponent<UIManager>();
        canHeavyShot = false;
    }

    void Update()
    {
        StartCoroutine(SingleShot());
        StartCoroutine(MultiFire());
        HeavyShot();
    }
    public void SpawnShot()
    {
        Instantiate(_laser, transform.position, transform.rotation);
    }
    public void SpawnHeavy()
    {
        Instantiate(_heavy, transform.position + new Vector3(0.3f, 1, 0), Quaternion.identity);
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
    public void ShotType()
    {
        if (canDoubleShot == true)
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
            yield return new WaitForSeconds(8);
            multiShotON = false;
            canDoubleShot = false;
            canTripleShot = false;
            canQuadShot = false;
            canSingleShot = true;
        }
    }
    public void HeavyShotsAdded()
    {
        canHeavyShot = true;
        if (_heavyShotsCount < 3)
        {
            _heavyShotsCount++;
            _ui.UpdateHeavyCount(_heavyShotsCount);
        }
    }
    public void HeavyShot()
    {
        if (canHeavyShot == true)
        {
            if(_heavyShotsCount > 0)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _timeOnPressed = Time.time;
                }
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    _timeOnReleased = Time.time - _timeOnPressed;
                    if (_timeOnReleased >= 0.5f)
                    {
                        SpawnHeavy();
                        _heavyShotsCount--;
                        _ui.UpdateHeavyCount(_heavyShotsCount);
                        _ui.heavyShotBar.value = 0;
                    }
                }
            }
            else
            {
                canHeavyShot = false;
            }
        }
    }
}