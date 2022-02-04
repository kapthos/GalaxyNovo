using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _horizontalInput;
    private float _verticalInput;

    [SerializeField] private float _movementSpeed;
    [SerializeField] public int _lives;

    [SerializeField] private int _speedLives;

    public bool _shieldOn;
    [SerializeField] private float _shieldDuration;
    [SerializeField] public int _score = 0;

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

        if (transform.position.x <= -8.4f)
        {
            transform.position = new Vector3(-8.4f, transform.position.y, 0);
        }
        else if (transform.position.x >= 8.4f)
        {
            transform.position = new Vector3(8.4f, transform.position.y, 0);
        }
        if (transform.position.y >= 3)
        {
            transform.position = new Vector3(transform.position.x, 3, 0);
        }
        else if (transform.position.y <= -4.5f)
        {
            transform.position = new Vector3(transform.position.x, -4.5f, 0);
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
        _speedLives++;
    }

    public void SpeedBoostOn()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && _speedLives > 0)
        {
            _movementSpeed = _movementSpeed + 3.5f;
            _speedLives--;
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

}
