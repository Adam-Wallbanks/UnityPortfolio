using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealBaseButtonScript : MonoBehaviour
{
    public int requiredResources = 500;
    public BaseHealth baseHealthScript;
    public TextMeshProUGUI buttonText;
    public MinerScript playerResources;
    public AudioSource repairSound;
    public VolumeSO volumeVar;
    // Start is called before the first frame update
    private void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = "Base Repair (" + requiredResources + " resources)";

        Debug.Log("start health :"+baseHealthScript.baseHealth);
    }

    public void OnClick()
    {
        Debug.Log("button press");
        Debug.Log("resources : "+playerResources.totalResources);
        if (baseHealthScript.currentHealth < baseHealthScript.maxHealth && playerResources.totalResources >= requiredResources)
        {
            Debug.Log("healing");
            baseHealthScript.heal(200);
            playerResources.totalResources -= requiredResources;
            if (!(volumeVar.volume == 0))
            {
                repairSound.volume = volumeVar.volume;

            }
            else
            {
                volumeVar.volume = 1;
                repairSound.volume = volumeVar.volume;
            }
            repairSound.Play();
        }
        else
        {
            Debug.Log("not enough resources");
            Debug.Log(baseHealthScript.baseHealth);
            Debug.Log(playerResources.totalResources + " in else");

        }
    }
}
