using TMPro;
using UnityEngine;
using System.Collections;

public class TypeWriterEffect : MonoBehaviour
{
    public float delay = 0.1f;
    private TextMeshProUGUI tmp;
    private string fullText;
    private bool triggered = false;

    void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        fullText = tmp.text;
        tmp.text = "";
    }

    public void StartTyping()
    {
        if (!triggered)
        {
            triggered = true;
            StartCoroutine(ShowText());
        }
    }

    IEnumerator ShowText()
    {
        foreach (char c in fullText)
        {
            tmp.text += c;
            yield return new WaitForSeconds(delay);
        }
    }
}
