using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // creating a speed variable that can be changed within unity
    public float BulletSpeed = 0.5f;
    // creating a reference to rigid body to move the bullet
    public Rigidbody2D Rigidbody;
    // creating a reference for the damage to be dealt to the zombie
    public int damage = 40;

    // Start is called before the first frame update
    void Start()
    {
        // making the bullet move forward (to right because its 2d) dependant on speed value
        Rigidbody.velocity = transform.right * BulletSpeed;

        Object.Destroy(gameObject, 3.0f);
    }
    // using on trigger enter to detect collision with other gameobjects
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // setting the collision so it only affects game objects with the ZombieScript
        ZombieScript zombie = collision.GetComponent<ZombieScript>();
        // if there is a zombie cause the zombie to take damage
        if (zombie != null)
        {
            zombie.Damager(damage);
        }
        // destroy the gameobject after collision
        Destroy(gameObject);
    }
}
