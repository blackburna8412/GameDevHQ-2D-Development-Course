using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 4f;
    private float yMin = -7f, yMax = 7f;

    [SerializeField] private GameObject _laserPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _movementSpeed * Time.deltaTime);

        if(transform.position.y < yMin)
        {
            float randomXRange = Random.Range(-9.37f, 9.37f);
            transform.position = new Vector3(randomXRange, yMax, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.GetComponent<Player>().Damage();
            Destroy(this.gameObject);
        }
        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
