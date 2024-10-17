using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public GameObject player;
    // creating a reference to the zombies health
    public int health = 100;
    public VolumeSO killNum;

    // making a method to be used when zombie takes damage
    public void Damager(int Damage)
    {
        // take the damage amount away from the zombies health
        health -= Damage;
        // if health hits 0 or goes below the die method is called
        if (health <= 0)
        {
            Die();
            Debug.Log("kills =" + killNum.kills);
        }
    }
    // destroy game object on die
   void Die()
    {
        Destroy(gameObject);
        killNum.kills += 1;
    }

    void Update()
    {

        float Zombiepos = transform.position.x;
        float playerPos = player.transform.position.x;
        if (Zombiepos - 1 > playerPos)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else{
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }

    }

    
}
