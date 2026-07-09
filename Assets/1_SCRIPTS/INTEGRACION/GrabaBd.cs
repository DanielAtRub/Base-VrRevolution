/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GrabaBd : MonoBehaviour
{
    [Header("DATOS PARTIDA")]
    [SerializeField]
    private GameObject[] Players;
    [SerializeField]
    private GameObject _Manager;
    [Header("MSJ")]
    [SerializeField]
    private GameObject MsjGuardado;

    //DEFINIMOS LA ESTRUCTURA DE DATOS
    [System.Serializable]
    public class GameData
    {
        public int Game_id;
        public int Sala;
        public int Records_id;
        public int Total_plazas;
        public string Team_name_1;
        public string Team_name_2;
        public string User_nick_1;
        public string User_nick_2;
        public string User_nick_3;
        public string User_nick_4;
        public string User_nick_5;
        public string User_nick_6;
        public int Rounds_team_1;
        public int Rounds_team_2;
        public int Timer;
    }

    [System.Serializable]
    public class UsersData
    {
        public int Records_id;
        public int User_id;
        public string User_nick;
        public string Team_name;
        public bool Logeado;
        public int Score;
        public int Killings;
        public int Deaths;
        public int Headshots;
        public int Enemies_shots;
        public int Null_shots;
        public int Total_shots;
        public int Total_flags;
    }

    [System.Serializable]
    public class DataToSend
    {
        public string tenant_id;
        public string action;
        public GameData game_data;
        public UsersData[] users_data;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void GrabaDatosBd()
    {
        // CARGA DATOS DE JUGADORES
        Players = GameObject.FindGameObjectsWithTag("Jugador");
        UsersData[] users_data = new UsersData[Players.Length];
        //System.Array.Resize(ref users_data, Players.Length);

        for (int i = 0; i < Players.Length; i++)
        {
            //ASIGNAMOS LOS DATOS DE PARTIDA DEL JUGADOR
            users_data[i] = new UsersData
            {
                User_id = i + 1,
                User_nick = Players[i].GetComponent<Player>().Name,
                Team_name = Players[i].GetComponent<Player>().Team,
                Killings = Players[i].GetComponent<Player>().EnemigosMatados,
                Records_id = 0, //Dejar este valor por defecto
                Score = Players[i].GetComponent<Player>().PlayerPuntos,
                Deaths = 0,
                Headshots = 0,
                Enemies_shots = 0,
                Null_shots = 0,
                Total_shots = 0,
                Total_flags = 0,
                Logeado = false //Dejar este valor sin tocar
            };
        }

        //ARRAY CON LOS DATOS DE LA PARTIDA
        GameData gameData = new GameData
        {
            Game_id = 3,  //Asignar valor del 1 al 6 según el juego
            Sala = 1,  //Asignar el número de la sala. 1 por defecto
            Records_id = 0,  //Dejar este valor sin tocar
            Total_plazas = Players.Length,
            Team_name_1 = "VrTeam", //"Rojo" o "Negro" para los juegos competitivos o "VrTeam" para los colaborativos
            Team_name_2 = "", //"Rojo" o "Negro" para los juegos competitivos o "VrTeam" para los colaborativos
            User_nick_1 = users_data.Length > 0 ? users_data[0].User_nick : "",
            User_nick_2 = users_data.Length > 1 ? users_data[1].User_nick : "",
            User_nick_3 = users_data.Length > 2 ? users_data[2].User_nick : "",
            User_nick_4 = users_data.Length > 3 ? users_data[3].User_nick : "",
            User_nick_5 = users_data.Length > 4 ? users_data[4].User_nick : "",
            User_nick_6 = users_data.Length > 5 ? users_data[5].User_nick : "",
            Rounds_team_1 = 0,
            Rounds_team_2 = 0,
            Timer = (int)_Manager.GetComponent<JuegoManager>().TiempoPartidaTotal / 60, //Incluir la duración de la partida en minutos 10, 30, 60 lo que sea
        };

        DataToSend dataToSend = new DataToSend
        {
            //INDICA EL CENTRO QUE REALIZA LA PETICION
            tenant_id = "MW001", //Para "Magic World" utilizaremos este código
            //INDICA LA ACCION A REALIZAR POR EL ARCHIVO DEL SERVIDOR
            action = "Create", //Usaremos siempre "Create" para este tipo de clientes de partidas directas
            game_data = gameData,
            users_data = users_data
        };

        StartCoroutine(SendData(dataToSend));
    }

    IEnumerator SendData(DataToSend data)
    {
        //CONVERTIMOS LOS DATOS A JSON
        string jsonData = JsonUtility.ToJson(data);

        //URL DEL ARCHIVO DEL SERVIDOR
        string url = "https://vr-manager.inusualinteractive.com/modelos/recordsInsert.php";

        //CREAMOS LA SOLICITUD
        UnityWebRequest www = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        //ENVIAMOS LA SOLICITUD Y ESPERAMOS LA RESPUESTA
        yield return www.SendWebRequest();

        //RECIBIMOS LA RESPUESTA PARA GESTIONARLA (ESTO REALMENTE CREO QUE PODEMOS OBVIARLO)
        /*if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Data sent successfully: " + www.downloadHandler.text);

            //MSJ GUARDADO PARTIDA CON EXITO
            MsjGuardado.SetActive(true);

        }
        else
        {
            Debug.LogError("Error sending data: " + www.error);
        }*/
/*    }

}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabaBd : MonoBehaviour
{
    [System.Serializable]
    public class PlayerDatos
    {
        public string nombre, n_equipo;
        public int puntos, vencidos;
    }
    [Header("GLOBALES")]
    [SerializeField]
    private string StringEsteJuego; // CONSTANTE
    [Header("DATOS USUARIOS")]
    [SerializeField]
    private GameObject[] Players;
    [SerializeField]
    private PlayerDatos[] User;
    [SerializeField]
    private GameObject _LeeBd; // SCRPT DE LECTURA DE B.D.
    [SerializeField]
    private int Records_id;
    [Header("DATOS PARTIDA")]
    [SerializeField]
    private GameObject _Manager;
    [Header("MSJ")]
    [SerializeField]
    private GameObject MsjGuardado;

    private string post_url;
    private WWW hs_post;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void GrabaDatosBd()
    {
        if (!_LeeBd.GetComponent<LeeBd>().PartidaTest) // SI NO ES PARTIDATEST GRABA
            StartCoroutine(PostRecordsUpdateUsers());
    }

    IEnumerator PostRecordsUpdateUsers()
    {
        // CARGA DATOS DE JUGADORES
        Players = GameObject.FindGameObjectsWithTag("Jugador");
        System.Array.Resize(ref User, Players.Length);
        Records_id = _LeeBd.GetComponent<LeeBd>().NumeroPartidaBd;
        for (int i = 0; i < User.Length; i++)
        {


            // === ¡AQUÍ ESTÁ LA SOLUCIÓN! ===
            // Si la casilla en memoria está vacía (null), creamos una nueva instancia de PlayerDatos
            if (User[i] == null) User[i] = new PlayerDatos();
            // Obtenemos el componente Player de forma segura
            Player playerScript = Players[i].GetComponent<Player>();


            User[i].puntos = Players[i].GetComponent<Player>().PlayerPuntos;
            User[i].nombre = Players[i].GetComponent<Player>().Name;
            User[i].n_equipo = Players[i].GetComponent<Player>().Team;
            User[i].vencidos = Players[i].GetComponent<Player>().EnemigosMatados;
            // GRABA EN LA B.D.
            post_url = "https://vrsystem.vrairsoftrevolution.com/modelos/recordsUpdate.php?" +
               "records_id=" + Records_id + //EL ID DE LA PARTIDA
               "&user_nick=" + User[i].nombre + //EL NICK DEL JUGADOR
               "&score=" + User[i].puntos + //PUNTOS OBTENIDOS POR EL JUGADOR. SI ESTE JUEGO NO USA ESTE CAMPO, EL VALOR DEBERA SER CERO
               "&killings=" + User[i].vencidos + //ENEMIGOS ELIMINADOS POR EL JUGADOR. SI ESTE JUEGO NO USA ESTE CAMPO, EL VALOR DEBERA SER CERO
               "&deaths=" + 0 + //VECES QUE HA MUERTO EL JUGADOR. SI ESTE JUEGO NO USA ESTE CAMPO, EL VALOR DEBERA SER CERO
               "&headshots=" + 0 + //TIROS EN LA CABEZA REALIZADOS POR EL JUGADOR. SI ESTE JUEGO NO USA ESTE CAMPO, EL VALOR DEBERA SER CERO
               "&enemies_shots=" + 0 + //DISPAROS IMPACTADOS EN EL ENEMIGO POR EL JUGADOR. SI ESTE JUEGO NO USA ESTE CAMPO, EL VALOR DEBERA SER CERO
               "&null_shots=" + 0 + //DISPAROS REALIZADOS EN VACIO POR EL JUGADOR. SI ESTE JUEGO NO USA ESTE CAMPO, EL VALOR DEBERA SER CERO
               "&total_shots=" + 0 + //TOTAL DE DISPAROS REALIZADOS POR EL JUGADOR. SI ESTE JUEGO NO USA ESTE CAMPO, EL VALOR DEBERA SER CERO
               "&total_flags=" + 0 + //TOTAL DE BANDERAS OBTENIDAS POR EL JUGADOR. SI ESTE JUEGO NO USA ESTE CAMPO, EL VALOR DEBERA SER CERO
               "&type=" + "users"; //EL TIPO DE CONSULTA QUE SE VA A REALIZAR (EN ESTE CASO users)
            hs_post = new WWW(post_url);
            yield return hs_post; //DEVOLVERA UN VALOR O UN ARRAY DE DATOS, SEGUN LA CONSULTA REALIZADA
            if (hs_post.error != null)
            {
                Debug.Log("Error al grabas B.D. JUGADORES" + hs_post.error);
                // SI NO GRBABA EN B.D. O NO HAY INTERNET QUE MUESTRE Y LLENE CON ZONAS ESTANDAR O QUE NO GRABE NADA ????
            }
            else
            {
                Debug.Log("OK al grabas B.D. JUGADORES");
                StartCoroutine(PostRecordsUpdate()); //PARA ACTUALIZAR LA TABLA DEL JUEGO
            }
        }
    }

    IEnumerator PostRecordsUpdate() // ¿ NO HAY RONDAS GRABAMOS DE TODOS MODOS ?
    {
        // GRABA EN LA B.D.
        post_url = "https://vrsystem.vrairsoftrevolution.com/modelos/recordsUpdate.php?" +
            "records_id=" + Records_id + //EL ID DE LA PARTIDA
            "&rounds_team_1=" + 0 + //RONDAS GANADAS POR EL EQUIPO 1. SI ESTE JUEGO NO USA ESTE CAMPO, EL VALOR DEBERA SER CERO
            "&rounds_team_2=" + 0 + //RONDAS GANADAS POR EL EQUIPO 2. SI ESTE JUEGO NO USA ESTE CAMPO, EL VALOR DEBERA SER CERO
            "&timer=" + 0 + //TIEMPO EN QUE SE HA CONSEGUIDO EL RETO. SI ESTE JUEGO NO USA ESTE CAMPO, EL VALOR DEBERA SER CERO
            "&type=" + StringEsteJuego; //EL TIPO DE CONSULTA QUE SE VA A REALIZAR (records, users, ses, capture, candy)
        hs_post = new WWW(post_url);
        yield return hs_post; //DEVOLVERA UN VALOR O UN ARRAY DE DATOS, SEGUN LA CONSULTA REALIZADA
        if (hs_post.error != null)
        {
            Debug.Log("Error al grabar B.D. PARTIDA" + hs_post.error);
            // SI NO GRBABA EN B.D. O NO HAY INTERNET QUE MUESTRE Y LLENE CON ZONAS ESTANDAR O QUE NO GRABE NADA ????
        }
        else
        {
            Debug.Log("OK al grabas B.D. PARTIDA");
            StartCoroutine(PostRecordsUpdateJugada()); //PARA ACTUALIZAR LA TABLA DE LA PARTIDA COMO JUGADA
        }
    }

    IEnumerator PostRecordsUpdateJugada()
    {
        // GRABA EN LA B.D.
        post_url = "https://vrsystem.vrairsoftrevolution.com/modelos/recordsUpdate.php?" +
            "records_id=" + Records_id + //EL ID DE LA PARTIDA
            "&rounds_team_1=" + 0 + //RONDAS GANADAS POR EL EQUIPO 1. SI ESTE JUEGO NO USA ESTE CAMPO, EL VALOR DEBERA SER CERO
            "&rounds_team_2=" + 0 + //RONDAS GANADAS POR EL EQUIPO 2. SI ESTE JUEGO NO USA ESTE CAMPO, EL VALOR DEBERA SER CERO
            "&timer=" + 0 + //TIEMPO EN QUE SE HA CONSEGUIDO EL RETO. SI ESTE JUEGO NO USA ESTE CAMPO, EL VALOR DEBERA SER CERO
            "&type=" + "records"; //EL TIPO DE CONSULTA QUE SE VA A REALIZAR (records, users, ses, capture, candy)
        hs_post = new WWW(post_url);
        yield return hs_post; //DEVOLVERA UN VALOR O UN ARRAY DE DATOS, SEGUN LA CONSULTA REALIZADA
        if (hs_post.error != null)
        {
            Debug.Log("Error al grabar B.D. PARTIDA JUGADA" + hs_post.error);
            // SI NO GRBABA EN B.D. O NO HAY INTERNET QUE MUESTRE Y LLENE CON ZONAS ESTANDAR O QUE NO GRABE NADA ????
        }
        else
            Debug.Log("OK al grabar B.D. PARTIDA JUGADA");

        //MSJ GUARDADO PARTIDA CON EXITO
        //MsjGuardado.SetActive(true);
    }

}

