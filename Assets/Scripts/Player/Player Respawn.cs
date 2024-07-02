using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint; // We'll store our ;ast checkpoint here
    private Health playerHealth;
    private UIManager uiManager;

    private void Awake()
    {  
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
    }

    public void CheckRespawn()
    {
        // Check if checkpoint availabele
        if (currentCheckpoint == null)
        {
            // Show game over screen
            uiManager.GameOver();

            return; // Don't execute the rest of this functio.
        }

        transform.position = currentCheckpoint.position; // Move player to checkpoint position
        playerHealth.Respawn(); // Restore player health and reset animation

        // Move camera to checkpoint room (** For the to work, the checkpoint object has to be palced 
        // as a child of thr room object)
        Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent);
    } 

    // Activate checkpoints
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform; // Store the checkpoint that we activated as the current one
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false; // Deactivate checkpoint collider
            collision.GetComponent<Animator>().SetTrigger("appear"); //Trigger checkpoint animation
        }
    }

}
