using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class HoverGlow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text text;
    public Color normalColor = Color.gray;
    public Color hoverColor = Color.white;

    public AudioClip hoverSound;
    private AudioSource audioSource;

    void Start()
    {
        if (text == null)
            text = GetComponent<TMP_Text>();

        text.color = normalColor;

        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = hoverColor;

        if (hoverSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hoverSound, 0.2f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = normalColor;
    }
}
