using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnerScript : MonoBehaviour
{
    private int numOfZombies;
    private int waveNum = 1;
    public GameObject zombie;
    private int respawnNum = 4;

    // Start is called before the first frame update
    void Start()
    {
        numOfZombies = GameObject.FindGameObjectsWithTag("Zombie").Length;
    }

    // Update is called once per frame
    void Update()
    {
        numOfZombies = GameObject.FindGameObjectsWithTag("Zombie").Length;
        if ((numOfZombies == 0))
        {
            waveNum += 1;
            RespawnZombies(waveNum);
        }
        
    }

    void RespawnZombies(int wave)
    {
        respawnNum *= wave;
        // a for loop to instantiate the number of zombies given by the respawn number, these will be spawned in a random range provided by the vector 3
        for (int i = 0; i < respawnNum; i++)
        {
            // calculating a random position between -20 and 20
            Vector3 randomPosition = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0f);
            // instantiating the zombie gameobject in the random position generated
            Instantiate(zombie, randomPosition, Quaternion.identity);
        }
    }
}
