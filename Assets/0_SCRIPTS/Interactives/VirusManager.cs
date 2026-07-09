using UnityEngine;
using UnityEngine.UI;
using NodeCanvas.BehaviourTrees;
using Mirror;

public class VirusManager : NetworkBehaviour
{
    [Header("ORDENADOR")]
    [SyncVar]
    public float VirusTime;
    [SyncVar]
    public bool VirusKilled;
    [SerializeField]
    private Image imagen;
    [SerializeField]
    private float TimeIniRef;
    [SyncVar]
    public bool falloT1, falloT2, falloT3, falloT4, sinFallos;
    [SyncVar]
    public bool fT1, fT2, fT3, fT4;
    [Header("TERMINALES")]
    [SerializeField]
    private GameObject T1;
    [SerializeField]
    private GameObject T2, T3, T4, TOK, TERROR;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    [ServerCallback]
    void Update()
    {   /*
        //ORDENADOR
        if (sinFallos)
            VirusTime -= Time.deltaTime * 2;
        else
            VirusTime -= Time.deltaTime; //DE ESTA DORMA SABEMOS QUE EL TIEMPO MAXIMO SERA TimeIniRef
        */
        //ORDENADOR
        if (sinFallos)
            VirusTime -= Time.deltaTime;
        float porcentaje = 1 - (VirusTime / TimeIniRef);
        imagen.fillAmount = porcentaje;
        RpcFillAmount(porcentaje);

        if (VirusTime <= 0)
        {
            VirusTime = 0;
            GetComponent<BehaviourTreeOwner>().enabled = false;
            VirusKilled = true;
        }

        //ACTUALIZA EL ORDENADOR CENTRAL A OK O ERROR
        if (!falloT1 && !falloT2 && !falloT3 && !falloT4)
        {
            if (!sinFallos)
            {
                sinFallos = true;
                TOK.SetActive(true);
                TERROR.SetActive(false);
                RpcTerminalOK();
            }
        }
        else 
        {
            if (sinFallos)
            {
                sinFallos = false;
                TOK.SetActive(false);
                TERROR.SetActive(true);
                RpcTerminalERROR();
            }
        }
    }

    [Server]
    public void fallo(int term)
    {
        //TERMINALES
        if (term == 1)
            falloT1 = true;
        
        if (term == 2)
            falloT2 = true;
        
        if (term == 3)
            falloT3 = true;
        
        if (term == 4)
            falloT4 = true;
        //
        if (falloT1)
        {
            T1.GetComponent<InteractiveVirusTerminal>().Activado = false; //fallo Terminal
            T1.GetComponent<InteractiveVirusTerminal>().Actualiza();
            
        }
        else
        {
            T1.GetComponent<InteractiveVirusTerminal>().Activado = true; //ok Terminal
            T1.GetComponent<InteractiveVirusTerminal>().Actualiza();
        }
        if (falloT2)
        {
            T2.GetComponent<InteractiveVirusTerminal>().Activado = false; //fallo Terminal
            T2.GetComponent<InteractiveVirusTerminal>().Actualiza();
        }
        else
        {
            T2.GetComponent<InteractiveVirusTerminal>().Activado = true; //ok Terminal
            T2.GetComponent<InteractiveVirusTerminal>().Actualiza();
        }
        if (falloT3)
        {
            T3.GetComponent<InteractiveVirusTerminal>().Activado = false; //fallo Terminal
            T3.GetComponent<InteractiveVirusTerminal>().Actualiza();
        }
        else
        {
            T3.GetComponent<InteractiveVirusTerminal>().Activado = true; //ok Terminal
            T3.GetComponent<InteractiveVirusTerminal>().Actualiza();
        }
        if (falloT4)
        {
            T4.GetComponent<InteractiveVirusTerminal>().Activado = false; //fallo Terminal
            T4.GetComponent<InteractiveVirusTerminal>().Actualiza();
        }
        else
        {
            T4.GetComponent<InteractiveVirusTerminal>().Activado = true; //ok Terminal
            T4.GetComponent<InteractiveVirusTerminal>().Actualiza();
        }
    }

    [ClientRpc]
    void RpcTerminalOK()
    {
        sinFallos = true;
        TOK.SetActive(true);
        TERROR.SetActive(false);
    }
    [ClientRpc]
    void RpcTerminalERROR()
    {
        sinFallos = false;
        TOK.SetActive(false);
        TERROR.SetActive(true);
    }

    [ClientRpc]
    void RpcFillAmount(float porcent)
    {
        imagen.fillAmount = porcent;
    }
}
