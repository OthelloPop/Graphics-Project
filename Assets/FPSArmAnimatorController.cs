using UnityEngine;
using UnityEngine.InputSystem;

public class FPSArmAnimatorController : MonoBehaviour
{
    public Animator armsAnimator;
    private CharacterController controller;
    private PlayerInput playerInput;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        bool isWalking = controller.velocity.magnitude > 0.1f && controller.isGrounded;
        bool isJumping = !controller.isGrounded;

        armsAnimator.SetBool("isWalking", isWalking);
        armsAnimator.SetBool("isJumping", isJumping);
    }
}
