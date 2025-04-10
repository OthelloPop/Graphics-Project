using UnityEngine;

public class TriggerHelpMe : MonoBehaviour
{
    public TypeWriterEffect helpMeText;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered trigger: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("It's the player!");
            helpMeText.StartTyping();
        }
    }
}
