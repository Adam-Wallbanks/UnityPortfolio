using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    // creating a reference for the point where the bullet is fired from
    public Transform FiringPoint;
    // creating a reference to the bullet that will be fired
    public GameObject Bullet;
    public AudioSource shootSound;
    public VolumeSO volumeVar;

    // Update is called once per frame
    void Update()
    {
        // if statement to detct whther the fire button is being pressed and if so performing the shoot method
        if (Input.GetButtonDown("Fire1"))
        {
            shoot();
        }
    }
    // creating the shoot method
    void shoot() 
    {
        if (!(volumeVar.volume == 0))
        {
            shootSound.volume = volumeVar.volume;
        }
        else
        {
            volumeVar.volume = 1;
            shootSound.volume = volumeVar.volume;
        }
        shootSound.Play();
        // showing the bullet in unity by instantiating the bullet to show at the firing point position and with the firing points rotation
        Instantiate(Bullet, FiringPoint.position, FiringPoint.rotation);

        
    }
}
