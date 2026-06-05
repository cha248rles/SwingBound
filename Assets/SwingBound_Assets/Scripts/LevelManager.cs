using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class LevelManager : MonoBehaviour
{
    public static bool IsPlaying {get; private set;}
    
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
    }

    public void LevelBeat()
    {
        IsPlaying = false;
        PlaySoundClip(winSFX);
        DisplayGameMessage("YOU WIN!");
        nextButton.SetActive(true);
    }

    public void LevelLost()
    {
        IsPlaying = false;
        PlaySoundClip(loseSFX);
        DisplayGameMessage("GAME OVER!");
        Invoke("ReloadSameScene", 2);
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
        Debug.Log("nextLevel: " + nextLevel);
        
        if (nextLevel != null && nextLevel.Length > 0)
        {
            LoadSceneByName(nextLevel);
        }
        else
        {
            Debug.LogError("No nextLevel specified in Inspector!");
        }
    }
}