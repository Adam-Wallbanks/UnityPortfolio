using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisiontest : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Base")) {
            Debug.Log("base collision success");
        }
    }
}
