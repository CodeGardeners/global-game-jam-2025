using UnityEngine;

public class EndScreen : MonoBehaviour
{
    public GameObject EndScreenCanvas;

    public void BackToMainMenuButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");
    }
}
