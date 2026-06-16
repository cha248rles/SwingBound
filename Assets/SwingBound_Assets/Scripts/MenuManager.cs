using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject levelSelect;
    void Start()
    {
        mainMenu.SetActive(true);
        levelSelect.SetActive(false);
    }

    public void SwitchToLevelSelect()
    {
        levelSelect.SetActive(true);
        mainMenu.SetActive(false);
        Debug.Log("clicked");
    }

    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        levelSelect.SetActive(false);
    }
}
