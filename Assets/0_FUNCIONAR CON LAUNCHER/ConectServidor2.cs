using UnityEngine;
using Mirror;
using TMPro;

public class ConectServidor2 : MonoBehaviour
{
    [SerializeField]
    private GameObject ManagerHud;
    //LICENCIA
    [SerializeField]
    private GameObject LICENCIA, textLoading;
    //CODIGO OFFLINE
    [SerializeField]
    private GameObject CodeOff;
    [SerializeField]
    private TMP_InputField InputCode;

    //SEGURIDAD
    [SerializeField]
    private int SecurityNlimit = 50;
    [SerializeField]
    private int SecurityN;
    [SerializeField]
    private int LicenciaInicial;
    private bool trig;
    [SerializeField]
    private TMP_Text TextSecurity;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ComienzaServidor", 0, 2); //ARRANCA DIRECTO SIN PULSAR EN A PANTALLA
    }

    public void ComienzaServidor()
    {
        if (!ManagerHud)
            ManagerHud = GameObject.Find("NetworkManager");
        else
        {

            comienzaServer(); //VRARENA

            /*
            //SEGURIDAD
            LicenciaInicial = PlayerPrefs.GetInt("LicenciaInicial");
            if (LICENCIA.GetComponent<licencia>().LicenciaOk) // SI HAY LICENCIA Y INTERNET ACTIVA LICENCIAINICIAL = 1 UNA VEZ
            {
                if (!PlayerPrefs.HasKey("LicenciaInicial"))
                    PlayerPrefs.SetInt("LicenciaInicial", 1);
                LicenciaInicial = PlayerPrefs.GetInt("LicenciaInicial");
            }
            if (LicenciaInicial == 1)
            {
                if (!LICENCIA.GetComponent<licencia>().LicenciaOk) //SI NO HAY LICENCIA O CONEXION
                {
                    if (!trig)
                    {
                        trig = true;
                        SecurityN = PlayerPrefs.GetInt("SecurityN");
                        SecurityN++;
                        PlayerPrefs.SetInt("SecurityN", SecurityN);
                    }
                }
                else //SI SI HAY LICENCIA Y CONEXION, PONE A 1 PARA COMPROBAR QUE EN LA PRIMERA INSTALACIÓN EN ESTE EQUIPO HAY LICENCIA
                {
                    PlayerPrefs.SetInt("SecurityN", 0);
                }
            }
            else
            {
                TextSecurity.text = "Error 404";
                return; //SI NO HAY LICENCIA INICIAL SALE
            }
            if (SecurityN >= SecurityNlimit) //COMPRUEBA EL LIMITE DE CONEXIONES SIN LICENCIA O INTERNET SI EN ESTE EQUIPO EXISTE LA LICENCIA INICIAL
            {
                TextSecurity.text = "Error 404";
                return; //SI SUPERA EL CONTADOR SALE
            }
            //PlayerPrefs.DeleteKey("SecurityN");
            //PlayerPrefs.DeleteKey("LicenciaInicial");
            //SEGURIDAD


            if (LICENCIA.GetComponent<licencia>().LicenciaOk
            || InputCode.text == CodeOff.GetComponent<CodeOffline>().code.ToString())//COMPRUEBA LICENCIA
            {
                if (!NetworkClient.isConnected && !NetworkServer.active)
                    if (!NetworkClient.active)
                    {
                        textLoading.SetActive(true);
                        Invoke("comienzaServer", 0.5f);
                    }
            }
            */
        }
    }

    void comienzaServer()
    {
        ManagerHud.GetComponent<NetworkManager>().StartServer();
    }
}