using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Security;
using UnityEngine.Networking;

public class Highscore : MonoBehaviour
{
    //Text to display the result on
    public Text statusText;
    public Text[] UsuarioText, EquipoText, PuntosText;

    //public string _usuario, _mail, _centro, _equipo;
    //public int _juego, _puntos;

    public GameObject[] Players;

    void Start()
    {
    }

    public void GetAll()
    {
        StartCoroutine(GetALLScores());
    }

    public void GetCentro()
    {
        StartCoroutine(GetCENTROScores("Sambil"));
    }

    public void Post()
    {
        Players = GameObject.FindGameObjectsWithTag("Jugador");
        if (Players != null)
        {
            for (int i = 0; i < Players.Length; i++)
            {
                //VidaP[i] = MarcadoresPlayers[i].GetComponent<Player>().PlayerHealth;
                string UsuarioP = Players[i].GetComponent<Player>().Name;
                string EquipoP = Players[i].GetComponent<Player>().Team;
                int PuntosP = Players[i].GetComponent<Player>().PlayerPuntos;
                StartCoroutine(PostScores(UsuarioP, "a@a.com", "Sambil", 1, EquipoP, PuntosP));
            }
        }
    }

    IEnumerator PostScores(string Usuario, string Mail, string Centro, int Juego, string Equipo, int Puntos)
    {
        string post_url = "https://vrairsoft.es/wp-content/vrgames/php/addscore.php?" +
            "usuario=" + Usuario +
            "&mail=" + Mail +
            "&centro=" + Centro +
            "&juego=" + Juego +
            "&equipo=" + Equipo +
            "&puntos=" + Puntos;
        
        // Post the URL to the site and create a download object to get the result.
        WWW hs_post = new WWW(post_url);
        yield return hs_post; // Wait until the download is done

        if (hs_post.error != null)
            Debug.Log("There was an error posting the high score: " + hs_post.error);
        else
            Debug.Log(post_url);
    }

    IEnumerator GetALLScores()
    {
        statusText.text = "Loading Scores";
        WWW hs_get = new WWW("https://vrairsoft.es/wp-content/vrgames/php/displayAll.php");
        yield return hs_get;

        if (hs_get.error != null)
            Debug.Log("There was an error getting the high score: " + hs_get.error);
        else
            statusText.text = hs_get.text; // this is a GUIText that will display the scores in game.
        /*
        //MUESTRA LA LINEA
        string[] temp = hs_get.text.Split("\n".ToCharArray());
        statusText.text = temp[0];
        */
    }

    IEnumerator GetCENTROScores(string Centro)
    {
        statusText.text = "Loading Scores";
        WWW hs_get = new WWW("https://vrairsoft.es/wp-content/vrgames/php/displayCentro.php");
        yield return hs_get;

        if (hs_get.error != null)
            Debug.Log("There was an error getting the high score: " + hs_get.error);
        else
            statusText.text = hs_get.text; // this is a GUIText that will display the scores in game.
        
        //MUESTRA LA LINEA
        string[] fila = hs_get.text.Split("\n".ToCharArray());
        for (int i = 0; i < fila.Length-1; i++)
        {
            string[] campo = fila[i].Split("\t".ToCharArray());
            UsuarioText[i].text = campo[0];
            EquipoText[i].text = campo[1];
            PuntosText[i].text = campo[2];
        }
    }
}