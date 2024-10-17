using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    
    // initiating the animator, the animator controller and sprite renderer
    SpriteRenderer mySpriteRenderer;
    Animator myAnimator;
    RuntimeAnimatorController idleAnimation;
    RuntimeAnimatorController walkAnimation;
    RuntimeAnimatorController dieAnimation;
    // creating a running speed reference
    public float RunSpeed = 1f;
    // a boolean on whether character is flipped or not

    [SerializeField] public bool flipped = false;

    public Rigidbody2D rigidBod;

    Vector2 playerMove;


    public RuntimeAnimatorController die, walk, idle;
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        playerMove.x = Input.GetAxisRaw("Horizontal");
        playerMove.y = Input.GetAxisRaw("Vertical");


        if (!Input.anyKey)
        {
            myAnimator.runtimeAnimatorController = idle;
        }
        else if (playerMove.x < 0)
        {
            myAnimator.runtimeAnimatorController = walk;
            flipped = true;
            
        }
        if (playerMove.x > 0)
        {
            // walk right, and flip the sprite in X
            myAnimator.runtimeAnimatorController = walk;
            flipped = false;
           
        }



        if (flipped)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    private void FixedUpdate()
    {
        rigidBod.MovePosition(rigidBod.position + playerMove * RunSpeed * Time.fixedDeltaTime);
    }

}
