using UnityEngine;
using NodeCanvas.BehaviourTrees;
using NodeCanvas.Framework;
using Mirror;

public class InteractiveObj : NetworkBehaviour
{
    [SyncVar]
    public bool Seleccionado;
    [SyncVar]
    public bool Activado;
    [SerializeField]
    private GameObject selectObj, onObj, offObj;
    [Header("Animación Objeto afectado")]
    [SerializeField]
    private bool haveAnim;
    [SerializeField]
    private bool PlayAudio;
    [SerializeField]
    private GameObject obj;
    [SerializeField]
    public AudioClip onClip, offClip;
    [Header("Activación Objeto afectado")]
    [SerializeField]
    private bool haveActBehavior;
    [SerializeField]
    private GameObject ObjBehavior;

    private bool trig;//Para que solo se ejecute una vez por disparo

    // Start is called before the first frame update
    void Start()
    {
        if (!Activado)
        {
            selectObj.SetActive(false);
            offObj.SetActive(true);
            onObj.SetActive(false);
        }
        else
        {
            selectObj.SetActive(false);
            offObj.SetActive(false);
            onObj.SetActive(true);
        }
    }
   
    [ServerCallback]
    public void Actualiza()
    {
        if (!Activado)
        {
            Activado = true;
            offObj.SetActive(false);
            onObj.SetActive(true);
            selectObj.SetActive(false);
            if (PlayAudio)
            {
                GetComponent<AudioSource>().clip = onClip;
                GetComponent<AudioSource>().Play();
            }
            if (haveAnim)
                obj.GetComponent<Animator>().SetBool("Open", true);
            if (haveActBehavior)
                ObjBehavior.GetComponent<Blackboard>().SetVariableValue("On", true);

            RpcOn();
        }
        else
        {
            Activado = false;
            offObj.SetActive(true);
            onObj.SetActive(false);
            selectObj.SetActive(false);
            if (PlayAudio)
            {
                GetComponent<AudioSource>().clip = offClip;
                GetComponent<AudioSource>().Play();
            }
            if (haveAnim)
                obj.GetComponent<Animator>().SetBool("Open", false);
            if (haveActBehavior)
                ObjBehavior.GetComponent<Blackboard>().SetVariableValue("Off", true);

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
                //if (!puerta.GetComponent<Animation>().isPlaying)
                //{
                    trig = true;
                    if (!Activado)
                    {
                        Activado = true;
                        offObj.SetActive(false);
                        onObj.SetActive(true);
                        selectObj.SetActive(false);
                        if (PlayAudio)
                        {
                            GetComponent<AudioSource>().clip = onClip;
                            GetComponent<AudioSource>().Play();
                        }
                        if (haveAnim)
                            obj.GetComponent<Animator>().SetBool("Open", true);
                        if (haveActBehavior)
                            ObjBehavior.GetComponent<Blackboard>().SetVariableValue("On", true);

                        RpcOn();
                    }
                    else
                    {
                        Activado = false;
                        offObj.SetActive(true);
                        onObj.SetActive(false);
                        selectObj.SetActive(false);
                        if (PlayAudio)
                        {
                            GetComponent<AudioSource>().clip = offClip;
                            GetComponent<AudioSource>().Play();
                        }
                        if (haveAnim)
                            obj.GetComponent<Animator>().SetBool("Open", false);
                        if (haveActBehavior)
                            ObjBehavior.GetComponent<Blackboard>().SetVariableValue("Off", true);

                        RpcOff();
                    }
                //}
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
        if (PlayAudio)
        {
            GetComponent<AudioSource>().clip = onClip;
            GetComponent<AudioSource>().Play();
        }
        //puerta.GetComponent<Animator>().SetBool("Open", true); //YA QUE ES ANIMATOR Y TIENE UN NETWORKANIMATOR
    }
    [ClientRpc]
    void RpcOff()
    {
        //Activado = false;
        selectObj.SetActive(false);
        offObj.SetActive(true);
        onObj.SetActive(false);
        if (PlayAudio)
        {
            GetComponent<AudioSource>().clip = offClip;
            GetComponent<AudioSource>().Play();
        }
        //puerta.GetComponent<Animator>().SetBool("Open", false); //YA QUE ES ANIMATOR Y TIENE UN NETWORKANIMATOR
    }

}
