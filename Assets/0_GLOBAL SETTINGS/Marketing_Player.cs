using UnityEngine;
using Mirror;

public class Marketing_Player : NetworkBehaviour
{
    [SerializeField]
    private GameObject GameManager;
    [SerializeField]
    private GameObject Limite;
    [SerializeField]
    private GameObject Rejilla;
    [SerializeField]
    private GameObject Nombre;//, Equipo, Vida;

    // Start is called before the first frame update
    void Start()
    {
        if (isServer)
        {
            if (!GameManager)
                GameManager = GameObject.Find("GameManager");
            if (GameManager.GetComponent<JuegoManager>().isMARKETING) // DESACTIVA
            {
                Limite.GetComponent<SpriteRenderer>().enabled = false;
                Rejilla.GetComponent<MeshRenderer>().enabled = false;
                Nombre.SetActive(false);
                //Equipo.SetActive(false);
                //Vida.SetActive(false);
            }
            else // ACTIVA
            {
                Limite.GetComponent<SpriteRenderer>().enabled = true;
                Rejilla.GetComponent<MeshRenderer>().enabled = true;
                Nombre.SetActive(true);
                //Equipo.SetActive(true);
                //Vida.SetActive(true);
            }
        }
    }

    public void MarketinJugador()
    {
        if (isServer) 
        {
            if (!GameManager)
                GameManager = GameObject.Find("GameManager");
            if (GameManager.GetComponent<JuegoManager>().isMARKETING) // DESACTIVA
            {
                Limite.GetComponent<SpriteRenderer>().enabled = false;
                Rejilla.GetComponent<MeshRenderer>().enabled = false;
                Nombre.SetActive(false);
                //Equipo.SetActive(false);
                //Vida.SetActive(false);
            }
            else // ACTIVA
            {
                Limite.GetComponent<SpriteRenderer>().enabled = true;
                Rejilla.GetComponent<MeshRenderer>().enabled = true;
                Nombre.SetActive(true);
                //Equipo.SetActive(true);
                //Vida.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/
}
