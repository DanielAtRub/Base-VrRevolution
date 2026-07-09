using UnityEngine;
using System.Globalization;
using TMPro; 
using UnityEngine.UI;

public class ControlUiPlayers : MonoBehaviour
{
    [SerializeField]
    private GameObject Gamemanager, panelNoPlayer1, panelNoPlayer2, panelNoPlayer3,
        panelNoPlayer4, panelNoPlayer5, panelNoPlayer6;

    [SerializeField]
    private TMP_Text IdP1, IdP2, IdP3, IdP4, IdP5, IdP6;  

    [SerializeField]
    private GameObject[] Players;
    [SerializeField]
    private TMP_InputField inputNameUIp1, inputEquipoUIp1, inputAlturaUIp1,
        inputNameUIp2, inputEquipoUIp2, inputAlturaUIp2,
        inputNameUIp3, inputEquipoUIp3, inputAlturaUIp3,
        inputNameUIp4, inputEquipoUIp4, inputAlturaUIp4,
        inputNameUIp5, inputEquipoUIp5, inputAlturaUIp5,
        inputNameUIp6, inputEquipoUIp6, inputAlturaUIp6;

    [SerializeField]
    private Toggle ChicoP1, ChicoP2, ChicoP3, ChicoP4, ChicoP5, ChicoP6;

    [SerializeField]
    private Toggle GenderP1, GenderP2, GenderP3, GenderP4, GenderP5, GenderP6;

    [SerializeField]
    private GameObject BotonPlayers;

    [SerializeField]
    private bool JugadoresCapturados;

    // Start is called before the first frame update
    void Start()
    {   
    }

    public void PlayersCaptured()
    {
        JugadoresCapturados = true;
    }

