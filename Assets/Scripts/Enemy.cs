using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// Enemy Behavior Script
    /// Written By: Alex Blackburn
    /// Learned through: GameDevHQ
    /// Project Name: Space Shooter Pro 2D
    /// </summary>

    [SerializeField] private float _movementSpeed = 4f;
    private float yMin = -8f, yMax = 8f;
    private float _enemyFireRate = 3f;
    private float _enemyCanFire = -1f;

    private Player _player;
    private Laser __laser;

    private Animator _enemyAnimation;
    private Collider2D _collider;
    private AudioSource _explosionAudio;
    [SerializeField] private GameObject _laserPrefab;

    // Start is called before the first frame update
    void Start()
    {
        _explosionAudio = GetComponent<AudioSource>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _enemyAnimation = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovementCalculation();

        if(Time.time > _enemyCanFire)
        {
            _enemyFireRate = Random.Range(3f, 7f);
            _enemyCanFire = Time.time + _enemyFireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

            for(int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
        }
    }

    private void EnemyMovementCalculation()
    {
        transform.Translate(Vector3.down * _movementSpeed * Time.deltaTime);

        if (transform.position.y < yMin)
        {
            float randomXRange = Random.Range(-9.37f, 9.37f);
            transform.position = new Vector3(randomXRange, yMax, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            other.transform.GetComponent<Player>().Damage();
            _enemyAnimation.SetTrigger("OnEnemyDestroyed");
            _collider.enabled = false;
            _explosionAudio.Play();
            Destroy(this.gameObject, 2.4f);
            Debug.Log("Enemy Destroyed");
        }

        if (other.tag == "Laser") ;
        {
            Destroy(other.gameObject);
            if(_player != null)
            {
               _player.AddScore(10);
            }
            _enemyAnimation.SetTrigger("OnEnemyDestroyed");
            _collider.enabled = false;
            _explosionAudio.Play();
            Destroy(this.gameObject, 2.4f);
            Debug.Log("Enemy Destroyed");
         
        }
    }
}

