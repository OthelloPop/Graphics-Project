using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    [Header("Tag to search for lights")]
    public string lightTag = "ToggleSpot";

    [Header("Optional")]
    public bool updateGI = true;

    private Light[] lightsToTurnOff;

    private void Start()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(lightTag);
        lightsToTurnOff = new Light[taggedObjects.Length];

        for (int i = 0; i < taggedObjects.Length; i++)
        {
            lightsToTurnOff[i] = taggedObjects[i].GetComponent<Light>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Light light in lightsToTurnOff)
            {
                if (light != null)
                    light.enabled = false;
            }

            if (updateGI)
                DynamicGI.UpdateEnvironment();
        }

        gameObject.SetActive(false);
        LightSwitchChallenge.Instance.StartChallenge();
    }
}