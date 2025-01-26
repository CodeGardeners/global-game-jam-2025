using UnityEngine;

public class Menu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject MainMenu;
    public GameObject StoryMenu;
    public GameObject TutorialMenu;

    public void Start()
    {
        MainMenu.SetActive(true);
        StoryMenu.SetActive(false);
        TutorialMenu.SetActive(false);
    }

    public void StartButton()
    {
        MainMenu.SetActive(false);
        StoryMenu.SetActive(false);
        TutorialMenu.SetActive(true);
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

    public void SkipButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
}
