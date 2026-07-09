using UnityEngine;
using Mirror;
using InputTracking = UnityEngine.XR.InputTracking;
using Node = UnityEngine.XR.XRNode;

public class DesPlayerCompNET : NetworkBehaviour
{
    //NOTA: PONER CONTROL public override void OnStartServer() {}

    public GameObject OVR;
    public Camera CamaraL, CamaraR;
    public Transform LHand, RHand, CamaraC;

    void Start()
    {
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            if (OVR != null)
                Destroy(OVR);
        }
        else
        {
            /* //PRUEBAS 14/7
            if (CamaraL.tag != "MainCamera")
            {
                CamaraL.tag = "MainCamera";
                CamaraL.enabled = true;
            }
            if (CamaraR.tag != "MainCamera")
            {
                CamaraR.tag = "MainCamera";
                CamaraR.enabled = true;
            }


            LHand.localRotation = InputTracking.GetLocalRotation(Node.LeftHand);
            RHand.localRotation = InputTracking.GetLocalRotation(Node.RightHand);
            LHand.localPosition = OVR.transform.localPosition + InputTracking.GetLocalPosition(Node.LeftHand);
            RHand.localPosition = OVR.transform.localPosition + InputTracking.GetLocalPosition(Node.RightHand);

            CamaraC.localRotation = InputTracking.GetLocalRotation(Node.CenterEye);
            CamaraC.localPosition = OVR.transform.localPosition + InputTracking.GetLocalPosition(Node.CenterEye);
            */ //PRUEBAS 14/7

            /*
            inputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.CenterEye);
            if (device.isValid)
            {
                if (device.TryGetFeatureValue(CommonUsages.centerEyeRotation, out rotation))
                    return true;
            }
            // This is the fail case, where there was no center eye was available.
            rotation = Quaternion.identity;
            return false;
            */
        }

        //GetComponent<Disparo>().enabled = false; //DUDA

        //CamaraC.GetComponent<Camera>().enabled = false;
        //CamaraC.GetComponent<AudioListener>().enabled = false;
        //CamaraC.GetComponent<OVRScreenFade>().enabled = false;
        //OVR.GetComponent<OVRCameraRig>().enabled = false;

        //CmdSwitch();//DUDA
    }
    /*
    [Command]
    public void CmdSwitch()
    {
        //GetComponent<Disparo>().enabled = false;
        Camara.GetComponent<Camera>().enabled = false;
        Camara.GetComponent<AudioListener>().enabled = false;
        ManiI.GetComponent<SteamVR_Behaviour_Pose>().enabled = false;
        ManoD.GetComponent<SteamVR_Behaviour_Pose>().enabled = false;

        RpcSwitch();
    }

    [ClientRpc]
    void RpcSwitch()
    {
        //GetComponent<Disparo>().enabled = false;
        Camara.GetComponent<Camera>().enabled = false;
        Camara.GetComponent<AudioListener>().enabled = false;
        ManiI.GetComponent<SteamVR_Behaviour_Pose>().enabled = false;
        ManoD.GetComponent<SteamVR_Behaviour_Pose>().enabled = false;
    }
    */
}
