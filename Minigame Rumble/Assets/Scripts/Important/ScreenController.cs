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

    public void LoadRandomGame()
    {
        int rng = Random.Range(0, 100);
        if (rng < 50)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            SceneManager.LoadScene(6);
        }
    }
}
