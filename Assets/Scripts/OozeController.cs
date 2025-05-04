using UnityEngine;

public class OozeController : MonoBehaviour
{
    public ParticleSystem oozeSystem;
    public float stopAfterSeconds = 20f;

    void Start()
    {
        if (oozeSystem != null)
        {
            oozeSystem.Play();
            Invoke(nameof(PauseOoze), stopAfterSeconds);
        }
    }

    void PauseOoze()
    {
        if (oozeSystem != null)
        {
            oozeSystem.Pause();
            Debug.Log("Ooze system paused");
        }
    }
}
