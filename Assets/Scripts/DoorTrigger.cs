using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    public bool isExitDoor = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (PlayerIsLookingAtDoor())
            {
                if (LightSwitchChallenge.Instance != null && LightSwitchChallenge.Instance.challengeActive)
                {
                    Debug.Log("Door is disabled during the light switch challenge.");
                    return;
                }

                EvaluateChoice();
            }
        }
    }

    private bool PlayerIsLookingAtDoor()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 3f))
        {
            return hit.collider.gameObject == gameObject;
        }
        return false;
    }

    private void EvaluateChoice()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        bool isBase = currentScene == "Base";

        bool playerChoseNoAnomaly = isExitDoor;
        bool correct = (isBase && playerChoseNoAnomaly) || (!isBase && !playerChoseNoAnomaly);

        if (correct)
        {
            Debug.Log("Correct choice. Advancing to next level.");
            GameManager.Instance.LoadNextLevel();
        }
        else
        {
            Debug.Log("Incorrect choice. Restarting game.");
            GameManager.Instance.RestartGame();
        }
    }
}

