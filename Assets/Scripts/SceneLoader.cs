using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void LoadSceneWithDelay(string targetScene)
    {
        StartCoroutine(LoadSceneRoutine(targetScene));
    }

    private IEnumerator LoadSceneRoutine(string targetScene)
    {
        SceneManager.LoadScene("LoadingScreen");
        yield return null;

        LoadingScreenController loader = null;
        float timeout = 5f;
        float timer = 0f;

        while (loader == null && timer < timeout)
        {
            loader = FindObjectOfType<LoadingScreenController>();
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        if (loader != null)
        {
            loader.SetTargetScene(targetScene);
        }
        else
        {
            Debug.LogError("Failed to find LoadingScreenController after scene load.");
        }
    }
}





