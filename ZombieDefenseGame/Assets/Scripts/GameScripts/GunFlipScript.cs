using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFlipScript : MonoBehaviour
{
    public PlayerMovement PlayerScript;

    public GameObject GunHolder;
    public GameObject hand;

    // Update is called once per frame
    void Update()
    {
        if(PlayerScript.flipped == true)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            transform.localPosition = new Vector3(- 0.37f*2 ,0,0 );
        }
        else
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }
}
