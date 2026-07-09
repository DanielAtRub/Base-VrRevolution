using UnityEngine;

public class Marketing_Decorado : MonoBehaviour
{
    [SerializeField]
    private GameObject GameManager;
    [SerializeField]
    private GameObject[] LimiteDeco;
    [SerializeField]
    private GameObject[] PlayersEnJuego;

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnEnable()
    {
        // DESORADO
        if (!GameManager)
            GameManager = GameObject.Find("GameManager");
        if (GameManager.GetComponent<JuegoManager>().isMARKETING) // DESACTIVA
        {
            for (int i = 0; i < LimiteDeco.Length; i++)
            {
                LimiteDeco[i].GetComponent<MeshRenderer>().enabled = false;
            }
        }

        //PLAYERS
        PlayersEnJuego = GameObject.FindGameObjectsWithTag("Jugador");
        for (int i = 0; i < PlayersEnJuego.Length; i++)
        {
            PlayersEnJuego[i].GetComponentInParent<Marketing_Player>().MarketinJugador();
        }
    }

    void OnDisable()
    {
        // DESORADO
        if (!GameManager)
            GameManager = GameObject.Find("GameManager");
        if (!GameManager.GetComponent<JuegoManager>().isMARKETING) // ACTIVA
        {
            for (int i = 0; i < LimiteDeco.Length; i++)
            {
                LimiteDeco[i].GetComponent<MeshRenderer>().enabled = true;
            }
        }

        //PLAYERS
        PlayersEnJuego = GameObject.FindGameObjectsWithTag("Jugador");
        for (int i = 0; i < PlayersEnJuego.Length; i++)
        {
            PlayersEnJuego[i].GetComponentInParent<Marketing_Player>().MarketinJugador();
        }
    }
}
