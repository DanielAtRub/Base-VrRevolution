using Mirror;
using UnityEngine;

public class Caida : NetworkBehaviour
{
    [SerializeField]
    private Transform limite;
    [SerializeField]
    private GameObject duplicado;
    [SerializeField]
    private Transform plataforma;
    [SyncVar]
    public bool cayendo;
    [SerializeField]
    private float velCaida, TiempoCaida, TiempoCaidaTotal;
    [SerializeField]
    private bool trig;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    [ClientCallback]
    void Update()
    {
        if (cayendo)
        {
            if (!plataforma)
            {
                if (GameObject.Find("Plataforma"))
                    plataforma = GameObject.Find("Plataforma").transform;
            }
            else
            {
                //CAE
                if (!trig)
                {
                    trig = true;
                    duplicado.SetActive(true);
                    CmdADuplicado(true);
                }
                limite.position = new Vector3(limite.position.x, plataforma.position.y + 0.02f, limite.position.z); //MANTIENE ESTATICO EN Y
                transform.position = new Vector3(transform.position.x, transform.position.y - velCaida, transform.position.z);

                //REPOSICIONA
                TiempoCaida -= Time.deltaTime;
                if (TiempoCaida <= 0)
                {
                    trig = false;

                    TiempoCaida = TiempoCaidaTotal;

                    cayendo = false;
                    CmdActualiza();
                    duplicado.SetActive(false);
                    CmdADuplicado(false);
                    transform.position = new Vector3(transform.position.x, plataforma.position.y, transform.position.z);
                    limite.position = new Vector3(limite.position.x, plataforma.position.y + 0.02f, limite.position.z);
                }

            }
        }

    }

    [Command]
    void CmdActualiza()
    {
        cayendo = false;
    }

    [Command]
    void CmdADuplicado(bool act)
    {
        RpcDuplicado(act);
    }
    [ClientRpc]
    void RpcDuplicado(bool act)
    {
        duplicado.SetActive(act);
    }
}
