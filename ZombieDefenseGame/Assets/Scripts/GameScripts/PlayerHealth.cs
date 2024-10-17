using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int PlayersHealth = 200;
    public int maxHealth = 200;
    public ParticleSystem bloodParticles;
    public VolumeSO volumeVar;
    public AudioSource deathSound;
    // Start is called before the first frame update
    void Start()
    {
        bloodParticles.Stop();

        ParticleSystem.MainModule bloodMainModule = bloodParticles.main;
        bloodMainModule.duration = 6.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        bloodParticles.Pause();    
    }

    public void TakeDamage(int damage)
    {
        PlayersHealth -= damage;
        Debug.Log("Damage taken");
        if (PlayersHealth <= 0)
        {
            PlayersHealth = 0;
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        if (!(volumeVar.volume == 0))
        {
            deathSound.volume = volumeVar.volume;
        }
        else
        {
            volumeVar.volume = 1;
            deathSound.volume = volumeVar.volume;
        }
        deathSound.Play();

        bloodParticles.Play();

        yield return new WaitForSeconds(1);
        bloodParticles.Stop();

        Destroy(gameObject);
        Debug.Log("Player is dead");

        SceneManager.LoadScene("GameOverScreen");
    }

    public void heal(int healAmount)
    {
        PlayersHealth += healAmount;
        if (PlayersHealth > 200)
        {
            PlayersHealth = 200;
        }

    }
}
