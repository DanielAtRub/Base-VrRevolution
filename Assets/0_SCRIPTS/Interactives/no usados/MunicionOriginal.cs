using UnityEngine;
using TMPro;
using Mirror;

public class MunicionOriginal : NetworkBehaviour
{
    public int Num = 100;
    [SerializeField]
    private TextMeshPro textMunicion;

    [SyncVar]
    public bool Seleccionado;
    [SyncVar]
    public bool Activado;
    [SerializeField]
    private GameObject selectObj;
    [SerializeField]
    private GameObject normalObj;
    //[SerializeField]
    //private GameObject activateObj;
    //[Header("Objeto afectado")]
    //[SerializeField]
    //private GameObject obj;
    //[SerializeField]
    //public AudioClip actClip, desClip;

    private bool trig;//Para que solo se ejecute una vez por disparo

    // Start is called before the first frame update
    void Start()
    {
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        textMunicion.text = Num.ToString();
    }

    // Update is called once per frame
    void Update()
    {
    }

    [ServerCallback]
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Interactive")
        {
            //seleccionar
            Seleccionado = true;
            selectObj.SetActive(true);
            RpcSelect();
            //activar si un player presiona el disparo
            if (other.GetComponentInParent<HitInteractives>().activar && !trig)
            {
                trig = true;
                if (!Activado)
                {
                    Activado = true;
                    normalObj.SetActive(false);
                    //activateObj.SetActive(true);
                    //obj.GetComponent<AudioSource>().clip = actClip;
                    //obj.GetComponent<AudioSource>().Play();
                    //obj.GetComponent<Animator>().SetBool("Open", true);

                    //other.GetComponentInParent<Player>().Municion += Num; //INCREMENTA MUNICION
                    RpcAct();

                    NetworkServer.Destroy(gameObject); //DESTRUYE
                }
                else
                {
                    Activado = false;
                    normalObj.SetActive(true);
                    //activateObj.SetActive(false);
                    //obj.GetComponent<AudioSource>().clip = desClip;
                    //obj.GetComponent<AudioSource>().Play();
                    //obj.GetComponent<Animator>().SetBool("Open", false);
                    RpcDes();
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
            //seleccionar
            Seleccionado = false;
            selectObj.SetActive(false);
            RpcNoSelect();
        }
    }

    [ClientRpc]
    void RpcSelect()
    {
        Seleccionado = true;
        selectObj.SetActive(true);
    }
    [ClientRpc]
    void RpcNoSelect()
    {
        Seleccionado = false;
        selectObj.SetActive(false);
    }

    [ClientRpc]
    void RpcAct()
    {
        Activado = true;
        normalObj.SetActive(false);
        //activateObj.SetActive(true);
        //obj.GetComponent<AudioSource>().clip = actClip;
        //obj.GetComponent<AudioSource>().Play();
    }
    [ClientRpc]
    void RpcDes()
    {
        Activado = false;
        normalObj.SetActive(true);
        //activateObj.SetActive(false);
        //obj.GetComponent<AudioSource>().clip = desClip;
        //obj.GetComponent<AudioSource>().Play();
    }
    
}
