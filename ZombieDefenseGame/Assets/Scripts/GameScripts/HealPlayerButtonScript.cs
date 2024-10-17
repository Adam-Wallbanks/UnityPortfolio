using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class HealPlayerButtonScript : MonoBehaviour
{
    public int requiredResources = 200;
    public PlayerHealth playerHealth;
    public TextMeshProUGUI buttonText;
    public MinerScript playerResources;

    // Start is called before the first frame update
    private void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = "Heal (" + requiredResources + " resources)";


    }

    public void OnClick()
    {
        Debug.Log("button clicked");
        Debug.Log("max :" + playerHealth.maxHealth);
        Debug.Log("current :" + playerHealth.PlayersHealth);
        Debug.Log(playerResources.totalResources);
        if (playerHealth.PlayersHealth < playerHealth.maxHealth && playerResources.totalResources  >= requiredResources)
        {
            playerHealth.heal(100);
            playerResources.totalResources -= requiredResources;
        }
        else
        {
            Debug.Log("not enough resources");
            Debug.Log(playerHealth.PlayersHealth);
            Debug.Log(playerResources.totalResources + "in else");

        }
    }
}


