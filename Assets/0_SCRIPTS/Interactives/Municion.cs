using UnityEngine;
using Mirror;

public class Municion : NetworkBehaviour
{
    [SerializeField]
    private int Num = 100;
    [SerializeField]
    private float destroyAfter = 5;
    [SyncVar]
    public bool Seleccionado;
    [SerializeField]
    private GameObject normalObj, selectObj;

    private bool trig;//Para que solo se ejecute una vez por disparo

    public override void OnStartServer()
    {
        Invoke(nameof(DestroySelf), destroyAfter);
    }

    // Start is called before the first frame update
    void Start()
    {
    }
    
    [ServerCallback]
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Interactive")
        {
            //seleccionar
            Seleccionado = true;
            normalObj.SetActive(false);
            selectObj.SetActive(true);
            RpcSelect();
            //activar si un player presiona el disparo
            if (other.GetComponentInParent<HitInteractives>().activar && !trig)
            {
                trig = true;
                //obj.GetComponent<AudioSource>().clip = actClip;
                //obj.GetComponent<AudioSource>().Play();
                //obj.GetComponent<Animator>().SetBool("Open", true);
                //other.GetComponentInParent<Player>().Municion += Num; //INCREMENTA MUNICION
                //RpcAct();
                NetworkServer.Destroy(gameObject); //DESTRUYE
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
            //seleccionar
            Seleccionado = false;
            selectObj.SetActive(false);
            normalObj.SetActive(true);
            RpcNoSelect();
        }
    }

    [ClientRpc]
    void RpcSelect()
    {
        Seleccionado = true;
        normalObj.SetActive(false);
        selectObj.SetActive(true);
    }
    [ClientRpc]
    void RpcNoSelect()
    {
        Seleccionado = false;
        selectObj.SetActive(false);
        normalObj.SetActive(true);
    }

    // destroy for everyone on the server
    [Server]
    void DestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }
}
