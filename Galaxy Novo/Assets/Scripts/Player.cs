using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _horizontalInput;
    private float _verticalInput;

    [SerializeField] private float _movementSpeed;
    [SerializeField] private int _lives;

    [SerializeField] private int _speedLives;

    [SerializeField] private bool _shieldOn;
    [SerializeField] private float _shieldDuration;

    void Start()
    {
        transform.position = new Vector3(0, -2, 0);
    }

    void Update()
    {
        Movement();
        SpeedBoostOn();
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
        if (_shieldOn == false)
        {
            _lives--;
        }

        if(_lives < 1)
        {
            Destroy(this.gameObject);
        }
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
        StartCoroutine(ShieldPowerDownRoutine());
    }

    IEnumerator ShieldPowerDownRoutine()
    {
        yield return new WaitForSeconds(_shieldDuration);
        _shieldOn = false;
    }
}
