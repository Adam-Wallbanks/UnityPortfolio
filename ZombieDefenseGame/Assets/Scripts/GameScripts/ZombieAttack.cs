using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    public float attackRange = 1.0f; // the distance at which the zombie will attack the player
    public int attackDamage = 5; // the amount of damage the zombie will do to the player when it attacks
    public float attackCooldown = 3.0f; // the time between attacks
    public float moveSpeed = 1.0f; // the speed at which the zombie will move towards the player
    public AudioSource zombieAttackSound;
    public VolumeSO volumeVar;

    private Transform baseTransform; // transform component for base game object
    private bool isAttacking = false; // a flag to track whether the zombie is currently attacking
    private Transform playerTransform; // the transform component of the player game object
    private Rigidbody2D playerRigidbody; // the rigidbody component of the player game object
    private Rigidbody2D zombieRigidbody; // the rigidbody component of the zombie game object
    public Animator myAnimator; // the Animator component of the zombie game object
    private float attackTimer = 0.0f; // a timer used to track the time between attacks

    // Start is called before the first frame update
    void Start()
    {
        // get the transform of the base game object
        baseTransform = GameObject.FindGameObjectWithTag("Base").transform;

        // get the transform component of the player game object
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // get the rigidbody component of the player game object
        playerRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        // get the rigidbody component of the zombie game object
        zombieRigidbody = GetComponent<Rigidbody2D>();

        // get the Animator component of the zombie game object
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // decrease the attack timer by the time that has passed since the last frame
        attackTimer -= Time.deltaTime;

        // if the zombie is closer to the player move to player or if closer to base move towards the base
        if (Vector2.Distance(transform.position, playerTransform.position) < Vector2.Distance(transform.position, baseTransform.position))
        {
            // move the zombie towards the player
            zombieRigidbody.velocity = (playerTransform.position - transform.position).normalized * moveSpeed;
        }
        else if (Vector2.Distance(transform.position, baseTransform.position) < Vector2.Distance(transform.position, playerTransform.position))
        {
            // move the zombie towards the base
            zombieRigidbody.velocity = (baseTransform.position - transform.position).normalized * moveSpeed;
        }



        zombieAttackSound.volume = volumeVar.volume;

    }

    // attack the player
    void playerAttack()
    {
        // reset the attack timer
        attackTimer = attackCooldown;

        playerRigidbody.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        // set the isAttacking flag to true
        isAttacking = true;

        Debug.Log("is attacking is true");
        myAnimator.SetBool("isAttacking", true);
    }
    // Attack the Base
    void BaseAttack()
    {
        // reset the attack timer
        attackTimer = attackCooldown;

        baseTransform.GetComponent<BaseHealth>().TakeBaseDamage(attackDamage);
        Debug.Log("Base attack active");
        // set the isAttacking flag to true
        isAttacking = true;

        Debug.Log("is attacking is true");
        myAnimator.SetBool("isAttacking", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        zombieAttackSound.Play();
        if (collision.gameObject.CompareTag("Player"))
        {
            playerAttack();
        }
        else if (collision.gameObject.CompareTag("Base"))
        {
            zombieAttackSound.Play();
            BaseAttack();
        }
    }

   private void OnTriggerStay2D(Collider2D collision)
    {
        zombieAttackSound.Play();
        if (attackTimer <= 0)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                playerAttack();
            }
            else if (collision.gameObject.CompareTag("Base"))
            {
                zombieAttackSound.Play();
                BaseAttack();
            }
        }
    }
   

    private void OnTriggerExit2D(Collider2D collision)
    {
        zombieAttackSound.Stop();
        if (!(collision.gameObject.CompareTag("Player")))
        {
            Debug.Log("isAttacking is false");
            myAnimator.SetBool("isAttacking", false);
        }
    }
}
