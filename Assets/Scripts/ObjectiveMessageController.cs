using UnityEngine;
using TMPro;
using System.Collections;

public class ObjectiveMessageController : MonoBehaviour
{
    public static ObjectiveMessageController Instance;

    public GameObject messagePanel;
    public TextMeshProUGUI messageText;

    public float slideDuration = 0.5f;
    public float displayTime = 4f;

    private RectTransform rectTransform;
    private Vector2 offscreenPosition;
    private Vector2 onscreenPosition;

    void Awake()
    {
        Instance = this;

        if (messagePanel != null)
        {
            rectTransform = messagePanel.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                onscreenPosition = rectTransform.anchoredPosition;
                offscreenPosition = onscreenPosition + new Vector2(-Screen.width, 0);
                rectTransform.anchoredPosition = offscreenPosition;
            }
            messagePanel.SetActive(false);
        }
    }

    public void ShowObjective(string text)
    {
        if (rectTransform == null) return;

        messageText.text = text;
        messagePanel.SetActive(true);
        StartCoroutine(SlideInAndOut());
    }

    private IEnumerator SlideInAndOut()
    {
        float t = 0f;
        while (t < slideDuration)
        {
            t += Time.deltaTime;
            rectTransform.anchoredPosition = Vector2.Lerp(offscreenPosition, onscreenPosition, t / slideDuration);
            yield return null;
        }
        rectTransform.anchoredPosition = onscreenPosition;

        yield return new WaitForSeconds(displayTime);

        t = 0f;
        while (t < slideDuration)
        {
            t += Time.deltaTime;
            rectTransform.anchoredPosition = Vector2.Lerp(onscreenPosition, offscreenPosition, t / slideDuration);
            yield return null;
        }
        rectTransform.anchoredPosition = offscreenPosition;
        messagePanel.SetActive(false);
    }
}