    // Update is called once per frame
    void Update()
    {
        int n = Gamemanager.GetComponent<JuegoManager>().PlayersEnJuego.Length;

        //BOTON JUGAR
        if (Gamemanager.GetComponent<JuegoManager>().PlayersUbicadosCapturaUser ==
            Gamemanager.GetComponent<JuegoManager>().PlayersEnJuego.Length && n > 0 && !JugadoresCapturados)
            //if (n > 0)
            BotonPlayers.SetActive(true);
        else
            BotonPlayers.SetActive(false);

        //PANELES       
        IdP1.text = "";
        IdP2.text = "";
        IdP3.text = "";
        IdP4.text = "";
        IdP5.text = "";
        IdP6.text = "";

        // PRUEBAS
        Players = GameObject.FindGameObjectsWithTag("Jugador");
        if (Players.Length == 0)
        {
            panelNoPlayer1.SetActive(true);
            panelNoPlayer2.SetActive(true);
            panelNoPlayer3.SetActive(true);
            panelNoPlayer4.SetActive(true);
            panelNoPlayer5.SetActive(true);
            panelNoPlayer6.SetActive(true);
        }
        for (int i = 0; i < Players.Length; i++)
        {
            if (Players[i].GetComponent<IdGetVisor>().idGafas == 1)
            {
                panelNoPlayer1.SetActive(false);
                //SI LA CAPTURA DE LA B.D. = NULL
                inputNameUIp1.text = "Player " + Players[i].GetComponent<IdGetVisor>().idGafas.ToString();
                //SI CAPTURA DE LA B.D
                IdP1.text = Players[i].GetComponent<IdGetVisor>().idGafas.ToString(); //ID DEL PANEL
                inputNameUIp1.text = Players[i].GetComponent<Player>().Name;
                inputEquipoUIp1.text = Players[i].GetComponent<Player>().Team;

                //SEXO
                Players[i].GetComponent<Player>().Avatar = GenderP1.isOn;
                if (GenderP1.isOn)
                    Players[i].GetComponent<Player>().SetAvatar(false, true);
                else
                    Players[i].GetComponent<Player>().SetAvatar(true, false);

                //INMORTALIDAD
                Players[i].GetComponent<Player>().Inmortal = ChicoP1.isOn;
                if (ChicoP1.isOn)
                    Players[i].GetComponent<Player>().Inmortalidad_NO();
                else
                    Players[i].GetComponent<Player>().Inmortalidad_SI();
            }
            if (Players[i].GetComponent<IdGetVisor>().idGafas == 2)
            {
                panelNoPlayer2.SetActive(false);
                //SI LA CAPTURA DE LA B.D. = NULL
                inputNameUIp2.text = "Player " + Players[i].GetComponent<IdGetVisor>().idGafas.ToString();
                //SI CAPTURA DE LA B.D
                IdP2.text = Players[i].GetComponent<IdGetVisor>().idGafas.ToString(); //ID DEL PANEL
                inputNameUIp2.text = Players[i].GetComponent<Player>().Name;
                inputEquipoUIp2.text = Players[i].GetComponent<Player>().Team;
                //SEXO
                Players[i].GetComponent<Player>().Avatar = GenderP2.isOn;
                if (GenderP2.isOn)
                    Players[i].GetComponent<Player>().SetAvatar(false, true);
                else
                    Players[i].GetComponent<Player>().SetAvatar(true, false);

                //INMORTALIDAD
                Players[i].GetComponent<Player>().Inmortal = ChicoP2.isOn;
                if (ChicoP2.isOn)
                    Players[i].GetComponent<Player>().Inmortalidad_NO();
                else
                    Players[i].GetComponent<Player>().Inmortalidad_SI();
            }
            if (Players[i].GetComponent<IdGetVisor>().idGafas == 3)
            {
                panelNoPlayer3.SetActive(false);
                //SI LA CAPTURA DE LA B.D. = NULL
                inputNameUIp3.text = "Player " + Players[i].GetComponent<IdGetVisor>().idGafas.ToString();
                //SI CAPTURA DE LA B.D
                IdP3.text = Players[i].GetComponent<IdGetVisor>().idGafas.ToString(); //ID DEL PANEL
                inputNameUIp3.text = Players[i].GetComponent<Player>().Name;
                inputEquipoUIp3.text = Players[i].GetComponent<Player>().Team;
                //SEXO
                Players[i].GetComponent<Player>().Avatar = GenderP3.isOn;
                if (GenderP3.isOn)
                    Players[i].GetComponent<Player>().SetAvatar(false, true);
                else
                    Players[i].GetComponent<Player>().SetAvatar(true, false);

                //INMORTALIDAD
                Players[i].GetComponent<Player>().Inmortal = ChicoP3.isOn;
                if (ChicoP3.isOn)
                    Players[i].GetComponent<Player>().Inmortalidad_NO();
                else
                    Players[i].GetComponent<Player>().Inmortalidad_SI();
            }
            if (Players[i].GetComponent<IdGetVisor>().idGafas == 4)
            {
                panelNoPlayer4.SetActive(false);
                //SI LA CAPTURA DE LA B.D. = NULL
                inputNameUIp4.text = "Player " + Players[i].GetComponent<IdGetVisor>().idGafas.ToString();
                //SI CAPTURA DE LA B.D
                IdP4.text = Players[i].GetComponent<IdGetVisor>().idGafas.ToString(); //ID DEL PANEL
                inputNameUIp4.text = Players[i].GetComponent<Player>().Name;
                inputEquipoUIp4.text = Players[i].GetComponent<Player>().Team;
                //SEXO
                Players[i].GetComponent<Player>().Avatar = GenderP4.isOn;
                if (GenderP4.isOn)
                    Players[i].GetComponent<Player>().SetAvatar(false, true);
                else
                    Players[i].GetComponent<Player>().SetAvatar(true, false);

                //INMORTALIDAD
                Players[i].GetComponent<Player>().Inmortal = ChicoP4.isOn;
                if (ChicoP4.isOn)
                    Players[i].GetComponent<Player>().Inmortalidad_NO();
                else
                    Players[i].GetComponent<Player>().Inmortalidad_SI();
            }
            if (Players[i].GetComponent<IdGetVisor>().idGafas == 5)
            {
                panelNoPlayer5.SetActive(false);
                //SI LA CAPTURA DE LA B.D. = NULL
                inputNameUIp5.text = "Player " + Players[i].GetComponent<IdGetVisor>().idGafas.ToString();
                //SI CAPTURA DE LA B.D
                IdP5.text = Players[i].GetComponent<IdGetVisor>().idGafas.ToString(); //ID DEL PANEL
                inputNameUIp5.text = Players[i].GetComponent<Player>().Name;
                inputEquipoUIp5.text = Players[i].GetComponent<Player>().Team;
                //SEXO
                Players[i].GetComponent<Player>().Avatar = GenderP5.isOn;
                if (GenderP5.isOn)
                    Players[i].GetComponent<Player>().SetAvatar(false, true);
                else
                    Players[i].GetComponent<Player>().SetAvatar(true, false);

                //INMORTALIDAD
                Players[i].GetComponent<Player>().Inmortal = ChicoP5.isOn;
                if (ChicoP5.isOn)
                    Players[i].GetComponent<Player>().Inmortalidad_NO();
                else
                    Players[i].GetComponent<Player>().Inmortalidad_SI();
            }
            if (Players[i].GetComponent<IdGetVisor>().idGafas == 6)
            {
                panelNoPlayer6.SetActive(false);
                //SI LA CAPTURA DE LA B.D. = NULL
                inputNameUIp6.text = "Player " + Players[i].GetComponent<IdGetVisor>().idGafas.ToString();
                //SI CAPTURA DE LA B.D
                IdP6.text = Players[i].GetComponent<IdGetVisor>().idGafas.ToString(); //ID DEL PANEL
                inputNameUIp6.text = Players[i].GetComponent<Player>().Name;
                inputEquipoUIp6.text = Players[i].GetComponent<Player>().Team;
                //SEXO
                Players[i].GetComponent<Player>().Avatar = GenderP6.isOn;
                if (GenderP6.isOn)
                    Players[i].GetComponent<Player>().SetAvatar(false, true);
                else
                    Players[i].GetComponent<Player>().SetAvatar(true, false);

                //INMORTALIDAD
                Players[i].GetComponent<Player>().Inmortal = ChicoP6.isOn;
                if (ChicoP6.isOn)
                    Players[i].GetComponent<Player>().Inmortalidad_NO();
                else
                    Players[i].GetComponent<Player>().Inmortalidad_SI();
            }
        }
        // PRUEBAS

        /*
        if (n == 0)
        {
            panelNoPlayer1.SetActive(true);
            panelNoPlayer2.SetActive(true);
            panelNoPlayer3.SetActive(true);
            panelNoPlayer4.SetActive(true);
            panelNoPlayer5.SetActive(true);
            panelNoPlayer6.SetActive(true);
        }
        if (n == 1)
        {
            panelNoPlayer1.SetActive(false);
            panelNoPlayer2.SetActive(true);
            panelNoPlayer3.SetActive(true);
            panelNoPlayer4.SetActive(true);
            panelNoPlayer5.SetActive(true);
            panelNoPlayer6.SetActive(true);

            //ACTUALIZA LAS VARIABLES DEL PLAYER
            Players = GameObject.FindGameObjectsWithTag("Jugador");
            if (Players != null)
            {
                //Players[0].GetComponent<Player>().Name = inputNameUIp1.text;
                //Players[0].GetComponent<Player>().Team = inputEquipoUIp1.text;
                //Players[0].GetComponent<Player>().Avatar = ChicoP1.isOn;
                //Players[0].GetComponent<Player>().MiAltura = float.Parse(inputAlturaUIp1.text);
            }
        }
        if (n == 2)
        {
            panelNoPlayer1.SetActive(false);
            panelNoPlayer2.SetActive(false);
            panelNoPlayer3.SetActive(true);
            panelNoPlayer4.SetActive(true);
            panelNoPlayer5.SetActive(true);
            panelNoPlayer6.SetActive(true);

            //ACTUALIZA LAS VARIABLES DEL PLAYER
            Players = GameObject.FindGameObjectsWithTag("Jugador");
            if (Players != null)
            {
                //Players[0].GetComponent<Player>().Name = inputNameUIp1.text;
                //Players[0].GetComponent<Player>().Team = inputEquipoUIp1.text;
                //Players[0].GetComponent<Player>().Avatar = ChicoP1.isOn;
                //Players[0].GetComponent<Player>().MiAltura = float.Parse(inputAlturaUIp1.text);

                //Players[1].GetComponent<Player>().Name = inputNameUIp2.text;
                //Players[1].GetComponent<Player>().Team = inputEquipoUIp2.text;
                //Players[1].GetComponent<Player>().Avatar = ChicoP2.isOn;
                //Players[1].GetComponent<Player>().MiAltura = float.Parse(inputAlturaUIp2.text);
            }
        }
        if (n == 3)
        {
            panelNoPlayer1.SetActive(false);
            panelNoPlayer2.SetActive(false);
            panelNoPlayer3.SetActive(false);
            panelNoPlayer4.SetActive(true);
            panelNoPlayer5.SetActive(true);
            panelNoPlayer6.SetActive(true);

            //ACTUALIZA LAS VARIABLES DEL PLAYER
            Players = GameObject.FindGameObjectsWithTag("Jugador");
            if (Players != null)
            {
                Players[0].GetComponent<Player>().Name = inputNameUIp1.text;
                Players[0].GetComponent<Player>().Team = inputEquipoUIp1.text;
                Players[0].GetComponent<Player>().Avatar = ChicoP1.isOn;
                //Players[0].GetComponent<Player>().MiAltura = float.Parse(inputAlturaUIp1.text);

                Players[1].GetComponent<Player>().Name = inputNameUIp2.text;
                Players[1].GetComponent<Player>().Team = inputEquipoUIp2.text;
                Players[1].GetComponent<Player>().Avatar = ChicoP2.isOn;
                //Players[1].GetComponent<Player>().MiAltura = float.Parse(inputAlturaUIp2.text);

                Players[2].GetComponent<Player>().Name = inputNameUIp3.text;
                Players[2].GetComponent<Player>().Team = inputEquipoUIp3.text;
                Players[2].GetComponent<Player>().Avatar = ChicoP3.isOn;
                //Players[2].GetComponent<Player>().MiAltura = float.Parse(inputAlturaUIp3.text);
            }
        }
        if (n == 4)
        {
            panelNoPlayer1.SetActive(false);
            panelNoPlayer2.SetActive(false);
            panelNoPlayer3.SetActive(false);
            panelNoPlayer4.SetActive(false);
            panelNoPlayer5.SetActive(true);
            panelNoPlayer6.SetActive(true);

            //ACTUALIZA LAS VARIABLES DEL PLAYER
            Players = GameObject.FindGameObjectsWithTag("Jugador");
            if (Players != null)
            {
                Players[0].GetComponent<Player>().Name = inputNameUIp1.text;
                Players[0].GetComponent<Player>().Team = inputEquipoUIp1.text;
                Players[0].GetComponent<Player>().Avatar = ChicoP1.isOn;
                //Players[0].GetComponent<Player>().MiAltura = float.Parse(inputAlturaUIp1.text);

                Players[1].GetComponent<Player>().Name = inputNameUIp2.text;
                Players[1].GetComponent<Player>().Team = inputEquipoUIp2.text;
                Players[1].GetComponent<Player>().Avatar = ChicoP2.isOn;
                //Players[1].GetComponent<Player>().MiAltura = float.Parse(inputAlturaUIp2.text);

                Players[2].GetComponent<Player>().Name = inputNameUIp3.text;
                Players[2].GetComponent<Player>().Team = inputEquipoUIp3.text;
                Players[2].GetComponent<Player>().Avatar = ChicoP3.isOn;
                //Players[2].GetComponent<Player>().MiAltura = float.Parse(inputAlturaUIp3.text);

                Players[3].GetComponent<Player>().Name = inputNameUIp4.text;
                Players[3].GetComponent<Player>().Team = inputEquipoUIp4.text;
                Players[3].GetComponent<Player>().Avatar = ChicoP4.isOn;
                //Players[3].GetComponent<Player>().MiAltura = float.Parse(inputAlturaUIp4.text);
            }
        }
        if (n == 5)
        {
            panelNoPlayer1.SetActive(false);
            panelNoPlayer2.SetActive(false);
            panelNoPlayer3.SetActive(false);
            panelNoPlayer4.SetActive(false);
            panelNoPlayer5.SetActive(false);
            panelNoPlayer6.SetActive(true);

            //ACTUALIZA LAS VARIABLES DEL PLAYER
            Players = GameObject.FindGameObjectsWithTag("Jugador");
            if (Players != null)
            {
                Players[0].GetComponent<Player>().Name = inputNameUIp1.text;
                Players[0].GetComponent<Player>().Team = inputEquipoUIp1.text;
                Players[0].GetComponent<Player>().Avatar = ChicoP1.isOn;
                //Players[0].GetComponent<Player>().MiAltura = float.Parse(inputAlturaUIp1.text);

                Players[1].GetComponent<Player>().Name = inputNameUIp2.text;
                Players[1].GetComponent<Player>().Team = inputEquipoUIp2.text;
                Players[1].GetComponent<Player>().Avatar = ChicoP2.isOn;
                //Players[1].GetComponent<Player>().MiAltura = float.Parse(inputAlturaUIp2.text);

                Players[2].GetComponent<Player>().Name = inputNameUIp3.text;
                Players[2].GetComponent<Player>().Team = inputEquipoUIp3.text;
                Players[2].GetComponent<Player>().Avatar = ChicoP3.isOn;
                //Players[2].GetComponent<Player>().MiAltura = float.Parse(inputAlturaUIp3.text);

                Players[3].GetComponent<Player>().Name = inputNameUIp4.text;
                Players[3].GetComponent<Player>().Team = inputEquipoUIp4.text;
                Players[3].GetComponent<Player>().Avatar = ChicoP4.isOn;
                //Players[3].GetComponent<Player>().MiAltura = float.Parse(inputAlturaUIp4.text);

                Players[4].GetComponent<Player>().Name = inputNameUIp5.text;
                Players[4].GetComponent<Player>().Team = inputEquipoUIp5.text;
                Players[4].GetComponent<Player>().Avatar = ChicoP5.isOn;
                //Players[4].GetComponent<Player>().MiAltura = float.Parse(inputAlturaUIp5.text);
            }
        }
        if (n == 6)
        {
            panelNoPlayer1.SetActive(false);
            panelNoPlayer2.SetActive(false);
            panelNoPlayer3.SetActive(false);
            panelNoPlayer4.SetActive(false);
            panelNoPlayer5.SetActive(false);
            panelNoPlayer6.SetActive(false);

            //ACTUALIZA LAS VARIABLES DEL PLAYER
            Players = GameObject.FindGameObjectsWithTag("Jugador");
            if (Players != null)
            {
                Players[0].GetComponent<Player>().Name = inputNameUIp1.text;
                Players[0].GetComponent<Player>().Team = inputEquipoUIp1.text;
                Players[0].GetComponent<Player>().Avatar = ChicoP1.isOn;
                //Players[0].GetComponent<Player>().MiAltura = float.Parse(inputAlturaUIp1.text);

                Players[1].GetComponent<Player>().Name = inputNameUIp2.text;
                Players[1].GetComponent<Player>().Team = inputEquipoUIp2.text;
                Players[1].GetComponent<Player>().Avatar = ChicoP2.isOn;
                //Players[1].GetComponent<Player>().MiAltura = float.Parse(inputAlturaUIp2.text);

                Players[2].GetComponent<Player>().Name = inputNameUIp3.text;
                Players[2].GetComponent<Player>().Team = inputEquipoUIp3.text;
                Players[2].GetComponent<Player>().Avatar = ChicoP3.isOn;
                //Players[2].GetComponent<Player>().MiAltura = float.Parse(inputAlturaUIp3.text);

                Players[3].GetComponent<Player>().Name = inputNameUIp4.text;
                Players[3].GetComponent<Player>().Team = inputEquipoUIp4.text;
                Players[3].GetComponent<Player>().Avatar = ChicoP4.isOn;
                //Players[3].GetComponent<Player>().MiAltura = float.Parse(inputAlturaUIp4.text);

                Players[4].GetComponent<Player>().Name = inputNameUIp5.text;
                Players[4].GetComponent<Player>().Team = inputEquipoUIp5.text;
                Players[4].GetComponent<Player>().Avatar = ChicoP5.isOn;
                //Players[5].GetComponent<Player>().MiAltura = float.Parse(inputAlturaUIp5.text);

                Players[5].GetComponent<Player>().Name = inputNameUIp6.text;
                Players[5].GetComponent<Player>().Team = inputEquipoUIp6.text;
                Players[5].GetComponent<Player>().Avatar = ChicoP6.isOn;
                //Players[5].GetComponent<Player>().MiAltura = float.Parse(inputAlturaUIp6.text);
            }
        }*/

    }
}
