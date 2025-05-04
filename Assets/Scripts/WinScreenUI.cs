using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenUI : MonoBehaviour
{
    public void OnReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
