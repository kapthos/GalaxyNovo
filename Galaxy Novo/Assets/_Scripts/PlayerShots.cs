using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShots : MonoBehaviour
{
    private float _nextFire;

    [SerializeField] private float _singleShotCD;
    [SerializeField] private float _multiShotCD;

    public bool multiShotON;
    public int numShotType;
    private float multiShotTimer;
    [SerializeField] private float multiShotDuration = 4f;

    public bool canHeavyShot = false;
    private float _timeOnPressed;
    private float _timeOnReleased;
    public int _heavyShotsCount;

    // References
    [SerializeField] private GameObject _laser;
    [SerializeField] private GameObject _heavy;
    private UIManager ui;
    private Player pl;

    public void Start()
    {
        ui = GameObject.Find("UIManager").GetComponent<UIManager>();
        pl = GameObject.Find("Player").GetComponent<Player>();
        canHeavyShot = false;
        numShotType = 1;
    }

    void Update()
    {
        HeavyShot();
        SingleShot();
        StartCoroutine(MultiShot());
    }
    public void SpawnShot()
    {
        Instantiate(_laser, transform.position, transform.rotation);
    }
    public void SpawnHeavy()
    {
        Instantiate(_heavy, transform.position + new Vector3(0.3f, 1, 0), Quaternion.identity);
    }
    public void HeavyShotsAdded()
    {
        canHeavyShot = true;
        if (_heavyShotsCount < 3)
        {
            _heavyShotsCount++;
            ui.UpdateHeavyCount(_heavyShotsCount);
        }
        else
        {
            pl.mustAddGold = true;
        }
    }
    public void HeavyShot()
    {
        if (canHeavyShot == true)
        {
            if (_heavyShotsCount > 0)
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
                        ui.UpdateHeavyCount(_heavyShotsCount);
                        ui.heavyShotBar.value = 0;
                    }
                }
            }
            else
            {
                canHeavyShot = false;
            }
        }
    }
    public void SingleShot()
    {
        if (numShotType == 1)
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= _nextFire)
            {
                SpawnShot();
                _nextFire = Time.time + _singleShotCD;
            }
        }
    }
    public IEnumerator MultiShot()
    {
        if (multiShotON == true)
        {
            multiShotTimer += Time.deltaTime;
            pl.mustAddGold = true;

            if (Input.GetButtonDown("Fire1") && Time.time >= _nextFire)
            {
                for (int i = 0; i < numShotType; i++)
                {
                    SpawnShot();
                    yield return new WaitForSeconds(0.15f);
                    _nextFire = Time.time + _multiShotCD;
                }
            }
            if (multiShotTimer > multiShotDuration)
            {
                multiShotON = false;
                numShotType = 1;
                multiShotTimer = 0;
                pl.mustAddGold = false;
            }
        }
    }
}