using UnityEngine;
using Mirror;

public class HitInteractives : NetworkBehaviour
{
    [Header("Dispara y Activa")]
    //public AudioSource SelectAudio;
    public bool activar;

    void Start()
    {
        //SelectAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        // for local player
        if (!isLocalPlayer) return;

        // shoot Select
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            //SelectAudio.Play();
            activar = true;
            CmdActiva(activar);
        }
        if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))
        {
            activar = false;
            CmdActiva(activar);
        }
    }
    
    [Command]
    void CmdActiva(bool act)
    {
        activar = act;
    }   
}
