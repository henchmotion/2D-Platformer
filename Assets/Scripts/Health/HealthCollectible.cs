using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
  [SerializeField] private float healthValue;

  [Header ("SFX")]
  [SerializeField] private AudioClip lifeSound;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.tag == "Player")
    {
        collision.GetOrAddComponent<Health>().AddHealth(healthValue);
        gameObject.SetActive(false);
        SoundManager.instance.PlaySound(lifeSound);
    }

  }
}
