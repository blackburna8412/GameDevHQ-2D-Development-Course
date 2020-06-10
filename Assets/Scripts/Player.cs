using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// Summarized Player Control Script
    /// Written By: Alex Blackburn
    /// Course: GameDevHQ 2D Course Development
    /// Project Name: Space Shooter Pro
    /// </summary>

    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private GameObject _shieldObject;

    [SerializeField] private SpawnManager _spawnManager;

    [SerializeField] private float _fireRate = .5f;
    private float _canFire = -1f;
    [SerializeField] private float _movementSpeed = 3.5f;
    private float xMin = -11.31f, xMax = 11.31f;
    private float yMin = -3.64f, yMax = 0f;
    private float boostedSpeed = 2;

    [SerializeField] private int _healthCount = 3;

    [SerializeField] private bool _isTripleShotActive = false;
    [SerializeField] private bool _isSpeedBoostActive = false;
    [SerializeField] private bool _isShieldActive = false;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if(_laserPrefab == null)
        {
            Debug.LogError("_laserPrefab on player is NULL");
        }
        if(_spawnManager == null)
        {
            Debug.LogError("_spawnManager on player is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    private void FireLaser()
    {
        Vector3 offset = transform.position;
        offset.y += 1.05f;

        _canFire = Time.time + _fireRate;

        if(_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, offset, Quaternion.identity);
        }
    }

    private void CalculateMovement()
    {
        float _hInput = Input.GetAxis("Horizontal");
        float _vInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(_hInput, _vInput, 0);

        transform.Translate(direction * _movementSpeed * Time.deltaTime);

        if (transform.position.y > yMax)
        {
            transform.position = new Vector3(transform.position.x, yMax, transform.position.z);
        }
        else if (transform.position.y < yMin)
        {
            transform.position = new Vector3(transform.position.x, yMin, transform.position.z);
        }

        if (transform.position.x > xMax)
        {
            transform.position = new Vector3(xMin, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < xMin)
        {
            transform.position = new Vector3(xMax, transform.position.y, transform.position.z);
        }
    }

    public void Damage()
    {
        if(_isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldObject.SetActive(false);
            return;
        }

        if(_healthCount > 0)
        {
            _healthCount--;
            Debug.Log("Remaining health: " + _healthCount);
        }

        if(_healthCount < 1)
        {
            _spawnManager.StopSpawning();
            Destroy(this.gameObject);
            Debug.Log("Game Over!");
        }

    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _isTripleShotActive = false;
    }

    public void SpeedPowerUp()
    {
        if(_isSpeedBoostActive != true)
        {
            _isSpeedBoostActive = true;
            _movementSpeed *= boostedSpeed;
            StartCoroutine(SpeedBoostCoroutine());
        }
    }

    IEnumerator SpeedBoostCoroutine()
    {
        yield return new WaitForSeconds(5);
        _isSpeedBoostActive = false;
        _movementSpeed /= boostedSpeed;
    }

    public void ShieldPowerUp()
    {
        _isShieldActive = true;
        _shieldObject.SetActive(true);
    }
}
