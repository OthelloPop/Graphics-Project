using UnityEngine;
using System.Collections;

public class CreatureSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] creatureSounds;

    public float minDelay = 5f;
    public float maxDelay = 15f;

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        StartCoroutine(PlayCreatureSounds());
    }

    IEnumerator PlayCreatureSounds()
    {
        while (true)
        {
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);

            if (creatureSounds.Length > 0)
            {
                AudioClip clip = creatureSounds[Random.Range(0, creatureSounds.Length)];
                audioSource.PlayOneShot(clip);
            }
            else if (audioSource.clip != null)
            {
                audioSource.Play();
            }
        }
    }
}
