using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _horizontalInput;
    private float _verticalInput;

    [SerializeField] private float _movementSpeed;
    public int _lives;

    [SerializeField] private int _speedLives;
    [SerializeField] private int _gold = 0;

    public bool _shieldOn;
    [SerializeField] private float _shieldDuration;
    public int _score = 0;

    [SerializeField] private GameObject shields;
    private UIManager _uiManager;
    private GameManager _gm;

    void Start()
    {
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();    
    }

    void Update()
    {
        Movement();
        SpeedBoostOn();
        PlayerDeath();
    }

    public void Movement()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * _horizontalInput * _movementSpeed * Time.deltaTime);
        transform.Translate(Vector3.up * _verticalInput * _movementSpeed * Time.deltaTime);

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

    public void SpeedLivesAdded()
    {
        if(_speedLives < 3)
        {
            _speedLives++;
        }
        _uiManager.UpdateTurbo(_speedLives);
    }

    public void SpeedBoostOn()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && _speedLives > 0)
        {
            _movementSpeed = _movementSpeed + 2.0f;
            _speedLives--;
            _uiManager.UpdateTurbo(_speedLives);
            StartCoroutine(SpeedPowerDownRoutine());
        }
    }

    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _movementSpeed = _movementSpeed - 3.5f;
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

}
