using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BaseHealth : MonoBehaviour
{
    [SerializeField] public int baseHealth = 400;
    public int maxHealth = 400;
    public TextMeshProUGUI baseHealthText;
    public int currentHealth;
    public AudioSource baseSound;
    public AudioClip baseFireClip;
    public AudioClip baseDeathClip;
    public VolumeSO volumeVar;
    public ParticleSystem fireParticleSystemObject;
    public ParticleSystem explosionParticleSystem;
    // Start is called before the first frame update
    void Start()
    {
        baseHealthText.text = "Base Health: " + baseHealth; 
        fireParticleSystemObject.Stop();
        explosionParticleSystem.Stop();

        ParticleSystem.MainModule fireMainModule = fireParticleSystemObject.main;
        fireMainModule.duration = 6.0f;

        ParticleSystem.MainModule explosionMainModule = explosionParticleSystem.main;
        explosionMainModule.duration = 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("base heath = " + baseHealth);
    }

    void Awake()
    {
        fireParticleSystemObject.Pause();
        explosionParticleSystem.Pause();
    }
    // damage the base function
    public void TakeBaseDamage(int damage)
    {
        Debug.Log("base health : "+baseHealth);
        // take damage off of bases health
        baseHealth -= damage;
        currentHealth = baseHealth;
        baseHealthText.text = "Base Health: " + baseHealth;
        // if base health is less than or equal to 0 end game
        if (baseHealth <= 0 )
        {
            baseHealthText.text = "Base Health: 0";
            Debug.Log("base death");
            StartCoroutine(BaseDeath());
        }
    }

    IEnumerator BaseDeath()
    {
        if (!(volumeVar.volume == 0))
        {
            baseSound.volume = volumeVar.volume;
        }
        else
        {
            volumeVar.volume = 1;
            baseSound.volume = volumeVar.volume;
        }


        fireParticleSystemObject.Play();

        baseSound.clip = baseFireClip;
        baseSound.Play();

        yield return new WaitForSeconds(5);
        fireParticleSystemObject.Stop();


        explosionParticleSystem.Play();

        baseSound.clip = baseDeathClip;
        baseSound.Play();

        yield return new WaitForSeconds(2);

        Destroy(gameObject);

        SceneManager.LoadScene("GameOverScreen");
    }

    public void heal(int healAmount)
    {
        Debug.Log("heal called");
        baseHealth += healAmount;
        if (baseHealth > maxHealth)
        {
            baseHealth = maxHealth;
        }
        baseHealthText.text = "Base Health: " + baseHealth; 
    }
}
