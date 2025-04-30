using UnityEngine;

public class LoopManager : MonoBehaviour
{
    public Transform player;
    public float resetZ = -40f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 pos = player.position;
            player.position = new Vector3(pos.x, pos.y, pos.z + resetZ);
            Debug.Log("Loop triggered. Player repositioned.");
        }
    }
}
