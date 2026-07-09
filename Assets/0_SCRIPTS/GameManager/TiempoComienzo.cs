using UnityEngine;
using TMPro;
using Mirror;

public class TiempoComienzo : NetworkBehaviour
{
    [SerializeField]
    private TextMeshPro textTiempo;
    [SyncVar]
    public float tiempo;
    [SerializeField]
    private GameObject manager;
    [SerializeField]
    private bool L1;

    // Start is called before the first frame update
    void Start()
    {
    }

    //[ServerCallback]
    void Update()
    {
        /*
        if (isServerOnly)
        {
            tiempo -= Time.deltaTime;
            if (tiempo <= 0)
            {
                tiempo = 0;
                if (L1)
                    manager.GetComponent<JuegoManager>().StartOrda1();
                //if (L2)
                    //manager.GetComponent<JuegoManager>().StartBoss();
                gameObject.SetActive(false);
                RpcDes();
            }
        }
        int tp = (int)tiempo;
        textTiempo.text = tp.ToString();
        */
    }
    
    [ClientRpc]
    void RpcDes()
    {
        gameObject.SetActive(false);
    }
    
}
