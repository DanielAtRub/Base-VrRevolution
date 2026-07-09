using UnityEngine;
using NodeCanvas.BehaviourTrees;
using Mirror;

public class InteractiveVirusTerminal : NetworkBehaviour
{
    [SyncVar]
    public bool Seleccionado;
    [SyncVar]
    public bool Activado;
    [SerializeField]
    private GameObject selectObj, onObj, offObj;
    [SerializeField]
    private bool PlayAudio;
    [SerializeField]
    public AudioClip onClip, offClip;

    [SerializeField]
    private GameObject virusManager;
    [SerializeField]
    private int soyTerminal;

    [SerializeField]
    private GameObject tError, tOk;

    private bool trig;//Para que solo se ejecute una vez por disparo

    // Start is called before the first frame update
    void Start()
    {
        if (!Activado) //OK
        {
            selectObj.SetActive(false);
            offObj.SetActive(true); //OK
            onObj.SetActive(false); //ERROR
        }
        else //ERROR
        {
            selectObj.SetActive(false);
            offObj.SetActive(false);
            onObj.SetActive(true);
        }
    }
    
    [Server]
    public void Actualiza()
    {
        if (!Activado)
        {
            Activado = true;
            selectObj.SetActive(false);
            offObj.SetActive(false);
            onObj.SetActive(true);
            //ESTADO TERMINALES EN ORDENADOR CENTRAL
            tOk.SetActive(false);
            tError.SetActive(true);
            if (PlayAudio)
            {
                GetComponent<AudioSource>().clip = onClip;
                GetComponent<AudioSource>().Play();
            }

            RpcOn();
        }
        else
        {
            Activado = false;
            selectObj.SetActive(false);
            offObj.SetActive(true);
            onObj.SetActive(false);
            //ESTADO TERMINALES EN ORDENADOR CENTRAL
            tOk.SetActive(true);
            tError.SetActive(false);
            if (PlayAudio)
            {
                GetComponent<AudioSource>().clip = offClip;
                GetComponent<AudioSource>().Play();
            }

            RpcOff();
        }
    }

    [ServerCallback]
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Interactive")
        {
            //seleccionar
            Seleccionado = true;
            offObj.SetActive(false);
            onObj.SetActive(false);
            selectObj.SetActive(true);
            RpcSelect();
            //activar si un player presiona el disparo
            if (other.GetComponentInParent<HitInteractives>().activar && !trig)
            {
                trig = true;
                if (Activado)
                {
                    Activado = false;
                    offObj.SetActive(true);
                    onObj.SetActive(false);
                    selectObj.SetActive(false);
                    //ESTADO TERMINALES EN ORDENADOR CENTRAL
                    tOk.SetActive(true);
                    tError.SetActive(false);
                    if (PlayAudio)
                    {
                        GetComponent<AudioSource>().clip = offClip;
                        GetComponent<AudioSource>().Play();
                    }

                    tOk.SetActive(true);
                    tError.SetActive(false);
                    if (soyTerminal == 1)
                    {
                        virusManager.GetComponent<VirusManager>().falloT1 = false; //QUITA EL FALLO
                    }
                    if (soyTerminal == 2)
                    {
                        virusManager.GetComponent<VirusManager>().falloT2 = false; //QUITA EL FALLO
                    }
                    if (soyTerminal == 3)
                    {
                        virusManager.GetComponent<VirusManager>().falloT3 = false; //QUITA EL FALLO
                    }
                    if (soyTerminal == 4)
                    {
                        virusManager.GetComponent<VirusManager>().falloT4 = false; //QUITA EL FALLO
                    }

                    RpcOff();
                }
            }
            if (!other.GetComponentInParent<HitInteractives>().activar)
            {
                trig = false;
            }
        }
    }
    [ServerCallback]
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactive")
        {
            //deseleccionar
            Seleccionado = false;
            selectObj.SetActive(false);
            if (!Activado)
            {
                onObj.SetActive(false);
                offObj.SetActive(true);
            }
            else
            {
                onObj.SetActive(true);
                offObj.SetActive(false);
            }
            RpcNoSelect();
        }
    }
    
    [ClientRpc]
    void RpcSelect()
    {
        //Seleccionado = true;
        offObj.SetActive(false);
        onObj.SetActive(false);
        selectObj.SetActive(true);
    }
    [ClientRpc]
    void RpcNoSelect()
    {
        //Seleccionado = false;
        selectObj.SetActive(false);
        if (!Activado)
        {
            onObj.SetActive(false);
            offObj.SetActive(true);
        }
        else
        {
            onObj.SetActive(true);
            offObj.SetActive(false);
        }
    }
    
    [ClientRpc]
    void RpcOn()
    {
        //Activado = true;
        selectObj.SetActive(false);
        offObj.SetActive(false);
        onObj.SetActive(true);
        //ESTADO TERMINALES EN ORDENADOR CENTRAL
        tOk.SetActive(false);
        tError.SetActive(true);
        if (PlayAudio)
        {
            GetComponent<AudioSource>().clip = onClip;
            GetComponent<AudioSource>().Play();
        }
    }
    [ClientRpc]
    void RpcOff()
    {
        //Activado = false;
        selectObj.SetActive(false);
        offObj.SetActive(true);
        onObj.SetActive(false);
        //ESTADO TERMINALES EN ORDENADOR CENTRAL
        tOk.SetActive(true);
        tError.SetActive(false);
        if (PlayAudio)
        {
            GetComponent<AudioSource>().clip = offClip;
            GetComponent<AudioSource>().Play();
        }
        /*
        if (soyTerminal == 1)
        {
            virusManager.GetComponent<VirusManager>().falloT1 = false; //QUITA EL FALLO
        }
        if (soyTerminal == 2)
        {
            virusManager.GetComponent<VirusManager>().falloT2 = false; //QUITA EL FALLO
        }
        if (soyTerminal == 3)
        {
            virusManager.GetComponent<VirusManager>().falloT3 = false; //QUITA EL FALLO
        }
        if (soyTerminal == 4)
        {
            virusManager.GetComponent<VirusManager>().falloT4 = false; //QUITA EL FALLO
        }
        */
    }

}
