using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelWin : Panel
{
    [SerializeField] private int _nextLevel;

    public void NextLevel()
    {
        SceneManager.LoadScene(_nextLevel);
    }
    
    public void GoMenu()
    {
        SceneManager.LoadScene(0);
    }
}
