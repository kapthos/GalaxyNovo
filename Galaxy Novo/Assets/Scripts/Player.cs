using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject shields;

    private float _horizontalInput;
    private float _verticalInput;

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

    //References
    private UIManager _uiManager;
    private GameManager _gm;

    void Start()
    {
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        _currentTurbo = 0;
    }

    void Update()
    {
        Movement();
        PlayerDeath();
        TurnTurboOn();
    }
    public void Movement()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * _horizontalInput * _currentSpeed * Time.deltaTime);
        transform.Translate(Vector3.up * _verticalInput * _currentSpeed * Time.deltaTime);

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
    public void Damage()
    {
        if (_shieldOn == true)
        {
            _shieldOn = false;
            shields.SetActive(false);
            return;
        }

        _lives--;
        _uiManager.UpdateLives(_lives);
    }
    public void ShieldOn()
    {
        _shieldOn = true;
        shields.SetActive(true);
        StartCoroutine(ShieldPowerDownRoutine());
    }
    IEnumerator ShieldPowerDownRoutine()
    {
        yield return new WaitForSeconds(_shieldDuration);
        shields.SetActive(false);
        _shieldOn = false;
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

                if (_currentTurbo > 0)
                {
                    _currentSpeed = _turboSpeed;
                }
                else
                {
                    _currentSpeed = _defaultSpeed;
                }
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                if (_currentTurbo > 0)
                {
                    _currentSpeed = _defaultSpeed;
                }
                else
                {
                    _speedLives--;
                    _uiManager.UpdateTurbo(_speedLives);
                    _currentSpeed = _defaultSpeed;
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