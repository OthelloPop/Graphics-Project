using UnityEngine;
using StarterAssets;

[RequireComponent(typeof(CharacterController))]
public class LoopingFootstepController : MonoBehaviour
{
    [Header("References")]
    public AudioSource footstepAudio; // The looping AudioSource
    public StarterAssetsInputs input; // Input component from Starter Assets

    [Header("Audio Settings")]
    [Range(0f, 1f)] public float footstepVolume = 0.1f;
    public float walkPitch = 1.0f;
    public float runPitch = 1.5f;
    public float movementThreshold = 0.1f;

    private CharacterController controller;

    void Start()
    {
        Debug.Log("LoopingFootstepController started"); // Add this
        controller = GetComponent<CharacterController>();
        if (footstepAudio != null)
        {
            footstepAudio.loop = true;
            footstepAudio.playOnAwake = false;
        }
    }

    void Update()
    {
        if (controller == null || footstepAudio == null || input == null)
            return;

        bool isGrounded = controller.isGrounded;
        bool isMoving = controller.velocity.magnitude > movementThreshold;
        bool isRunning = input.sprint;

        // Apply volume and pitch
        footstepAudio.volume = footstepVolume;
        footstepAudio.pitch = isRunning ? runPitch : walkPitch;

        // Play or stop loop
        if (isGrounded && isMoving)
        {
            if (!footstepAudio.isPlaying)
                footstepAudio.Play();
        }
        else
        {
            if (footstepAudio.isPlaying)
                footstepAudio.Stop();
        }

        Debug.Log("Velocity: " + controller.velocity.magnitude);

        if (Input.GetKeyDown(KeyCode.F))
            footstepAudio.Play();
    }
}
