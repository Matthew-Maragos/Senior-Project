using UnityEngine;
using UnityEngine.UI;

public class HowManyPlayersScreenManager : MonoBehaviour
{
    public Button twoPlayerButton;
    public Button threePlayerButton;
    public Button fourPlayerButton;

    void Start()
    {
        if (twoPlayerButton != null)
            twoPlayerButton.onClick.AddListener(() => MainGameManager.Instance.SetPlayerCount(2));

        if (threePlayerButton != null)
            threePlayerButton.onClick.AddListener(() => MainGameManager.Instance.SetPlayerCount(3));

        if (fourPlayerButton != null)
            fourPlayerButton.onClick.AddListener(() => MainGameManager.Instance.SetPlayerCount(4));
    }
}