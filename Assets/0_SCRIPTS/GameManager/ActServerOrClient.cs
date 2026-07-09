using UnityEngine;
using Mirror;

public class ActServerOrClient : NetworkBehaviour
{
    [SerializeField]
    private GameObject Servidor, Cliente;
    //[SerializeField]
    //private string L_play, L_stop;

    [SerializeField]
    private GameObject ZonaAdquiereAvatar, ZonaCapturaUsersBD;

    private float timer = 0f;
    private bool activo = false;


    public override void OnStartClient()
    {
        if (isClient)
            cliente();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isServer)
            servidor();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 5f) // Han pasado 5 segundos
        {
            if (ZonaAdquiereAvatar.activeSelf && !activo) // Si está activo
            {
                // Mover hacia abajo
                ZonaCapturaUsersBD.transform.position += Vector3.down * 10000f * Time.deltaTime;
                activo = true;
            }
        }
    }

    [Server]
    void servidor()
    {
        Cliente.SetActive(false);
        Servidor.SetActive(true);
    }

    [Client]
    void cliente()
    {
        Servidor.SetActive(false);
        Cliente.SetActive(true);
    }
    /*
    //CARGA ESCENAS
    public void play()
    {
        LoadOnline(L_play);
    }
    public void stop()
    {
        LoadOnline(L_stop);
    }
    [ServerCallback]
    public void LoadOnline(string sceneName)
    {
        NetworkManager.singleton.ServerChangeScene(sceneName);
        Debug.Log("CARGA ESCENA servidor");
    }
    
    [Client]
    public void OnClientSceneChanged(NetworkConnection conn)
    {
        NetworkManager.singleton.OnClientSceneChanged(conn);
        Debug.Log("CARGA ESCENA cliente");
    }
    */
}
