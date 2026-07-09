using System.Collections.Generic;
using UnityEngine;

/// <inheritdoc />
/// <summary>
/// Cancels out Oculus Go's default camera recentering behaviour when HMD is put back on.
/// Attach this script to the parent object of OVRCameraRig and
/// drag n drop the center camera to the "Camera" field in the inspector.
/// The cancellation will happen one frame delayed to the recentering, so
/// you should additionally implement some fading transition (noted in the code below).
/// See original discussion and code by StudioRX: 
/// https://forums.oculusvr.com/developer/discussion/64728/oculus-go-disable-forced-recenter-after-putting-headset-back-on
/// </summary>
public class OculusGoHmdRecenteringCanceller : MonoBehaviour
{
    [SerializeField]
    Transform _camera;

    Queue<float> _pastAngles;

    void Start()
    {
        _pastAngles = new Queue<float>();
        OVRManager.HMDMounted += () => OnHmdMounted();
        OVRManager.HMDUnmounted += () => OnHmdUnmounted();
    }

    void Update()
    {
        if (OVRPlugin.userPresent) return; // Pass if the HMD is on.

        // We will recover this angle when the HMD gets put back on.
        float currentAngle = _camera.eulerAngles.y;

        // Cache the current angle to recover it later.
        // It's safer to look one or two frames back because
        // we might happen to cache an angle AFTER the recentering has happened.
        _pastAngles.Enqueue(currentAngle);
        while (_pastAngles.Count > 2)
        {
            _pastAngles.Dequeue();
        }
    }

    void OnHmdMounted()
    {
        if (_pastAngles.Count == 0) return; // Pass if nothing is cached.

        // Do recover using the cache.
        float recoveryAngle = _pastAngles.Dequeue();
        transform.eulerAngles = new Vector3(0f, recoveryAngle, 0f);

        _pastAngles.Clear();

        // TODO "Fade in" here
    }

    void OnHmdUnmounted()
    {
        // TODO "Fade out" here
    }
}