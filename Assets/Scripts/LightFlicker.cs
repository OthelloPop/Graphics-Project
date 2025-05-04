using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour
{
    public Light flickerLight;
    public AudioSource ambientBuzzAudio;

    public float minFlickerDelay = 2f;
    public float maxFlickerDelay = 6f;
    public float minFlickerDuration = 0.05f;
    public float maxFlickerDuration = 0.2f;
    public int minFlickers = 2;
    public int maxFlickers = 6;

    public float fadeDuration = 0.3f;
    private float originalVolume;

    void Start()
    {
        if (flickerLight == null)
            flickerLight = GetComponent<Light>();

        if (ambientBuzzAudio != null)
            originalVolume = ambientBuzzAudio.volume;

        StartCoroutine(FlickerRoutine());
    }

    IEnumerator FlickerRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minFlickerDelay, maxFlickerDelay));

            int flickers = Random.Range(minFlickers, maxFlickers);

            if (ambientBuzzAudio != null)
                StartCoroutine(FadeAudio(ambientBuzzAudio, originalVolume, 0f, fadeDuration));

            for (int i = 0; i < flickers; i++)
            {
                flickerLight.enabled = false;
                yield return new WaitForSeconds(Random.Range(minFlickerDuration, maxFlickerDuration));
                flickerLight.enabled = true;
                yield return new WaitForSeconds(Random.Range(minFlickerDuration, maxFlickerDuration));
            }

            if (ambientBuzzAudio != null)
                StartCoroutine(FadeAudio(ambientBuzzAudio, 0f, originalVolume, fadeDuration));
        }
    }

    IEnumerator FadeAudio(AudioSource audioSource, float from, float to, float duration)
    {
        float elapsed = 0f;
        audioSource.volume = from;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(from, to, elapsed / duration);
            yield return null;
        }

        audioSource.volume = to;
    }
}
