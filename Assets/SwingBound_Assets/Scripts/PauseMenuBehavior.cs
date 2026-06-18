using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehavior : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public static bool IsPaused { get; private set; }
    bool isGamePaused = false;

    // Tracks whether the cursor was pointer-locked last frame so we can detect
    // when the browser releases the lock (the WebGL Escape-key behaviour).
    bool wasCursorLocked = false;

    void Start()
    {
        // Make sure the pause menu is hidden when the scene starts
        pauseMenuPanel.SetActive(false);
    }

    void Update()
    {
        // In WebGL the browser intercepts the Escape key to exit pointer lock,
        // so Input.GetKeyDown(Escape) is usually swallowed during gameplay.
        // Detect the resulting pointer-lock release and treat it as "pause".
        bool cursorLocked = Cursor.lockState == CursorLockMode.Locked;
        if (!isGamePaused && wasCursorLocked && !cursorLocked)
        {
            PauseGame();
        }
        wasCursorLocked = cursorLocked;

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
