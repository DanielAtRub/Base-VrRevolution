using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror; // ES NECESARIO?

public class DatosJugadores : NetworkBehaviour
{
    [System.Serializable]
    public class PlayerId
    {
        public string nombre, equipo;
        public int id, puntos, nDisparo, matados;
        public bool PidExistio, PidExiste, Avatar;
    }

    [Header("ESTADO PLAYERS")]
    public bool EnJuego;
    [SerializeField]
    private GameObject Gamemanager;
    [SerializeField]
    private GameObject[] Players;
    [SerializeField]
    private PlayerId Pid1, Pid2, Pid3, Pid4, Pid5, Pid6;
    [SerializeField]
    private bool trig1, trig2, trig3, trig4, trig5, trig6;

    [Header("ESTADO JUEGO")]
    [SerializeField]
    private GameObject L0;
    [SerializeField]
    private GameObject L1, L2;
    [Header("          ACTIVA TREN")]
    [SerializeField]
    private GameObject PlataformaTren;
    //[SerializeField]
    //private GameObject T1, T2, T3, T4;
    [Header("ORDAS Y OTROS")]
    [SerializeField]
    private GameObject Orda1Manager;
    [SerializeField]
    private GameObject Orda1_2Manager, Orda1_3Manager;
    [SerializeField]
    private GameObject Orda4Manager, Orda4_2Manager, Orda4_3Manager;
    [SerializeField]
    private GameObject Orda4TrenManager;
    [SerializeField]
    private GameObject Orda4_2TrenManager, Orda4_3TrenManager;
    [Header("UBICACION")]
    [SerializeField]
    private GameObject ZonaUnicacionL1, ZonaUnicacionL2;

    // Start is called before the first frame update
    void Start()
    {
    }

    [Server]
    public void j() // LLAMADO DESDE EL BOTON JUGAR
    {
        EnJuego = true;
    }

    [ServerCallback]
    void Update()  // SIEMPRE UPDATE PARA QUE MANTENGA ACTUALIZADO LOS VALORES DE LOD JUGADORES POR SI CRASH
    {
        if (EnJuego)
        {
            Players = GameObject.FindGameObjectsWithTag("Jugador");

            Pid1.PidExiste = false;
            Pid2.PidExiste = false;
            Pid3.PidExiste = false;
            Pid4.PidExiste = false;
            Pid5.PidExiste = false;
            Pid6.PidExiste = false;

            for (int i = 0; i < Players.Length; i++)
            {
                if (Players[i].GetComponent<IdGetVisor>().idGafas == 1)
                {
                    if (!Pid1.PidExiste)
                    {
                        Pid1.PidExistio = true;
                        Pid1.PidExiste = true;
                        if (trig1)  // HACE QUE SOLO SETDATOS SI NO EXISTE EL PLAYER
                        {
                            SetDatos(Players[i], 1);
                            trig1 = false;
                        }
                    }
                    //ACTUALIZA DATOS
                    Pid1.id = 1;
                    Pid1.puntos = Players[i].GetComponent<Player>().PlayerPuntos;
                    Pid1.nombre = Players[i].GetComponent<Player>().Name;
                    Pid1.equipo = Players[i].GetComponent<Player>().Team;
                    Pid1.matados = Players[i].GetComponent<Player>().EnemigosMatados;
                    //Pid1.Avatar = Players[i].GetComponent<Player>().Avatar;
                }
                if (Players[i].GetComponent<IdGetVisor>().idGafas == 2)
                {
                    if (!Pid2.PidExiste)
                    {
                        Pid2.PidExistio = true;
                        Pid2.PidExiste = true;
                        if (trig2)
                        {
                            SetDatos(Players[i], 2);
                            trig2 = false;
                        }
                    }
                    //ACTUALIZA DATOS
                    Pid2.id = 2;
                    Pid2.puntos = Players[i].GetComponent<Player>().PlayerPuntos;
                    Pid2.nombre = Players[i].GetComponent<Player>().Name;
                    Pid2.equipo = Players[i].GetComponent<Player>().Team;
                    Pid2.matados = Players[i].GetComponent<Player>().EnemigosMatados;
                    //Pid2.Avatar = Players[i].GetComponent<Player>().Avatar;
                }
                if (Players[i].GetComponent<IdGetVisor>().idGafas == 3)
                {
                    if (!Pid3.PidExiste)
                    {
                        Pid3.PidExistio = true;
                        Pid3.PidExiste = true;
                        if (trig3)
                        {
                            SetDatos(Players[i], 3);
                            trig3 = false;
                        }
                    }
                    //ACTUALIZA DATOS
                    Pid3.id = 3;
                    Pid3.puntos = Players[i].GetComponent<Player>().PlayerPuntos;
                    Pid3.nombre = Players[i].GetComponent<Player>().Name;
                    Pid3.equipo = Players[i].GetComponent<Player>().Team;
                    Pid3.matados = Players[i].GetComponent<Player>().EnemigosMatados;
                    //Pid3.Avatar = Players[i].GetComponent<Player>().Avatar;
                }
                if (Players[i].GetComponent<IdGetVisor>().idGafas == 4)
                {
                    if (!Pid4.PidExiste)
                    {
                        Pid4.PidExistio = true;
                        Pid4.PidExiste = true;
                        if (trig4)
                        {
                            SetDatos(Players[i], 4);
                            trig4 = false;
                        }
                    }
                    //ACTUALIZA DATOS
                    Pid4.id = 4;
                    Pid4.puntos = Players[i].GetComponent<Player>().PlayerPuntos;
                    Pid4.nombre = Players[i].GetComponent<Player>().Name;
                    Pid4.equipo = Players[i].GetComponent<Player>().Team;
                    Pid4.matados = Players[i].GetComponent<Player>().EnemigosMatados;
                    //Pid4.Avatar = Players[i].GetComponent<Player>().Avatar;
                }
                if (Players[i].GetComponent<IdGetVisor>().idGafas == 5)
                {
                    if (!Pid5.PidExiste)
                    {
                        Pid5.PidExistio = true;
                        Pid5.PidExiste = true;
                        if (trig5)
                        {
                            SetDatos(Players[i], 5);
                            trig5 = false;
                        }
                    }
                    //ACTUALIZA DATOS
                    Pid5.id = 5;
                    Pid5.puntos = Players[i].GetComponent<Player>().PlayerPuntos;
                    Pid5.nombre = Players[i].GetComponent<Player>().Name;
                    Pid5.equipo = Players[i].GetComponent<Player>().Team;
                    Pid5.matados = Players[i].GetComponent<Player>().EnemigosMatados;
                    //Pid5.Avatar = Players[i].GetComponent<Player>().Avatar;
                }
                if (Players[i].GetComponent<IdGetVisor>().idGafas == 6)
                {
                    if (!Pid6.PidExiste)
                    {
                        Pid6.PidExistio = true;
                        Pid6.PidExiste = true;
                        if (trig6)
                        {
                            SetDatos(Players[i], 6);
                            trig6 = false;
                        }
                    }
                    //ACTUALIZA DATOS
                    Pid6.id = 6;
                    Pid6.puntos = Players[i].GetComponent<Player>().PlayerPuntos;
                    Pid6.nombre = Players[i].GetComponent<Player>().Name;
                    Pid6.equipo = Players[i].GetComponent<Player>().Team;
                    Pid6.matados = Players[i].GetComponent<Player>().EnemigosMatados;
                    //Pid6.Avatar = Players[i].GetComponent<Player>().Avatar;
                }
            }

            if (!Pid1.PidExiste)
                trig1 = true;
            if (!Pid2.PidExiste)
                trig2 = true;
            if (!Pid3.PidExiste)
                trig3 = true;
            if (!Pid4.PidExiste)
                trig4 = true;
            if (!Pid5.PidExiste)
                trig5 = true;
            if (!Pid6.PidExiste)
                trig6 = true;
        }
    }

