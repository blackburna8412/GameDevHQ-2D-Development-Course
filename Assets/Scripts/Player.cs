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

    //Bool Variables

    //Int Variables

    //String Variables


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
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
}
