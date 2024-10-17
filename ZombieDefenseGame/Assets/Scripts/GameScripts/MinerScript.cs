using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MinerScript : MonoBehaviour
{
    public float miningCooldown = 1.0f;

    private Transform RockTransform;
    public Transform minerTransform;
    private Rigidbody2D RockRigidBody;
    private float miningTimer = 0.0f;
    private int miningDamage = 20;
    public float moveSpeed = 2.0f;
    private float miningRange = 1.0f;
    public float totalResources = 0.0f;


    public float resources = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        

        RockRigidBody = GameObject.FindGameObjectWithTag("Rocks").GetComponent<Rigidbody2D>();

        RockTransform = GameObject.FindGameObjectWithTag("Rocks").transform;


        resources = RockRigidBody.GetComponent<ResourceScript>().resourceAmount;

    }

    // Update is called once per frame
    void Update()
    {
        RockRigidBody = GameObject.FindGameObjectWithTag("Rocks").GetComponent<Rigidbody2D>();

        RockTransform = GameObject.FindGameObjectWithTag("Rocks").transform;

        // taking time passed away from the mining timer/ time since last mined
        miningTimer -= Time.deltaTime;
        // move the miner towards the rocks

        minerTransform.position = Vector2.MoveTowards(this.transform.position, RockTransform.position, moveSpeed * Time.deltaTime);
        // if the miner is close enough to the rock and the mining timer is 0 then mine resources
        if (Vector2.Distance(transform.position, RockTransform.position) < miningRange && miningTimer <= 0.0f)
        {
            Mine();
            resources = RockRigidBody.GetComponent<ResourceScript>().resourceAmount;

            totalResources += resources;

           
        }

       
    }

    void Mine()
    {
        miningTimer = miningCooldown;

        RockRigidBody.GetComponent<ResourceScript>().DamageResource(miningDamage);

        Debug.Log("Resources damage applied");
    }

    
}

