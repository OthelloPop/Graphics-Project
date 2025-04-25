using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactRange = 3f;
    public KeyCode interactKey = KeyCode.E;

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, interactRange))
            {
                Debug.Log("Raycast hit: " + hit.collider.name);

                if (hit.collider.CompareTag("LightSwitch"))
                {
                    InteractableSwitch sw = hit.collider.GetComponentInParent<InteractableSwitch>();
                    if (sw != null)
                    {
                        Debug.Log("Switch found — calling Activate()");
                        sw.Activate();

                        LightSwitchChallenge.Instance.RegisterSwitchHit(sw);
                    }
                    else
                    {
                        Debug.LogWarning("Hit LightSwitch tag, but no InteractableSwitch component found.");
                    }
                }
                else
                {
                    Debug.Log("Hit object is not tagged LightSwitch");
                }
            }
            else
            {
                Debug.Log("Raycast hit nothing");
            }
        }
    }
}

