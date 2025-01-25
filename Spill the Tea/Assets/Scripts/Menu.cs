using UnityEngine;

public class Menu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject MainMenu;
    public GameObject StoryMenu;

    public void Start()
    {
        MainMenu.SetActive(true);
        StoryMenu.SetActive(false);
    }

    public void StartButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

    public void StoryButton()
    {
        MainMenu.SetActive(false);
        StoryMenu.SetActive(true);
    }
    
    public void QuitButton()
    {
        Application.Quit();
    }

    public void BackToMenuButton()
    {
        MainMenu.SetActive(true);
        StoryMenu.SetActive(false);
    }
}
