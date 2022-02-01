using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private int _lives;
    [SerializeField] private float _singleShotCD;
    [SerializeField] private float _tripleShotCD;
    private float _nextFire;
    private bool _canSingleShot = true;
    [SerializeField] private bool _canTripleShot;
    private float _horizontalInput;
    private float _verticalInput;

    [SerializeField] private GameObject _laser;
    [SerializeField] private GameObject _tripleShot;

    void Start()
    {
        transform.position = new Vector3(0, -2, 0);
    }

    void Update()
    {
        Movement();
        Shooting();
        TripleShot();
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
        if (transform.position.y >= 1)
        {
            transform.position = new Vector3(transform.position.x, 1, 0);
        }
        else if (transform.position.y <= -4.5f)
        {
            transform.position = new Vector3(transform.position.x, -4.5f, 0);
        }
    }

    public void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= _nextFire && _canSingleShot)
        {
            Instantiate(_laser, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            _nextFire = Time.time + _singleShotCD;
        }
        if (_canTripleShot == true)
        {
            _canSingleShot = false;
        }
    }

    public void Damage()
    {
        _lives--;

        if(_lives < 1)
        {
            Destroy(this.gameObject);
        }
    }

    public void TripleShot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _canTripleShot == true && Time.time >= _nextFire)
        {
            Instantiate(_tripleShot, transform.position + new Vector3(0,1,0), Quaternion.identity);
            _nextFire = Time.time + _tripleShotCD;
        }
    }

    public void TSPowerUpOn()
    {
        _canTripleShot = true;
        StartCoroutine(TSPowerDownRoutine());
    }
    IEnumerator TSPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _canTripleShot = false;
        _canSingleShot = true;
    }

}
