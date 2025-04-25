using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LightSwitchChallenge : MonoBehaviour
{
    public static LightSwitchChallenge Instance;

    public float timeLimit = 60f;
    private float remainingTime;

    public bool updateGI = true;

    public TMP_Text timerText; // Assign in canvas

    private List<InteractableSwitch> allSwitches;
    private int switchesHit = 0;
    private bool challengeActive = false;

    void Awake() => Instance = this;

    public bool IsChallengeActive()
    {
        return challengeActive;
    }

    public void StartChallenge()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("LightSwitch");
        allSwitches = new List<InteractableSwitch>();

        foreach (GameObject obj in taggedObjects)
        {
            InteractableSwitch sw = obj.GetComponent<InteractableSwitch>();
            if (sw != null)
            {
                allSwitches.Add(sw);
            }
        }

        switchesHit = 0;
        remainingTime = timeLimit;
        challengeActive = true;
    }

    public void RegisterSwitchHit(InteractableSwitch sw)
    {
        if (!challengeActive || sw.isActivated == false) return;

        switchesHit++;
        if (switchesHit >= allSwitches.Count)
        {
            EndChallenge(true);
        }
    }

    void Update()
    {
        if (!challengeActive) return;

        remainingTime -= Time.deltaTime;
        timerText.text = $"TIME: {Mathf.Ceil(remainingTime)}";

        if (remainingTime <= 0f)
        {
            EndChallenge(false);
        }
    }

    void EndChallenge(bool success)
    {
        challengeActive = false;

        if (success)
        {
            Debug.Log("Challenge completed! Turning lights back on...");

            GameObject[] lightObjects = GameObject.FindGameObjectsWithTag("ToggleSpot");
            Debug.Log("ToggleSpot lights found: " + lightObjects.Length);

            foreach (GameObject obj in lightObjects)
            {
                Light light = obj.GetComponent<Light>();
                if (light != null)
                {
                    Debug.Log("Enabling light: " + obj.name);
                    light.enabled = true;
                }
                else
                {
                    Debug.LogWarning("No Light component found on: " + obj.name);
                }
            }

            if (updateGI)
                DynamicGI.UpdateEnvironment();
        }
        else
        {
            Debug.Log("Challenge failed... triggering anomaly or fail state");
        }
    }
}
