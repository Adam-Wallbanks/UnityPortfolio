using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    private int numOfRocks;
    public GameObject rock;
    private int respawnNum = 7;

    // Start is called before the first frame update
    void Start()
    {
        numOfRocks = GameObject.FindGameObjectsWithTag("Rocks").Length;
    }

    // Update is called once per frame
    void Update()
    {
        numOfRocks = GameObject.FindGameObjectsWithTag("Rocks").Length;
        if (numOfRocks <= 1)
        {
            RespawnRocks(respawnNum);
        }
    }

    void RespawnRocks(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-30f, 30f), Random.Range(-12f, 12f), 0.2f);
            Instantiate(rock, randomPosition, Quaternion.identity);
        }
    }
}
