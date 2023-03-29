using UnityEngine.SceneManagement;

public class PanelGameOver : Panel
{
    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoMenu()
    {
        SceneManager.LoadScene(0);
    }
}
