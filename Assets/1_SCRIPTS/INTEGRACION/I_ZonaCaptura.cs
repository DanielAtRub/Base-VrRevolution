using UnityEngine;
using Mirror;
using TMPro;

public class I_ZonaCaptura : NetworkBehaviour
{
    [Header("GLOBALES")]
    [SerializeField]
    private int ZonaC;
    [Header("USUARIO")]
    public int id; // PROVIENE DEL PLAYER
    [SyncVar]
    public string n_Usuario, n_Equipo; // LO LLENA EL SCRIPT DE B.D.
    [Header("TEXTOS")]
    [SerializeField]
    private TMP_Text textId;
    [SerializeField]
    private TMP_Text textUsuario, textEquipo;
    [Header("OTROS")]
    [SerializeField]
    private GameObject GameManager;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        textUsuario.text = n_Usuario;
        textEquipo.text = n_Equipo;
    }

    [ServerCallback]
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ubicacion"))
        {
            id = other.GetComponentInParent<IdGetVisor>().idGafas;
            //textId.text = id.ToString();

            other.GetComponentInParent<Player>().Name = n_Usuario;
            other.GetComponentInParent<Player>().Team = n_Equipo;

            GameManager.GetComponent<JuegoManager>().PlayersUbicadosCapturaUser++;
        }
    }

    [ServerCallback]
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ubicacion"))
        {
            GameManager.GetComponent<JuegoManager>().PlayersUbicadosCapturaUser--;
        }
    }

}
