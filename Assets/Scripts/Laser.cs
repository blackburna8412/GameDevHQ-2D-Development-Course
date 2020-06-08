using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    /// <summary>
    /// Laser Control Script
    /// Written By: Alex Blackburn
    /// Learned through: GameDevHQ
    /// Project: Space Shooter Pro 2D
    /// </summary>

    //Float Variables
    [SerializeField] private float _speed = 8.0f;
    private float yMax = 8f;

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
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y > yMax)
        {
            Destroy(this.gameObject);
        }
    }
}
