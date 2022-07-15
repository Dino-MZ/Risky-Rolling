using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerSO player;
    [SerializeField] int currentHealth;

    public GameObject HealthBar;
    [SerializeField] private HitStop hitStop;
    public bool isDead;
    private FlashEffect flashEffect;

    [SerializeField] private AudioSource damageAudio;
    [SerializeField] private AudioSource healAudio;

    void Start()
    {
        currentHealth = player.MaxHealth;
        flashEffect = gameObject.GetComponent<FlashEffect>();
        HealthBar.GetComponent<PlayerHealthBar>().SetMaxHealth(player.MaxHealth);
    }

    void Update()
    {
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            StartCoroutine(Lose());
            Debug.Log("Player has Died");
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        HealthBar.GetComponent<PlayerHealthBar>().SetHealth(currentHealth);

        damageAudio.Play();

        flashEffect.Flash();

        hitStop.StopTime(player.HitStopDuration);

        Debug.Log("Damage taken");
        Debug.Log(currentHealth);

    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        HealthBar.GetComponent<PlayerHealthBar>().SetHealth(currentHealth);

        healAudio.Play();

        if (currentHealth > player.MaxHealth)
        {
            currentHealth = player.MaxHealth;

        }
    }
    private IEnumerator Lose()
    {
        yield return Waiter.GetWait(1);
        SceneManager.LoadScene(3);
    }
}