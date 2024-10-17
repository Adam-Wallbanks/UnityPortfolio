using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{


    // initiating the animator, the animator controller and sprite renderer
    SpriteRenderer mySpriteRenderer;
    // creating a running speed reference
    public float RunSpeed = 10f;

    public Rigidbody2D rigidBod;

    Vector2 playerMove;


    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        
    }


    // Update is called once per frame
    void Update()
    {
        playerMove.x = Input.GetAxisRaw("Horizontal");
        playerMove.y = Input.GetAxisRaw("Vertical");
        Color pink = new Color(255.0f,182.0f,193.0f);
    }

    private void FixedUpdate()
    {
        rigidBod.MovePosition(rigidBod.position + playerMove * RunSpeed * Time.fixedDeltaTime);
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) 
        {
            Destroy(this);
        }
    }

}