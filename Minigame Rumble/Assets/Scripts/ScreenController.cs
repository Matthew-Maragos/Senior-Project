using UnityEngine;
using UnityEngine.SceneManagement;
public class ScreenController : MonoBehaviour
{
    public void LoadScene(int sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log("Loading Scene....!");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
