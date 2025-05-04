using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LightSwitchChallenge : MonoBehaviour
{
    public static LightSwitchChallenge Instance;

    public AudioSource ambientAudioSource;
    public AudioSource powerDownAudioSource;
    public AudioSource tickingAudioSource;
    public AudioSource flashlightClickAudioSource;

    public Light flashlight;

    public float timeLimit = 60f;
    private float remainingTime;

    public bool updateGI = true;

    public TMP_Text timerText;

    private List<InteractableSwitch> allSwitches;
    private int switchesHit = 0;
    public bool challengeActive = false;

    void Awake() => Instance = this;

    public bool IsChallengeActive()
    {
        return challengeActive;
    }

    public void StartChallenge()
    {
        if (ambientAudioSource != null && ambientAudioSource.isPlaying)
            ambientAudioSource.Pause();

        if (powerDownAudioSource != null)
            powerDownAudioSource.Play();

        GameObject[] lightObjects = GameObject.FindGameObjectsWithTag("ToggleSpot");
        foreach (GameObject obj in lightObjects)
        {
            Light light = obj.GetComponentInChildren<Light>();
            if (light != null)
                light.enabled = false;
        }

        if (updateGI)
            DynamicGI.UpdateEnvironment();

        StartCoroutine(DelayedChallengeStart());
    }

    private IEnumerator DelayedChallengeStart()
    {
        yield return new WaitForSeconds(2f);

        ObjectiveMessageController.Instance.ShowObjective("Find and activate all the light switches before time runs out!");

        if (flashlight != null)
        {
            flashlight.enabled = true;
            if (flashlightClickAudioSource != null)
                flashlightClickAudioSource.Play();
        }

        yield return new WaitForSeconds(2f);

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

        if (timerText != null)
            timerText.gameObject.SetActive(true);

        if (tickingAudioSource != null)
            tickingAudioSource.Play();

        Debug.Log("Challenge fully started — ticking and timer active!");
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
            ambientAudioSource.UnPause();
            tickingAudioSource.Stop();
            if (flashlight != null && flashlight.enabled)
            {
                flashlight.enabled = false;

                if (flashlightClickAudioSource != null)
                    flashlightClickAudioSource.Play();
            }

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
            Debug.Log("Challenge failed... restarting level.");
            GameManager.Instance.RestartGame();
        }
    }
}
