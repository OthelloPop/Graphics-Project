using UnityEngine;
 
public class TeacherCharacterMove : MonoBehaviour

{

    public float moveSpeed = 3f; // Speed of movement

    public float distance = 1f; // Distance to move forward (1 meter)
 
    private Vector3 startPosition; // Starting position

    private Vector3 targetPosition; // Target position (1 meter forward)

    private bool movingForward = true; // Tracks direction

    private Animator animator; // Optional for animations
 
    void Start()

    {

        // Store the starting position

        startPosition = transform.position;

        // Calculate initial target (1 meter forward)

        targetPosition = startPosition + transform.forward * distance;

        animator = GetComponent<Animator>(); // Get Animator if present

    }
 
    void Update()

    {

        // Move towards the target position

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
 
        // Update animation

        bool isWalking = Vector3.Distance(transform.position, targetPosition) > 0.01f;

        if (animator) animator.SetBool("isWalking", isWalking);
 
        // Check if reached the target

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)

        {

            // Switch direction

            movingForward = !movingForward;

            // Set new target (forward or back to start)

            targetPosition = movingForward ? startPosition + transform.forward * distance : startPosition;

        }

    }

}
 