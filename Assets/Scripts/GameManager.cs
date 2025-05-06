using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentLevel = 0;
    private List<string> abnormalScenes = new();
    private HashSet<string> usedAbnormals = new();
    private const string baseScene = "Base";
    private const string winScene = "WinScene";
    private Queue<string> sceneSequence = new();

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        abnormalScenes.Add("Abnormality_LightsOff");
        abnormalScenes.Add("Abnormality_HelpMe"); 
        abnormalScenes.Add("Abnormality_Goo");
        abnormalScenes.Add("Abnormality_Alien");

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu" || scene.name == winScene)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void StartGame()
    {
        currentLevel = 0;
        usedAbnormals.Clear();
        sceneSequence.Clear();

        sceneSequence.Enqueue(baseScene);

        List<string> shuffled = new List<string>(abnormalScenes);
        Shuffle(shuffled);

        sceneSequence.Enqueue(shuffled[0]);
        sceneSequence.Enqueue(shuffled[1]);
        sceneSequence.Enqueue(baseScene);

        List<string> finalLevels = new List<string>(sceneSequence);
        string level1 = finalLevels[0];
        finalLevels.RemoveAt(0);
        Shuffle(finalLevels);
        sceneSequence.Clear();
        sceneSequence.Enqueue(level1);
        foreach (string s in finalLevels)
            sceneSequence.Enqueue(s);

        Debug.Log("Generated Scene Sequence:");
        int levelIndex = 1;
        foreach (string scene in sceneSequence)
        {
            Debug.Log($"Level {levelIndex++}: {scene}");
        }
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        currentLevel++;

        if (currentLevel > 4)
        {
            SceneManager.LoadScene("LoadingScreen");
            SceneLoader.Instance.LoadSceneWithDelay(winScene);
            return;
        }

        if (sceneSequence.Count == 0)
        {
            Debug.LogError("Scene sequence is empty!");
            return;
        }

        string nextScene = sceneSequence.Dequeue();
        SceneManager.LoadScene("LoadingScreen");
        SceneLoader.Instance.LoadSceneWithDelay(nextScene);
    }

    private void Shuffle(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int j = Random.Range(i, list.Count);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    public void RestartGame()
    {
        Debug.Log("Restarting game...");
        StartGame();
    }
}

