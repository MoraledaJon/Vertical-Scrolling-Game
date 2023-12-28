using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCoreHaptics;

public class VibrationManager : MonoBehaviour
{
    public static VibrationManager Instance { get; private set; }

    private bool isVibrationEnabled;

    public float intensity = 1f; // 0 to 1
    public float sharpness = 1f; // 0 to 1
    public float duration = 2f; // in seconds


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            UnityCoreHapticsProxy.CreateEngine();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetVibrationEnabled(bool isEnabled)
    {
        if (isEnabled)
        {
            isVibrationEnabled = true ;
        }
        else
        {
            isVibrationEnabled = false; 
        }
    }


    public void StartVibration()
    {
        if(!isVibrationEnabled)
        {
            return;
        }

        // Check if iOS device supports core haptics
        if (UnityCoreHapticsProxy.SupportsCoreHaptics())
        {
            // Play transient (one-time) haptics
            //UnityCoreHapticsProxy.PlayTransientHaptics(intensity, sharpness);

            // Play continuous haptics
            UnityCoreHapticsProxy.PlayContinuousHaptics(intensity, sharpness, duration);

        }
        else
        {
            Handheld.Vibrate();
        }
    }


}
