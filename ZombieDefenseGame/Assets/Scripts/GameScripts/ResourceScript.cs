using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;


public class ResourceScript : MonoBehaviour
{
    

    public int resourceHealth = 100;
    public float resourceAmount = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(resourceHealth <= 0)
        {
            FullyMined();
        }
    }

    public void DamageResource(int resourceDamage)
    {
        resourceHealth -= resourceDamage;
        resourceAmount += Mathf.CeilToInt(resourceDamage / 2);

        Debug.Log("resources gained " + resourceAmount);


    }

    private void FullyMined()
    {
        Destroy(gameObject);
    }
}