    //ESCRIBE DATOS
    [Server]
    private void SetDatos(GameObject P, int n)
    {
        if (n == 1)
        {
            P.GetComponent<Player>().PlayerPuntos = Pid1.puntos;
            P.GetComponent<Player>().Name = Pid1.nombre;
            P.GetComponent<Player>().Team = Pid1.equipo;
            P.GetComponent<Player>().EnemigosMatados = Pid1.matados;
            //P.GetComponent<Player>().Avatar = Pid1.Avatar;
            //P.GetComponent<Player>().StartAvatar(Pid1.Avatar); // PRUEBA
            ReentradaPlayer(P);
        }
        if (n == 2)
        {
            P.GetComponent<Player>().PlayerPuntos = Pid2.puntos;
            P.GetComponent<Player>().Name = Pid2.nombre;
            P.GetComponent<Player>().Team = Pid2.equipo;
            P.GetComponent<Player>().EnemigosMatados = Pid2.matados;
            //P.GetComponent<Player>().Avatar = Pid2.Avatar;
            //P.GetComponent<Player>().StartAvatar(Pid2.Avatar); // PRUEBA
            ReentradaPlayer(P);
        }
        if (n == 3)
        {
            P.GetComponent<Player>().PlayerPuntos = Pid3.puntos;
            P.GetComponent<Player>().Name = Pid3.nombre;
            P.GetComponent<Player>().Team = Pid3.equipo;
            P.GetComponent<Player>().EnemigosMatados = Pid3.matados;
            //P.GetComponent<Player>().Avatar = Pid3.Avatar;
            //P.GetComponent<Player>().StartAvatar(Pid3.Avatar); // PRUEBA
            ReentradaPlayer(P);
        }
        if (n == 4)
        {
            P.GetComponent<Player>().PlayerPuntos = Pid4.puntos;
            P.GetComponent<Player>().Name = Pid4.nombre;
            P.GetComponent<Player>().Team = Pid4.equipo;
            P.GetComponent<Player>().EnemigosMatados = Pid4.matados;
            //P.GetComponent<Player>().Avatar = Pid4.Avatar;
            //P.GetComponent<Player>().StartAvatar(Pid4.Avatar); // PRUEBA
            ReentradaPlayer(P);
        }
        if (n == 5)
        {
            P.GetComponent<Player>().PlayerPuntos = Pid5.puntos;
            P.GetComponent<Player>().Name = Pid5.nombre;
            P.GetComponent<Player>().Team = Pid5.equipo;
            P.GetComponent<Player>().EnemigosMatados = Pid5.matados;
            //P.GetComponent<Player>().Avatar = Pid5.Avatar;
            //P.GetComponent<Player>().StartAvatar(Pid5.Avatar); // PRUEBA
            ReentradaPlayer(P);
        }
        if (n == 6)
        {
            P.GetComponent<Player>().PlayerPuntos = Pid6.puntos;
            P.GetComponent<Player>().Name = Pid6.nombre;
            P.GetComponent<Player>().Team = Pid6.equipo;
            P.GetComponent<Player>().EnemigosMatados = Pid6.matados;
            //P.GetComponent<Player>().Avatar = Pid6.Avatar;
            //P.GetComponent<Player>().StartAvatar(Pid6.Avatar); // PRUEBA
            ReentradaPlayer(P);
        }
    }

