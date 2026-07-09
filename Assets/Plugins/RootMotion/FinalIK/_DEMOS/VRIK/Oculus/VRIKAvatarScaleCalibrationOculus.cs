using RootMotion.FinalIK;
using Mirror;
using UnityEngine;

using UnityEngine.InputSystem; //PICO

// Simple avatar scale calibration.
public class VRIKAvatarScaleCalibrationOculus : NetworkBehaviour
{
    public VRIK ik;
    //public float scaleMlp = 1f;

    [SerializeField]
    private AudioSource CalAltura;

    [Header("PICO")]
    [SerializeField]
    private InputActionReference rightUIButton; //PICO

    void Start()
    {
    }

    private void LateUpdate()
    {
        if (!isLocalPlayer)
            return;

        //if (OVRInput.GetDown(OVRInput.Button.Two))
        if (rightUIButton.action.triggered) //PICO
        {
            // Compare the height of the head target to the height of the head bone, multiply scale by that value.
            float sizeF = (ik.solver.spine.headTarget.position.y - ik.references.root.position.y) / (ik.references.head.position.y - ik.references.root.position.y);
            //ik.references.root.localScale *= sizeF * scaleMlp;
            ik.references.root.localScale *= sizeF;
            CalAltura.Play();
        }
    }
}
