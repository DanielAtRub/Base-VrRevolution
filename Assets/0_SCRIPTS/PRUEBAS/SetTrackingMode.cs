using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SetTrackingMode : MonoBehaviour
{
    public TrackingOriginModeFlags trackingFlags = TrackingOriginModeFlags.Floor;
    public bool recenterOnSet = true;

    private void Update()
    {
        SetTrackingModeTo(); // Change tracking mode on whatever is already initialized.
        //SubsystemManager.reloadSubsytemsCompleted += SetTrackingModeTo; // Add listener for future inits.
    }

    private void OnEnable()
    {
        SetTrackingModeTo(); // Change tracking mode on whatever is already initialized.
        SubsystemManager.afterReloadSubsystems += SetTrackingModeTo; // Add listener for future inits.
    }

    private void OnDisable()
    {
        SubsystemManager.afterReloadSubsystems -= SetTrackingModeTo;
    }

    /// <summary>
    /// Sets the tracking mode of all currently initialized XR Subsystems to the origin configured in the inspector.
    /// </summary>
    public void SetTrackingModeTo()
    {
        SetTrackingModeTo(trackingFlags, recenterOnSet);
    }

    /// <summary>
    /// Sets the tracking mode of all currently initialized XR Subsystems to the origin specified. 
    /// </summary>
    public void SetTrackingModeTo(TrackingOriginModeFlags flags, bool recenter)
    {
        List<XRInputSubsystem> subsystems = new List<XRInputSubsystem>();
        SubsystemManager.GetSubsystems(subsystems);

        foreach (var subsystem in subsystems)
        {
            if (subsystem.TrySetTrackingOriginMode(flags))
            {
                if (recenter)
                {
                    // Now that we've changed the origin mode we'll have to recalibrate to this new point
                    // in space (it will re-zero off of the last-set-height, which for Oculus Quest is the
                    // HMD boot position).
                    subsystem.TryRecenter();
                }
            }
            else
            {
                Debug.LogError("Failed to set the TrackingOriginMode of id:" + subsystem.SubsystemDescriptor.id, gameObject);
            }
        }
    }
}