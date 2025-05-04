using UnityEngine;

public class InteractableSwitch : MonoBehaviour
{
    public bool isActivated = false;

    public void Activate()
    {
        if (!LightSwitchChallenge.Instance.IsChallengeActive())
            return;

        if (isActivated)
        {
            SwitchUIFeedback.Instance.ShowMessage("Already used", Color.red);
            return;
        }

        isActivated = true;

        Debug.Log("Switch Activated!");
        SwitchUIFeedback.Instance.ShowMessage("Switch Activated!", Color.green);
    }
}
