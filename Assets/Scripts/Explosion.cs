using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private AudioSource _explosionAudio;
    // Start is called before the first frame update
    void Start()
    {
        _explosionAudio = GetComponent<AudioSource>();
        _explosionAudio.Play();
        Destroy(this.transform.gameObject, 3f);
    }

}
