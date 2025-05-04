using TMPro;
using UnityEngine;

public class LevelNumberUI : MonoBehaviour
{
    public TMP_Text levelText;

    void Start()
    {
        if (GameManager.Instance != null && levelText != null)
        {
            int level = GameManager.Instance.currentLevel - 1;
            levelText.text = $"LEVEL {level}";
            Debug.Log($"LevelNumberUI updated to Level {level}");
        }
        else
        {
            Debug.LogWarning("LevelNumberUI: GameManager or levelText not assigned.");
        }
    }
}
