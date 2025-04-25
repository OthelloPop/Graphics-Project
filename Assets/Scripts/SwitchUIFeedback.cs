using UnityEngine;
using TMPro;

public class SwitchUIFeedback : MonoBehaviour
{
    public static SwitchUIFeedback Instance;

    public TextMeshProUGUI messageText;
    public float displayTime = 2f;

    private float timer;
    private bool isShowing = false;

    void Awake()
    {
        Instance = this;
        messageText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isShowing)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                messageText.gameObject.SetActive(false);
                isShowing = false;
            }
        }
    }

    public void ShowMessage(string message, Color color)
    {
        messageText.text = message;
        messageText.color = color;
        messageText.gameObject.SetActive(true);

        timer = displayTime;
        isShowing = true;
    }
}

