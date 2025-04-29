using UnityEngine;
using UnityEngine.SceneManagement;
public class ScreenController : MonoBehaviour
{
    public void LoadScene(int sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log("Loading Scene....!");
    }

    public void ResetWins()
    {
        MainGameManager.Instance.ResetWins();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
