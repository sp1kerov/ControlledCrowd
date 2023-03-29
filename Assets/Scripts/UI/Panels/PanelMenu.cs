using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelMenu : Panel 
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}