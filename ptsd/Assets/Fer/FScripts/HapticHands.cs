using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticHands : MonoBehaviour
{
    public XRBaseController leftController;
    public XRBaseController rightController;

    public float amplitude = 0.5f;
    public float duration = 0.2f;

    public void TriggerLeftHaptic()
    {
        leftController.SendHapticImpulse(amplitude, duration);
    }

    public void TriggerRightHaptic()
    {
        rightController.SendHapticImpulse(amplitude, duration);
    }

    public void TriggerBothHaptics()
    {
        TriggerLeftHaptic();
        TriggerRightHaptic();
    }

}
