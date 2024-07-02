using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header ("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header ("Pause Game")]
     [SerializeField] private GameObject pauseScreen;


    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If pause scree already activr unpause and viceversa
            if(pauseScreen.activeInHierarchy)
                PauseGame(false);
            else    PauseGame(true);
        }
    }

   #region Game Over
    //Activate game over screen
        public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound); 
    }
    // Game over functions
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

     public void Quit()
    {
        #if UNITY_EDITOR 
        Application.Quit(); //Quits the game (doesn't work on unity but on build)
        UnityEditor.EditorApplication.isPlaying = false; // Exits playmode from inside te game.(will only be executed inside the editor.)
        #endif
;    }
    #endregion


    #region Pause
    public void PauseGame(bool status)
    {
        // If Status == true pause | if status == false unpause
        pauseScreen.SetActive(status);

        //When pause status is true change timescale to 0 (time stops)
        // When it's false change it back to 1 ( time goes by normally)
        if (status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void SoundVolume()
    {

    }

     public void MusicVolume()
    {
        
    }
    #endregion
}




