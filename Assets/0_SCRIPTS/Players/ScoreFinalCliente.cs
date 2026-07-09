using UnityEngine;
using TMPro;
using System;

public class ScoreFinalCliente : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] P; //PUNTOS
    //[SerializeField]
    //private TextMeshProUGUI E1, E2, E3, E4, E5, E6; //ENEMIGOS
    [SerializeField]
    private TextMeshProUGUI[] J; //JUGADOR

    [SerializeField]
    private int[] PuntosP;
    //[SerializeField]
    //private int[] EnemigosP;
    [SerializeField]
    private string[] JugadorP;
    [SerializeField]
    private GameObject[] Players;

    [SerializeField]
    private GameObject LeeBd;
    [SerializeField]
    private TextMeshProUGUI Equipo; //EQUIPO

    // Start is called before the first frame update
    void Start()
    {
        /*
        Players = GameObject.FindGameObjectsWithTag("Jugador");
        if (Players != null)
        {
            for (int i = 0; i < Players.Length; i++)
            {
                JugadorP[i] = Players[i].GetComponent<Player>().Name;
                PuntosP[i] = Players[i].GetComponent<Player>().PlayerPuntos;
                //EnemigosP[i] = MarcadoresPlayers[i].GetComponent<Player>().EnemigosMatados;
            }
            Array.Sort(PuntosP, JugadorP);
        }
        */
    }

    // Update is called once per frame
    //void Update()
    void OnEnable()
    {
        Equipo.text = LeeBd.GetComponent<LeeBd>().n_Equipo;

        Players = GameObject.FindGameObjectsWithTag("Jugador");
        JugadorP = new string[Players.Length];
        PuntosP = new int[Players.Length];
        if (Players != null)
        {
            for (int i = 0; i < Players.Length; i++)
            {
                JugadorP[i] = Players[i].GetComponent<Player>().Name;
                PuntosP[i] = -Players[i].GetComponent<Player>().PlayerPuntos; //PRUEBAS
            }
            Array.Sort(PuntosP, JugadorP);
            for (int i = 0; i < Players.Length; i++)
            {
                J[i].text = JugadorP[i];
                P[i].text = (PuntosP[i] * -1).ToString();
            }
        }
    }
}
