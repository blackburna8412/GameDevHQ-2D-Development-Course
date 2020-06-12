using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    /// <summary>
    /// Laser Control Script
    /// Written By: Alex Blackburn
    /// Learned through: GameDevHQ
    /// Project Name: Space Shooter Pro 2D
    /// </summary>

    [SerializeField] private float _speed = 8.0f;
    private float yMax = 8f;
    [SerializeField] public bool _isEnemyLaser = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LaserBehaviour();
    }

    private void LaserBehaviour()
    {
        if (_isEnemyLaser == false)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
    }

    private void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -yMax)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
    }

    private void MoveUp()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y > yMax)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            {
                if (_isEnemyLaser == true)
                {
                    Player player = other.GetComponent<Player>();
                    if (player != null)
                    {
                        player.Damage();
                    }
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
