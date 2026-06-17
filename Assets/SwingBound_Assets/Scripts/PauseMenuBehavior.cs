using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehavior : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public static bool IsPaused { get; private set; }
    bool isGamePaused = false;

    void Start()
    {
        // Make sure the pause menu is hidden when the scene starts
        pauseMenuPanel.SetActive(false);
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
        IsPaused = false;
        Time.timeScale = 1f;
        pauseMenuPanel.SetActive(false);

        // Re-lock and hide the cursor for gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PauseGame()
    {
        isGamePaused = true;
        IsPaused = true;
        Time.timeScale = 0f;
        pauseMenuPanel.SetActive(true);

        // Free and show the cursor so the player can click menu buttons
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadMainMenu()
    {
        Debug.Log("Loading the main menu scene");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    
    public void ExitGame()
    {
        Debug.Log("Exiting the game");
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
            }
}
