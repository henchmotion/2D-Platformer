using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{   
    [Header ("Health")]
   
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

     [Header("iframes")]
     [SerializeField] private float iframesDuration;
     [SerializeField] private int numberOfflashes;
     private SpriteRenderer spriteRend;

     [Header ("Components")]
     [SerializeField] private Behaviour[] components;
     private bool Invunerable;

     [Header ("Death Sound")]
     [SerializeField] private AudioClip deathSound;
     [SerializeField] private AudioClip hurtSound;

    public void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();  
    }

    public void TakeDamage(float _damage)
    {
        if (Invunerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("Hurt");
            StartCoroutine(Invunerability());
            SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            if (!dead)
            {

            // Deactivate all attached components
                foreach (Behaviour component in components)
                {
                    component.enabled = false;

                    anim.SetBool("grounded", true);
                    anim.SetTrigger("Die");

                    dead = true;
                    SoundManager.instance.PlaySound(deathSound);
                }

                
            }
            
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    public void Respawn()
    { 
        dead = false;
        AddHealth(startingHealth);
        anim.ResetTrigger("Die");
        anim.Play("Idle");
        StartCoroutine(Invunerability());

        // Activate all attached components
        foreach (Behaviour component in components)
            component.enabled = true;
            
    }

    private IEnumerator Invunerability()
    {
        Invunerable = true;
        Physics2D.IgnoreLayerCollision(8, 9, true);
        for (int i = 0; i < numberOfflashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iframesDuration /(numberOfflashes *2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iframesDuration /(numberOfflashes *2));
        }
        
         Physics2D.IgnoreLayerCollision(8, 9, false);
         Invunerable = false;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

