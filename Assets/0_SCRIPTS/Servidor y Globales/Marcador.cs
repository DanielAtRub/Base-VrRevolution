using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Marcador : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textNP1, textNP2, textNP3, textNP4, textNP5, textNP6;

    [SerializeField]
    private TextMeshProUGUI textVidaP1, textVidaP2, textVidaP3, textVidaP4, textVidaP5, textVidaP6;
    [SerializeField]
    private TextMeshProUGUI textPuntosP1, textPuntosP2, textPuntosP3, textPuntosP4, textPuntosP5, textPuntosP6;

    public string[] NombreP;
    public int[] VidaP;
    public int[] PuntosP;
    public GameObject[] MarcadoresPlayers;

    //EXPLOSIVOS
    [SerializeField]
    private GameObject Gamemanager;
    [SerializeField]
    private TextMeshProUGUI textExplosivosAct;

    void Start()
    {
    }

    void Update()
    {
        MarcadoresPlayers = GameObject.FindGameObjectsWithTag("Jugador");
        if (MarcadoresPlayers != null)
        {
            for (int i = 0; i < MarcadoresPlayers.Length; i++)
            {
                NombreP[i] = MarcadoresPlayers[i].GetComponent<Player>().Name;
                VidaP[i] = MarcadoresPlayers[i].GetComponent<Player>().PlayerHealth;
                PuntosP[i] = MarcadoresPlayers[i].GetComponent<Player>().PlayerPuntos;
            }
        }

        textNP1.text = NombreP[0];
        textNP2.text = NombreP[1];
        textNP3.text = NombreP[2];
        textNP4.text = NombreP[3];
        textNP5.text = NombreP[4];
        textNP6.text = NombreP[5];

        textVidaP1.text = VidaP[0].ToString();
        textVidaP2.text = VidaP[1].ToString();
        textVidaP3.text = VidaP[2].ToString();
        textVidaP4.text = VidaP[3].ToString();
        textVidaP5.text = VidaP[4].ToString();
        textVidaP6.text = VidaP[5].ToString();
        
        textPuntosP1.text = PuntosP[0].ToString();
        textPuntosP2.text = PuntosP[1].ToString();
        textPuntosP3.text = PuntosP[2].ToString();
        textPuntosP4.text = PuntosP[3].ToString();
        textPuntosP5.text = PuntosP[4].ToString();
        textPuntosP6.text = PuntosP[5].ToString();

        //EXPLOSIVOS
        //textExplosivosAct.text = Gamemanager.GetComponent<JuegoManager>().ExplosivosAct.ToString();
    }
}
