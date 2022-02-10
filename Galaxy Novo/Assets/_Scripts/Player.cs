using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject shields;
    [SerializeField] private GameObject largeThrusters;
    [SerializeField] private GameObject[] fireDamage;

    private float _horizontalInput;
    private float _verticalInput;

    public bool canMove = true;

    public int _lives;

    [SerializeField] private float _currentTurbo;
    [SerializeField] private float _currentSpeed;
    public bool canTurbo;
    public int _speedLives;
    private float _shiftHolding;
    private float _defaultSpeed = 5;
    private float _turboSpeed = 7.5f;

    public bool _shieldOn;
    [SerializeField] private float _shieldDuration;

    [SerializeField] private int _gold = 0;
    public int _score = 0;

    public bool mustAddGold;
    public bool mustAddGold0;
    public bool mustAddGold1;
    public bool mustAddGold2;

    //References
    private UIManager _uiManager;
    private GameManager _gm;

    void Start()
    {
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        _currentTurbo = 0;
        mustAddGold = false;
        mustAddGold0 = false;
        mustAddGold1 = false;
        mustAddGold2 = false;
    }
    void Update()
    {
        Movement();
        PlayerDeath();
        TurnTurboOn();
    }
    public void Movement()
    {
        if (canMove == true)
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");

            transform.Translate(Vector3.right * _horizontalInput * _currentSpeed * Time.deltaTime);
            transform.Translate(Vector3.up * _verticalInput * _currentSpeed * Time.deltaTime);
        }

        if (transform.position.x <= -10.2f)
        {
            transform.position = new Vector3(-10.2f, transform.position.y, 0);
        }
        else if (transform.position.x >= 10.2f)
        {
            transform.position = new Vector3(10.2f, transform.position.y, 0);
        }
        if (transform.position.y >= 3.5f)
        {
            transform.position = new Vector3(transform.position.x, 3.5f, 0);
        }
        else if (transform.position.y <= -5.4f)
        {
            transform.position = new Vector3(transform.position.x, -5.4f, 0);
        }
    }
    public void PlayerHurt()
    {
        _lives--;
        _uiManager.UpdateLives(_lives);

        int randFire = Random.Range(0, 6);
        float xRand = Random.Range(-0.2f, 0.2f);
        float yRand = Random.Range(-1.15f, -0.3f);
        fireDamage[randFire].SetActive(true);
    }
    public void Damage()
    {
        if (_shieldOn == true)
        {
            _shieldOn = false;
            shields.SetActive(false);
            return;
        }

        PlayerHurt();
    }
    public void ShieldOn()
    {
        _shieldOn = true;
        mustAddGold2 = true;
        shields.SetActive(true);
        StartCoroutine(ShieldPowerDownRoutine());
    }
    IEnumerator ShieldPowerDownRoutine()
    {
        yield return new WaitForSeconds(_shieldDuration);
        shields.SetActive(false);
        _shieldOn = false;
        mustAddGold2 = false;
    }
    public void PlayerDeath()
    {
        if (_lives < 1)
        {
            _gm.GameOver();
            Destroy(this.gameObject);
        }
    }
    public void AddPoints(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
    public void AddGold(int receivedGold)
    {
        _gold += receivedGold;
        _uiManager.UpdateGold(_gold);
    }
    public void SpeedLivesAdded()
    {
        canTurbo = true;
        _uiManager._turboBar.value = 30;

        if (_speedLives < 3)
        {
            if (_currentTurbo == 0)
            {
                _currentTurbo = _uiManager._turboBar.maxValue;
                _speedLives++;
            }
            else
            {
                _speedLives++;

            }
            if (_speedLives >= 3)
            {
                mustAddGold1 = true;
            }
        }
        _uiManager.UpdateTurbo(_speedLives);
    }
    public void TurnTurboOn()
    {
        if (canTurbo == true)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _shiftHolding = Time.deltaTime * 10;
                _currentTurbo -= _shiftHolding;
                largeThrusters.SetActive(true);

                if (_currentTurbo > 0)
                {
                    _currentSpeed = _turboSpeed;          
                }
                else
                {
                    _currentSpeed = _defaultSpeed;
                    largeThrusters.SetActive(false);
                }
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                if (_currentTurbo > 0)
                {
                    _currentSpeed = _defaultSpeed;
                    largeThrusters.SetActive(false);
                }
                else
                {
                    _speedLives--;
                    mustAddGold1 = false;
                    _uiManager.UpdateTurbo(_speedLives);
                    _currentSpeed = _defaultSpeed;
                    largeThrusters.SetActive(false);
                    _currentTurbo = _uiManager._turboBar.maxValue;
                }
                if (_speedLives == 0)
                {
                    canTurbo = false;
                }
            }
        }
    }
}