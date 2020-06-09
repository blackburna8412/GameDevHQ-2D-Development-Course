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

    //Float Variables
    [SerializeField] private float _movementSpeed = 3.5f;
    private float xMin = -11.31f, xMax = 11.31f;
    private float yMin = -3.8f, yMax = 0f;

    //GameObject variables
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private float _fireRate = .5f;
    private float _canFire = -1f;
    //Bool Variables

    //Int Variables
    [SerializeField] private int _healthCount = 3;
    //String Variables

    //Vector 3 Variables

    // Start is called before the first frame update
    void Start()
    {
        if(_laserPrefab == null)
        {
            Debug.LogError("_laserPrefab is NULL");
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
        //These lines of code clone a laser object with on the y axis with an offset of 1 when spacebar is pressed
        //at a rate of 1 object every .15 seconds
        //**Always assign laser prefab in the inspector or it will not work!**
        Vector3 offset = transform.position;
        offset.y += .8f;

        _canFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, offset, Quaternion.identity);
    }

    private void CalculateMovement()
    {
        //the following lines convert player movement input into a float value
        //which is then used to create a new optimize local Vector3 variable
        float _hInput = Input.GetAxis("Horizontal");
        float _vInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(_hInput, _vInput, 0);
        
        //                new Vector3((-1 or 1), (-1 or 1), 0) * 3.5f * real time
        transform.Translate(direction * _movementSpeed * Time.deltaTime);

        //these lines constrain the player to within -3.8f and 0 on the y axis
        if (transform.position.y > yMax)
        {
            transform.position = new Vector3(transform.position.x, yMax, transform.position.z);
        }
        else if (transform.position.y < yMin)
        {
            transform.position = new Vector3(transform.position.x, yMin, transform.position.z);
        }

        //these lines of code constrain the player within -11.31f and 11.31f on the X Axis 
        //which will loop the player giving the appearance of exiting one side of the screen 
        //and enter back from the other side of the screen
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
        //This section of code checks if the _healthCounter variable has a value of greater than 0
        //If this comes back true, the value is reduced by 1
        if(_healthCount > 0)
        {
            _healthCount--;
            Debug.Log("Remaining health: " + _healthCount);
        }

        //This section of code checks if the _healthCounter variable has a value of less than 1
        //If this comes back true, the player object is destroyed, and game over
        if(_healthCount < 1)
        {
            Destroy(this.gameObject);
            Debug.Log("Game Over!");
        }
    }
}
