using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class LeeBd : NetworkBehaviour
{
    [System.Serializable]
    public class I_Bd_Usuario
    {
        public string n_Usuario, n_Equipo;
    }
    [Header("GLOBALES")]
    [SerializeField]
    private int EsteJuego; // CONSTANTE, NO SE USA, SOLO COMO REF DE LA LECTURA DE LA B.D.
    [SerializeField]
    private string StringEsteJuego; // CONSTANTE
    [Header("GLOBAL SETTINGS")]
    [SerializeField]
    private string _EsteCentro;
    [SerializeField]
    private int _EstaSala;
    [Header("TIPO PARTIDA")]
    public bool PartidaTest;
    [SerializeField]
    private GameObject MsjDesconectado;
    [SerializeField]
    private int N_Desconectado;
    [SerializeField]
    private TMP_InputField Text_N_Desconectado;
    [Header("DATOS LECTURA B.D.")]
    [SerializeField]
    private int JuegoBd;
    //[SerializeField]
    private string PartidaBd;
    public int NumeroPartidaBd;
    [SerializeField]
    private int NumeroJugadoresBd;
    //[SerializeField]
    private string DatosPartidaBd;
    private string DatosEquiposJugadoresBd;
    [Header("EQUIPOS")]
    public string n_Equipo;
    [Header("USUARIOS")]
    public I_Bd_Usuario[] Pid;
    [Header("ZONAS DE CAPTURA")]
    [SerializeField]
    private GameObject[] Zc;
    [Header("UBICACIONES ZC")]
    [SerializeField]
    private Transform[] Uzc;
    //[Header("TEXTOS EQUIPO UI")]
    //[SerializeField]
    //private TMP_InputField inputNameTeam;

    private bool trig, trig1;
    private string post_url;
    private WWW hs_get;
    [SerializeField]
    private string[] campoDatosPartida, campoPartida;

    // Start is called before the first frame update
    [ServerCallback]
    void Start()
    {
    }

    [ServerCallback]
    void Update()
    {
        if (!trig)
        {
            if (GetComponent<LeeGlobalSettings>().EsteCentro != null)
            {
                trig = true;
                _EsteCentro = GetComponent<LeeGlobalSettings>().EsteCentro;
                _EstaSala = int.Parse(GetComponent<LeeGlobalSettings>().EstaSala);
                StartCoroutine(GetRecordsSelect()); // LEE JUEGO Y PARTIDA DE B.D.
            }
        }
    }

    [Server]
    IEnumerator GetRecordsSelect()
    {
        // LEE DE LA B.D. EL ID JUEGO Game_id
        post_url = "https://vrsystem.inusualinteractive.com/modelos/recordsSelect.php?" +
            "tenant_id=" + _EsteCentro + //EL ID DEL CENTRO
            "&sala=" + _EstaSala + //SALA EN LA QUE SE ENCUENTRA EL EQUIPO
            "&game_id=" + 0 + //ID DEL JUEGO QUE HACE LA CONSULTA. SI AUN SE DESCONOCE, ESTE VALOR DEBERA SER CERO
            "&records_id=" + 0 + //ID DE LA PARTIDA A CARGAR. SI AUN SE DESCONOCE, ESTE VALOR DEBERA SER CERO
            "&user_nick=" + "" + //NICK DEL USUARIO PARA CONOCER SU EQUIPO. SI AUN SE DESCONOCE, ESTE VALOR DEBERA SER CERO.
            "&type=" + "game_id"; //EL TIPO DE CONSULTA QUE SE VA A REALIZAR (game_id, records_id, records_game, ses, capture, candy)
        hs_get = new WWW(post_url);
        yield return hs_get; //DEVOLVERA UN VALOR O UN ARRAY DE DATOS, SEGUN LA CONSULTA REALIZADA
        if (hs_get.error != null)
        {
            Debug.Log("Error al leer B.D. EL JUEGO" + hs_get.error);
            // SI NO LEE DE B.D. O NO HAY INTERNET QUE MUESTRE Y LLENE LAS ZONAS DE CAPTURA CON NOMBRES ESTANDAR
            PartidaTest = true;
            //StartCoroutine(TipoTest());
            TipoTest();
        }
        else
        {
            Debug.Log("OK B.D. EL JUEGO");
            if (hs_get.text.Trim() != "")
                JuegoBd = int.Parse(hs_get.text.Trim());
            else
            {
                Debug.Log("VACIO B.D. EL JUEGO");
                // SI LEE DE B.D. VALOR VACIO DE JUEGO QUE MUESTRE Y LLENE LAS ZONAS DE CAPTURA CON NOMBRES ESTANDAR
                PartidaTest = true;
                //StartCoroutine(TipoTest());
                TipoTest();
            }
        }

        // LEE DE LA B.D. LA ID PARTIDA Records_id
        post_url = "https://vrsystem.inusualinteractive.com/modelos/recordsSelect.php?" +
            "tenant_id=" + _EsteCentro + //EL ID DEL CENTRO
            "&sala=" + _EstaSala + //SALA EN LA QUE SE ENCUENTRA EL EQUIPO
            "&game_id=" + JuegoBd + //ID DEL JUEGO QUE HACE LA CONSULTA. SI AUN SE DESCONOCE, ESTE VALOR DEBERA SER CERO
            "&records_id=" + 0 + //ID DE LA PARTIDA A CARGAR. SI AUN SE DESCONOCE, ESTE VALOR DEBERA SER CERO
            "&user_nick=" + "" + //NICK DEL USUARIO PARA CONOCER SU EQUIPO. SI AUN SE DESCONOCE, ESTE VALOR DEBERA SER CERO.
            "&type=" + "records_id"; //EL TIPO DE CONSULTA QUE SE VA A REALIZAR (game_id, records_id, records_game, ses, capture, candy)
        hs_get = new WWW(post_url);
        yield return hs_get; //DEVOLVERA UN VALOR O UN ARRAY DE DATOS, SEGUN LA CONSULTA REALIZADA
        if (hs_get.error != null)
        {
            Debug.Log("Error al leer B.D. LA PARTIDA" + hs_get.error);
            // SI NO LEE DE B.D. O NO HAY INTERNET QUE MUESTRE Y LLENE LAS ZONAS DE CAPTURA CON NOMBRES ESTANDAR
            PartidaTest = true;
            //StartCoroutine(TipoTest());
            TipoTest();
        }
        else
        {
            Debug.Log("OK B.D. LA PARTIDA");
            if (hs_get.text.Trim() != "")
            {
                PartidaBd = hs_get.text.Trim();
                campoPartida = PartidaBd.Split("\t".ToCharArray());
                NumeroPartidaBd = int.Parse(campoPartida[0]);
                NumeroJugadoresBd = int.Parse(campoPartida[1]);
            }
            else
            {
                Debug.Log("VACIO B.D. PARTIDA");
                // SI LEE DE B.D. VALOR VACIO DE PARTIDA QUE MUESTRE Y LLENE LAS ZONAS DE CAPTURA CON NOMBRES ESTANDAR
                PartidaTest = true;
                //StartCoroutine(TipoTest());
                TipoTest();
            }
        }

        // LEE DE LA B.D. DATOS DE LA PARTIDA candy
        post_url = "https://vrsystem.inusualinteractive.com/modelos/recordsSelect.php?" +
            "tenant_id=" + _EsteCentro + //EL ID DEL CENTRO
            "&sala=" + _EstaSala + //SALA EN LA QUE SE ENCUENTRA EL EQUIPO
            "&game_id=" + JuegoBd + //ID DEL JUEGO QUE HACE LA CONSULTA. SI AUN SE DESCONOCE, ESTE VALOR DEBERA SER CERO
            "&records_id=" + NumeroPartidaBd + //ID DE LA PARTIDA A CARGAR. SI AUN SE DESCONOCE, ESTE VALOR DEBERA SER CERO
            "&user_nick=" + "" + //NICK DEL USUARIO PARA CONOCER SU EQUIPO. SI AUN SE DESCONOCE, ESTE VALOR DEBERA SER CERO.
            "&type=" + StringEsteJuego; //EL TIPO DE CONSULTA QUE SE VA A REALIZAR (game_id, records_id, records_game, ses, capture, candy)
        hs_get = new WWW(post_url);
        yield return hs_get; //DEVOLVERA UN VALOR O UN ARRAY DE DATOS, SEGUN LA CONSULTA REALIZADA
        if (hs_get.error != null)
        {
            Debug.Log("Error al leer B.D. DATOS DE PARTIDA" + hs_get.error);
            // SI NO LEE DE B.D. O NO HAY INTERNET QUE MUESTRE Y LLENE LAS ZONAS DE CAPTURA CON NOMBRES ESTANDAR
            PartidaTest = true;
            //StartCoroutine(TipoTest());
            TipoTest();
        }
        else
        {
            Debug.Log("OK B.D. DATOS DE PARTIDA");
            DatosPartidaBd = hs_get.text.Trim(); //PRUEBA
        }
        
        ///////////////////////////////////////////////////////////////////////////
        
        if (DatosPartidaBd != "" && !PartidaTest) // SI HAY DATOS EN LA PARTIDABD Y NO ES PARTIDATEST
        {
            // LLENA LAS VARIABLES CON LA LECTURA DE LA B.D.
            campoDatosPartida = DatosPartidaBd.Split("\t".ToCharArray());
            // ESTABLECE EL NOMBRE DE EQUIPO
            n_Equipo = campoDatosPartida[2];
            // ESCRIBE EL INPUT DEL NOMBRE DE EQUIPO EN UI
            //inputNameTeam.text = n_Equipo;

            // ESTABLECE LOS NOMBRES DE LOS JUGADORES Y SU EQUIPO
            System.Array.Resize(ref Pid, NumeroJugadoresBd);
            for (int i = 0; i < Pid.Length; i++)
            {
                Pid[i].n_Usuario = campoDatosPartida[i + 3];

                // LEE DE LA B.D. EQUIPOS DE USUARIOS user_team
                post_url = "https://vrsystem.inusualinteractive.com/modelos/recordsSelect.php?" +
                    "tenant_id=" + _EsteCentro + //EL ID DEL CENTRO
                    "&sala=" + _EstaSala + //SALA EN LA QUE SE ENCUENTRA EL EQUIPO
                    "&game_id=" + JuegoBd + //ID DEL JUEGO QUE HACE LA CONSULTA. SI AUN SE DESCONOCE, ESTE VALOR DEBERA SER CERO
                    "&records_id=" + NumeroPartidaBd + //ID DE LA PARTIDA A CARGAR. SI AUN SE DESCONOCE, ESTE VALOR DEBERA SER CERO
                    "&user_nick=" + Pid[i].n_Usuario + //NICK DEL USUARIO PARA CONOCER SU EQUIPO. SI AUN SE DESCONOCE, ESTE VALOR DEBERA SER CERO.
                    "&type=" + "user_team"; //EL TIPO DE CONSULTA QUE SE VA A REALIZAR (game_id, records_id, records_game, ses, capture, candy)
                hs_get = new WWW(post_url);
                yield return hs_get; //DEVOLVERA UN VALOR O UN ARRAY DE DATOS, SEGUN LA CONSULTA REALIZADA
                if (hs_get.error != null)
                {
                    Debug.Log("Error al leer B.D.EQUIPOS JUGADORES" + hs_get.error);
                    // SI NO LEE DE B.D. O NO HAY INTERNET QUE MUESTRE Y LLENE LAS ZONAS DE CAPTURA CON NOMBRES ESTANDAR
                    PartidaTest = true;
                    //StartCoroutine(TipoTest());
                    TipoTest();
                }
                else
                {
                    Debug.Log("OK B.D. EQUIPOS JUGADORES");
                    DatosEquiposJugadoresBd = hs_get.text.Trim();
                    // LLENA LAS VARIABLES CON LA LECTURA DE LA B.D.
                    Pid[i].n_Equipo = DatosEquiposJugadoresBd;
                }
            }

            // UBICACIÓN Y LLENADO DE ZONAS DE CAPTURA
            for (int i = 0; i < Pid.Length; i++)
            {
                // LLENADO DE VRIABLES DE ZONAS DE CAPTURA
                Zc[i].GetComponent<I_ZonaCaptura>().n_Usuario = Pid[i].n_Usuario;
                Zc[i].GetComponent<I_ZonaCaptura>().n_Equipo = Pid[i].n_Equipo;
                // UBICACIÓN DE ZONAS DE CAPTURA
                Zc[i].SetActive(true);
                Zc[i].transform.position = Uzc[i].position;
                Zc[i].transform.rotation = Uzc[i].rotation;
                RpcSetActive(Zc[i], Zc[i].transform.position, Zc[i].transform.rotation, true);
            }
        }
        else
        {
            Debug.Log("VACIO B.D. DATOS PARTIDA");
            // SI LEE DE B.D. VALOR VACIO DE PARTIDA QUE MUESTRE Y LLENE LAS ZONAS DE CAPTURA CON NOMBRES ESTANDAR
            PartidaTest = true;
            //StartCoroutine(TipoTest());
            TipoTest();
        }
        yield return new WaitForSeconds(5);
        StartCoroutine(GetRecordsSelect());
    }

    [Server]
    //IEnumerator TipoTest()
    private void TipoTest()
    {
        Debug.Log("PARTIDA TIPO TEST");
        MsjDesconectado.SetActive(true);  // QUE MUESTRE MSJ
        if (!trig1) // QUE AUMENTE EL CONTADOR
        {
            trig1 = true;
            N_Desconectado = PlayerPrefs.GetInt("N_Desconectado", 0);
            N_Desconectado++;
            PlayerPrefs.SetInt("N_Desconectado", N_Desconectado);
            Text_N_Desconectado.text = N_Desconectado.ToString();
        }
        // ESCRIBE LO INPUTS DE LOS NOMBRES DE EQUIPO EN UI
        n_Equipo = "VrTeam";
        // LLENADO DE VRIABLES DE ZONAS DE CAPTURA
        /*Zc[0].GetComponent<I_ZonaCaptura>().n_Usuario = "Player 1";
        Zc[1].GetComponent<I_ZonaCaptura>().n_Usuario = "Player 2";
        Zc[2].GetComponent<I_ZonaCaptura>().n_Usuario = "Player 3";
        Zc[3].GetComponent<I_ZonaCaptura>().n_Usuario = "Player 4";
        Zc[4].GetComponent<I_ZonaCaptura>().n_Usuario = "Player 5";
        Zc[5].GetComponent<I_ZonaCaptura>().n_Usuario = "Player 6";*/
        Zc[0].GetComponent<I_ZonaCaptura>().n_Equipo = "VrTeam";
        Zc[1].GetComponent<I_ZonaCaptura>().n_Equipo = "VrTeam";
        Zc[2].GetComponent<I_ZonaCaptura>().n_Equipo = "VrTeam";
        Zc[3].GetComponent<I_ZonaCaptura>().n_Equipo = "VrTeam";
        Zc[4].GetComponent<I_ZonaCaptura>().n_Equipo = "VrTeam";
        Zc[5].GetComponent<I_ZonaCaptura>().n_Equipo = "VrTeam";
        for (int i = 0; i < 6; i++)
        {
            Zc[i].SetActive(true);
            Zc[i].transform.position = Uzc[i].position;
            Zc[i].transform.rotation = Uzc[i].rotation;
            //RpcSetActive(Zc[i], Zc[i].transform.position, Zc[i].transform.rotation, true);
        }
        RpcSetActive2();
        //yield return new WaitForSeconds(5);
        //StartCoroutine(TipoTest()); 
    }

    [ClientRpc]
    void RpcSetActive2()
    {
        /*Zc[0].GetComponent<I_ZonaCaptura>().n_Usuario = "Player 1";
        Zc[1].GetComponent<I_ZonaCaptura>().n_Usuario = "Player 2";
        Zc[2].GetComponent<I_ZonaCaptura>().n_Usuario = "Player 3";
        Zc[3].GetComponent<I_ZonaCaptura>().n_Usuario = "Player 4";
        Zc[4].GetComponent<I_ZonaCaptura>().n_Usuario = "Player 5";
        Zc[5].GetComponent<I_ZonaCaptura>().n_Usuario = "Player 6";*/
        Zc[0].GetComponent<I_ZonaCaptura>().n_Equipo = "VrTeam";
        Zc[1].GetComponent<I_ZonaCaptura>().n_Equipo = "VrTeam";
        Zc[2].GetComponent<I_ZonaCaptura>().n_Equipo = "VrTeam";
        Zc[3].GetComponent<I_ZonaCaptura>().n_Equipo = "VrTeam";
        Zc[4].GetComponent<I_ZonaCaptura>().n_Equipo = "VrTeam";
        Zc[5].GetComponent<I_ZonaCaptura>().n_Equipo = "VrTeam";
        for (int i = 0; i < 6; i++)
        {
            Zc[i].SetActive(true);
            Zc[i].transform.position = Uzc[i].position;
            Zc[i].transform.rotation = Uzc[i].rotation;
        }
    }

    [ClientRpc]
    void RpcSetActive(GameObject ZonaCap, Vector3 pos, Quaternion rot, bool act)
    {
        ZonaCap.transform.position = pos;
        ZonaCap.transform.rotation = rot;
        ZonaCap.SetActive(act);
    }

    [Server]
    public void ResetDesconectado()
    {
        if (Text_N_Desconectado.text == _EsteCentro + _EsteCentro)
        {
            N_Desconectado = 0;
            PlayerPrefs.SetInt("N_Desconectado", N_Desconectado);
            Text_N_Desconectado.text = N_Desconectado.ToString();
        }
    }

}
