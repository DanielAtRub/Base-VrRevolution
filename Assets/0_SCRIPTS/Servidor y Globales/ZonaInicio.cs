using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;

public class ZonaInicio : NetworkBehaviour
{
    [SerializeField]
    private GameObject GameManager;

    [SyncVar]
    public string IniName = "NO Name";
    [SyncVar]
    public string IniTeam = "NO Team";
    [SyncVar]
    public bool IniZurdo = false;
    [SyncVar]
    public float IniAltura = 1.8f;

    public TextMeshPro textName;
    public TextMeshPro textTeam;
    public TextMeshPro textAltura;

    //UI PLAYERS
    public TMP_InputField inputNameUI, inputEquipoUI, inputAlturaUI;

    void Start()
    {
    }

    void Update()
    {
        if (isServer)
            capturaDeUI();

        //LO MUESTRA
        textName.text = IniName;
        textTeam.text = IniTeam;
        textAltura.text = IniAltura.ToString();

        //ADAPTA ALTURA CILINDRO
        //Cilindro.localScale = new Vector3(Cilindro.localScale.x, IniAltura / 2f, Cilindro.localScale.z);
        //Cilindro.localPosition = new Vector3(Cilindro.localPosition.x, IniAltura / 2f, 
        //Cilindro.localPosition.z);
    }

    [Server]
    void capturaDeUI()
    {
        //LO CAPTA DE UIPLAYER
        IniName = inputNameUI.text;
        IniTeam = inputEquipoUI.text;
        IniAltura = float.Parse(inputAlturaUI.text);
        //if (float.TryParse(inputAlturaUI.text, out float val))
        //IniAltura = val;
    }

    [ServerCallback]
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.GetComponent<JuegoManager>().PlayersUbicadosL0 += 1;

            other.GetComponentInParent<Player>().Name = IniName;
            other.GetComponentInParent<Player>().Team = IniTeam;
            other.GetComponentInParent<Player>().Zurdo = IniZurdo;
            other.GetComponentInParent<Player>().MiAltura = IniAltura;
            //RESETEA
            //other.GetComponentInParent<Player>().ResetScala();
        }
    }
    [ServerCallback]
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.GetComponent<JuegoManager>().PlayersUbicadosL0 -= 1;

            other.GetComponentInParent<Player>().Name = "";
            other.GetComponentInParent<Player>().Team = "";
            other.GetComponentInParent<Player>().Zurdo = false;
            other.GetComponentInParent<Player>().MiAltura = 1.8f;
            //RESETEA
            //other.GetComponentInParent<Player>().ResetScala();
        }
    }
}
