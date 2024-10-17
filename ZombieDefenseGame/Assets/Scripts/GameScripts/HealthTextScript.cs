using UnityEngine.UI;
using TMPro;
using UnityEngine;


public class HealthTextScript : MonoBehaviour
{
    TMP_Text HealthText;
    public PlayerHealth HealthScript;

    public int health = 200;

    

    void Start()
    {
        health = HealthScript.PlayersHealth;
        //Text sets your text to say this message
        HealthText = GetComponent<TextMeshProUGUI>();
        HealthText.text = "Health: " + health;
    }

    void Update()
    {
        health = HealthScript.PlayersHealth;
        HealthText.text = "Health: " + health;

    }
}
