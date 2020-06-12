using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;

    //ID for powerups [0 = speed, 1 = Speed, 2 = Shield]
    [SerializeField] private int _powerUpID;
    private AudioSource _powerUpAudio;


    // Start is called before the first frame update
    void Start()
    {
        _powerUpAudio = GameObject.Find("PowerUp").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -6f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            _powerUpAudio.Play();

            if(player!= null)
            {
                switch (_powerUpID)
                {
                    case 0:
                        Debug.Log("Collected Triple Shot");
                        player.TripleShotActive();
                        break;
                    case 1:
                        Debug.Log("Collected Speed Boost");
                        player.SpeedPowerUp();
                        break;
                    case 2:
                        Debug.Log("Collected Shields");
                        player.ShieldPowerUp();
                        break;
                    case 3:
                        Debug.Log("Ammo Replenished!");
                        player.AmmoPickUp();
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
