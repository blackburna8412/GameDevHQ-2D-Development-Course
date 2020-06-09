﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    public bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        if(_enemyPrefab == null)
        {
            Debug.LogError("_enemyPrefab not assigned to SpawnManager");
        }
        if(_enemyContainer == null)
        {
            Debug.LogError("_enemyContainer not assigned to SpawnManager");
        }

        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StopSpawning()
    {
        _stopSpawning = true;
    }

    //Spawn game objects every 5 seconds
    //Create a coroutine of IEnumerator -- YYield Events
    //while loop 
    public IEnumerator SpawnRoutine()
    {
        while(_stopSpawning == false)
        {
            Vector3 randPos = new Vector3(Random.Range(-9.37f, 9.37f), 8, 0);
            Instantiate(_enemyPrefab, randPos, Quaternion.identity, _enemyContainer.transform);
            yield return new WaitForSeconds(5);
        }
    }

}
