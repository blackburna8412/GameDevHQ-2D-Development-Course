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
    [SerializeField] private GameObject[] _damageEngines;
    [SerializeField] private GameObject _explosionPrefab;

    [SerializeField] private SpawnManager _spawnManager;

    [SerializeField] private float _fireRate = .5f;
    private float _canFire = -1f;
    [SerializeField] private float _movementSpeed = 3.5f;
    private float xMin = -11.31f, xMax = 11.31f;
    private float yMin = -3.64f, yMax = 0f;
    private float boostedSpeed = 2;

    [SerializeField] private int _healthCount = 3;
    [SerializeField] public int _score = 0;

    [SerializeField] private bool _isTripleShotActive = false;
    [SerializeField] private bool _isSpeedBoostActive = false;
    [SerializeField] private bool _isShieldActive = false;

    private UIManager _uiManager;
    private AudioSource _laserAudio;

    //Certification Variables
    [SerializeField] private float _thrusterSpeed = 7.5f;
    [SerializeField] private int _shieldStrength = 0;
    [SerializeField] private int _ammoCount = 15;
    [SerializeField] private bool _isSpreadShotActive = false;
    [SerializeField] private GameObject _spreadShotPrefab;
    // Start is called before the first frame update
    void Start()
    {
        _laserAudio = GetComponent<AudioSource>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        if(_laserPrefab == null)
        {
            Debug.LogError("_laserPrefab on player is NULL");
        }
        if(_spawnManager == null)
        {
            Debug.LogError("_spawnManager on player is NULL");
        }
        if(_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        CheckHealthVisualizer();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
        
        _uiManager.UpdateShieldText(_shieldStrength);
        _uiManager.UpdateAmmoText(_ammoCount);
    }

    private void CheckHealthVisualizer()
    {
        if (_healthCount <= 2)
        {
            _damageEngines[0].SetActive(true);
        }
        else
        {
            _damageEngines[0].SetActive(false);
        }
        if (_healthCount == 1)
        {
            _damageEngines[1].SetActive(true);
        }
        else
        {
            _damageEngines[1].SetActive(false);
        }
    }

    private void FireLaser()
    {
        Vector3 offset = transform.position;
        offset.y += 1.05f;

        _canFire = Time.time + _fireRate;

        if(_ammoCount >= 1)
        {
            if(_isTripleShotActive == true)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else if(_isSpreadShotActive == true)
            {
                Instantiate(_spreadShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, offset, Quaternion.identity);
            }

            _ammoCount--;
            _laserAudio.Play();
        }

    }

    private void CalculateMovement()
    {
        float _hInput = Input.GetAxis("Horizontal");
        float _vInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(_hInput, _vInput, 0);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            ThrusterSpeed(direction);
        }
        else
        {
            transform.Translate(direction * _movementSpeed * Time.deltaTime);
        }

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

    private void ThrusterSpeed(Vector3 direction)
    {
        transform.Translate(direction * _thrusterSpeed * Time.deltaTime);
    }

    public void Damage()
    {
        if(_isShieldActive == true)
        {
            if(_shieldStrength == 1)
            {
                _shieldStrength = 0;
                _isShieldActive = false;
                _shieldObject.SetActive(false);
            }
            else
            {
                _shieldStrength--;
            }
        }


        if(_healthCount > 0 && _isShieldActive != true)
        {
            _healthCount--;
            _uiManager.UpdateLives(_healthCount);
            Debug.Log("Remaining health: " + _healthCount);         
        }

        if(_healthCount < 1 &&_isShieldActive != true)
        {
            _spawnManager.StopSpawning();
            Destroy(this.gameObject);
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
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
        _shieldStrength = 3;
        _isShieldActive = true;
        _shieldObject.SetActive(true);
        _uiManager.UpdateShieldText(_shieldStrength);
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScoreText(_score);
    }

    public void AmmoPickUp()
    {
        _ammoCount = 15;
        _uiManager.UpdateAmmoText(_ammoCount);
    }

    public void HealthPickUp()
    {
        if(_healthCount < 3)
        {
            _healthCount++;
            _uiManager.UpdateLives(_healthCount);
        }
    }

    public void SpreadShotPickUp()
    {
        _isSpreadShotActive = true;
        StartCoroutine(SpreadShotRoutine());
    }

    IEnumerator SpreadShotRoutine()
    {
        yield return new WaitForSeconds(5);
        _isSpreadShotActive = false;
    }
}