    //REACTUALIZA LAS COSAS DEL JUEGO CON AL CLIENTE
    [Server]
    public void ReentradaPlayer(GameObject p)
    {
        //DES/ACT LEVELS
        if (Gamemanager.GetComponent<JuegoManager>().ActLevel1)
            RpcL1(p.GetComponent<NetworkIdentity>().connectionToClient);
        if (Gamemanager.GetComponent<JuegoManager>().ActLevel2)
            RpcL2(p.GetComponent<NetworkIdentity>().connectionToClient);
        //DES/ACT UBICACIONES
        if (Gamemanager.GetComponent<JuegoManager>().ActUbicacionL1)
            RpcUbicaionL1(p.GetComponent<NetworkIdentity>().connectionToClient);
        //ACTIVA TREN
        //T1.GetComponent<PathCreation.Examples.PathFollower>().enabled = true;
        //T2.GetComponent<PathCreation.Examples.PathFollower>().enabled = true;
        //T3.GetComponent<PathCreation.Examples.PathFollower>().enabled = true;
        //T4.GetComponent<PathCreation.Examples.PathFollower>().enabled = true;
        //ORDAS L1
        //if (Gamemanager.GetComponent<JuegoManager>().ActOrda1)
        //    RpcOrda1(p.GetComponent<NetworkIdentity>().connectionToClient);
        //ORDAS L2
        //if (Gamemanager.GetComponent<JuegoManager>().ActOrda4)
        //    RpcOrda4(p.GetComponent<NetworkIdentity>().connectionToClient);
    }

    //MUESTRA LEVEL 1
    [TargetRpc]
    void RpcL1(NetworkConnection target)
    {
        L0.SetActive(false);
        L1.SetActive(true);
        L2.SetActive(false);
        PlataformaTren.SetActive(false);
    }
    //ORDAS L1
    [TargetRpc]
    void RpcOrda1(NetworkConnection target)
    {
        if (Players.Length == 5 || Players.Length == 6) // PARA 5 o 6 JUGADORES
        {
            Orda1Manager.SetActive(true);
        }
        if (Players.Length == 3 || Players.Length == 4) // PARA 3 o 4 JUGADORES
        {
            Orda1_2Manager.SetActive(true);
        }
        if (Players.Length == 1 || Players.Length == 2) // PARA 1 o 2 JUGADORES
        {
            Orda1_3Manager.SetActive(true);
        }
    }

    //UBICAION L1
    [TargetRpc]
    void RpcUbicaionL1(NetworkConnection target)
    {
        ZonaUnicacionL1.SetActive(true);
    }

    //MUESTRA LEVEL 2
    [TargetRpc]
    void RpcL2(NetworkConnection target)
    {
        L0.SetActive(false);
        L1.SetActive(false);
        L2.SetActive(true);
        PlataformaTren.SetActive(true);
    }
    //ORDAS L2
    [TargetRpc]
    void RpcOrda4(NetworkConnection target)
    {
        if (Players.Length == 5 || Players.Length == 6) // PARA 5 o 6 JUGADORES
        {
            Orda4Manager.SetActive(true);
            Orda4TrenManager.SetActive(true);
        }
        if (Players.Length == 3 || Players.Length == 4) // PARA 3 o 4 JUGADORES
        {
            Orda4_2Manager.SetActive(true);
            Orda4_2TrenManager.SetActive(true);
        }
        if (Players.Length == 1 || Players.Length == 2) // PARA 1 o 2 JUGADORES
        {
            Orda4_3Manager.SetActive(true);
            Orda4_3TrenManager.SetActive(true);
        }
    }
    //UBICAION L2
    [TargetRpc]
    void RpcUbicaionL2(NetworkConnection target)
    {
        ZonaUnicacionL2.SetActive(true);
    }
}
