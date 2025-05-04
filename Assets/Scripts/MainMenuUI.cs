using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void OnStartGame()
    {
        GameManager.Instance.StartGame();
    }

    public void OnQuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game clicked (won't work in editor)");
    }
}
