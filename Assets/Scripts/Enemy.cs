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
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        EnemyBehavior();
    }

    //EnemyBehavior() Variables
    [SerializeField] private float _movementSpeed = 4f;
    private float yMin = -7f, yMax = 7f;
    private void EnemyBehavior()
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
            Destroy(this.gameObject);
            Debug.Log("Enemy Destroyed");
        }

        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            Debug.Log("Enemy Destroyed");
        }
    }
}
