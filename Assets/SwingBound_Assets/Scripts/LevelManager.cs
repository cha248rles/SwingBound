using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class LevelManager : MonoBehaviour
{
    public static bool IsPlaying {get; private set;}
    
    [SerializeField] public bool isFinalLevel;
    [SerializeField] public string nextLevel;
    [SerializeField] public TMP_Text messageText;
    [SerializeField] public GameObject nextButton;
    [SerializeField] public GameObject restartButton;
    [SerializeField] public AudioClip winSFX;
    [SerializeField] public AudioClip loseSFX;
    
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    

    void Start()
    {
        IsPlaying = true;
    }

void Update()
{
    if (IsPlaying || !Input.GetMouseButtonDown(0))
        return;
    if (restartButton != null && restartButton.activeSelf && ClickedOn(restartButton))
        ReloadSameScene();
    else if (nextButton != null && nextButton.activeSelf && ClickedOn(nextButton))
        LoadNextLevel();
}

private bool ClickedOn(GameObject button)
{
    RectTransform rect = button.GetComponent<RectTransform>();
    return RectTransformUtility.RectangleContainsScreenPoint(rect, Input.mousePosition);
}

    public void LevelBeat()
    {
        IsPlaying = false;
        PlaySoundClip(winSFX);
        
        if (isFinalLevel)
        {
            DisplayGameMessage("GAME COMPLETE!");
            restartButton.SetActive(true);
        }
        else
        {
            DisplayGameMessage("YOU WIN!");
            nextButton.SetActive(true);
        }
    }

    public void LevelLost()
    {
        IsPlaying = false;
        PlaySoundClip(loseSFX);
        DisplayGameMessage("DEAD!");
        restartButton.SetActive(true);
    }

    private void PlaySoundClip(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    private void DisplayGameMessage(string message)
    {
        if (messageText != null)
        {
            messageText.gameObject.SetActive(true);
            messageText.text = message;
        }
    }

    public void LoadSceneByName(string name)
    {
        Debug.Log("Loading scene: " + name);
        SceneManager.LoadScene(name);
    }

    private void ReloadSameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        Debug.Log("LoadNextLevel called!");
        
        if (isFinalLevel)
        {
            SceneManager.LoadScene("Level1");
        }
        else if (nextLevel != null && nextLevel.Length > 0)
        {
            LoadSceneByName(nextLevel);
        }
        else
        {
            Debug.LogError("No nextLevel specified in Inspector!");
        }
    }
}