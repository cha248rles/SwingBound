using UnityEngine;

public class PauseMenuBehavior : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    bool isGamePaused = false;

    void Start()
    {
        
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isGamePaused)
            {
                // resume the game
                ResumeGame();
            }
            else
            {
                // pause the game
                PauseGame();
            } 
        }
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        pauseMenuPanel.SetActive(false);
    }

    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
        pauseMenuPanel.SetActive(true);
    }

    public void LoadMainMenu()
    {
        Debug.Log("Loading the main menu scene");
        //SceneManager.LoadScene(0);
    }
    
    public void ExitGame()
    {
        Debug.Log("Exiting the game");
        Application.Quit();
    }
}
