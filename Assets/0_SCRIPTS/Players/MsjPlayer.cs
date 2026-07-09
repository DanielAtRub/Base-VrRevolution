using UnityEngine;
using Mirror;
using TMPro;

public class MsjPlayer : NetworkBehaviour
{
    public GameObject GameManager;

    [SerializeField]
    private GameObject videoContinuara;
    [Header("MSJS")]
    public int msj;
    [SerializeField]
    private GameObject[] msjs;
    [SerializeField]
    private bool msjMostrado1, msjMostrado2, msjMostrado3, msjMostrado4, msjMostrado5, msjMostrado6,
        msjMostrado7, msjMostrado8, msjMostrado9, msjMostrado10, msjMostrado11, msjMostrado12, msjMostrado13, 
        msjMostrado14, msjMostrado15, msjMostrado16, msjMostrado17;
    [SerializeField]
    private GameObject ScoreCliente;
    /*[Header("A NEGRO - ENTRE LEVELS")]
    [SerializeField]
    private Camera camara;
    [SerializeField]
    private LayerMask mascaraMINIMO;*/
    /*[Header("INDICADORES EXPLOSIVOS")]
    [SerializeField]
    private TextMeshProUGUI textExplosivosAct;*/

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // for local player
        if (!isLocalPlayer) return;

        // COLOCADO AQUI PARA APROVECHAR EL FIND
        if (!GameManager)
            GameManager = GameObject.Find("GameManager");
        //textExplosivosAct.text = GameManager.GetComponent<JuegoManager>().ExplosivosAct.ToString(); 

        if (GameManager.GetComponent<JuegoManager>().ActContinuara)
        {
            videoContinuara.SetActive(true);
        }


        msj = GameManager.GetComponent<JuegoManager>().MsjServerToPlayers;

        //MENSAJES AL PULSAR GATILLO
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger) || OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
        {
            GameObject NivelActual = GameObject.FindGameObjectWithTag("Nivel");
            /*if (NivelActual.name == "LEVEL 0")
            {
                msjs[2].GetComponent<MsjAutoDes>().enabled = false;
                msjs[2].SetActive(true);
            }*/
            if (NivelActual.name == "LEVEL 1")
            {
                msjs[3].GetComponent<MsjAutoDes>().enabled = false;
                msjs[3].SetActive(true);
            }
            if (NivelActual.name == "LEVEL 2")
            {
                msjs[4].GetComponent<MsjAutoDes>().enabled = false;
                msjs[4].SetActive(true);
            }
            if (NivelActual.name == "LEVEL 3")
            {
                msjs[5].GetComponent<MsjAutoDes>().enabled = false;
                msjs[5].SetActive(true);
            }
            if (NivelActual.name == "LEVEL 4 ida y vuelta")
            {
                msjs[6].GetComponent<MsjAutoDes>().enabled = false;
                msjs[6].SetActive(true);
            }
            if (NivelActual.name == "LEVEL 5 ida y vuelta")
            {
                msjs[7].GetComponent<MsjAutoDes>().enabled = false;
                msjs[7].SetActive(true);
            }
            if (NivelActual.name == "LEVEL 6 ida y vuelta")
            {
                msjs[8].GetComponent<MsjAutoDes>().enabled = false;
                msjs[8].SetActive(true);
            }
            if (NivelActual.name == "LEVEL 7 ida")
            {
                msjs[9].GetComponent<MsjAutoDes>().enabled = false;
                msjs[9].SetActive(true);
            }
            if (NivelActual.name == "LEVEL 8")
            {
                msjs[10].GetComponent<MsjAutoDes>().enabled = false;
                msjs[10].SetActive(true);
            }
            if (NivelActual.name == "LEVEL 9 ida")
            {
                msjs[11].GetComponent<MsjAutoDes>().enabled = false;
                msjs[11].SetActive(true);
            }
            if (NivelActual.name == "LEVEL 10")
            {
                msjs[12].GetComponent<MsjAutoDes>().enabled = false;
                msjs[12].SetActive(true);
            }
            if (NivelActual.name == "LEVEL 11")
            {
                msjs[13].GetComponent<MsjAutoDes>().enabled = false;
                msjs[13].SetActive(true);
            }
            if (NivelActual.name == "LEVEL 12")
            {
                msjs[14].GetComponent<MsjAutoDes>().enabled = false;
                msjs[14].SetActive(true);
            }
        }
        if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger) || OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger))
        {
            //msjs[2].SetActive(false);
            msjs[3].SetActive(false);
            msjs[4].SetActive(false);
            msjs[5].SetActive(false);
            msjs[6].SetActive(false);
            msjs[7].SetActive(false);
            msjs[8].SetActive(false);
            msjs[9].SetActive(false);
            msjs[10].SetActive(false);
            msjs[11].SetActive(false);
            msjs[12].SetActive(false);
            msjs[13].SetActive(false);
            msjs[14].SetActive(false);
        }
        //MENSAJES AL PULSAR GATILLO

        if (msj == 1 && !msjMostrado1) //ELIGE JUGADOR
        {
            msjMostrado1 = true;
            msjMostrado2 = false;
            msjMostrado3 = false;
            msjMostrado4 = false;
            msjMostrado5 = false;
            msjMostrado6 = false;
            msjMostrado7 = false;
            msjMostrado8 = false;
            msjMostrado9 = false;
            msjMostrado10 = false;
            msjMostrado11 = false;
            msjMostrado12 = false;
            msjMostrado13 = false;
            msjMostrado14 = false;
            msjMostrado15 = false;
            msjMostrado16 = false;
            msjMostrado17 = false;

            msjs[0].SetActive(true);
            msjs[1].SetActive(false);
            msjs[2].SetActive(false);
            msjs[3].SetActive(false);
            msjs[4].SetActive(false);
            msjs[5].SetActive(false);
            msjs[6].SetActive(false);
            msjs[7].SetActive(false);
            msjs[8].SetActive(false);
            msjs[9].SetActive(false);
            msjs[10].SetActive(false);
            msjs[11].SetActive(false);
            msjs[12].SetActive(false);
            msjs[13].SetActive(false);
            msjs[14].SetActive(false);
            msjs[15].SetActive(false);
            msjs[16].SetActive(false);
        }
        if (msj == 2 && !msjMostrado2) //REUBICACION
        {
            msjMostrado1 = false;
            msjMostrado2 = true;
            msjMostrado3 = false;
            msjMostrado4 = false;
            msjMostrado5 = false;
            msjMostrado6 = false;
            msjMostrado7 = false;
            msjMostrado8 = false;
            msjMostrado9 = false;
            msjMostrado10 = false;
            msjMostrado11 = false;
            msjMostrado12 = false;
            msjMostrado13 = false;
            msjMostrado14 = false;
            msjMostrado15 = false;
            msjMostrado16 = false;
            msjMostrado17 = false;

            msjs[0].SetActive(false);
            msjs[1].SetActive(true);
            msjs[2].SetActive(false);
            msjs[3].SetActive(false);
            msjs[4].SetActive(false);
            msjs[5].SetActive(false);
            msjs[6].SetActive(false);
            msjs[7].SetActive(false);
            msjs[8].SetActive(false);
            msjs[9].SetActive(false);
            msjs[10].SetActive(false);
            msjs[11].SetActive(false);
            msjs[12].SetActive(false);
            msjs[13].SetActive(false);
            msjs[14].SetActive(false);
            msjs[15].SetActive(false);
            msjs[16].SetActive(false);
        }
        if (msj == 3 && !msjMostrado3) //LA MISION
        {
            msjMostrado1 = false;
            msjMostrado2 = false;
            msjMostrado3 = true;
            msjMostrado4 = false;
            msjMostrado5 = false;
            msjMostrado6 = false;
            msjMostrado7 = false;
            msjMostrado8 = false;
            msjMostrado9 = false;
            msjMostrado10 = false;
            msjMostrado11 = false;
            msjMostrado12 = false;
            msjMostrado13 = false;
            msjMostrado14 = false;
            msjMostrado15 = false;
            msjMostrado16 = false;
            msjMostrado17 = false;

            msjs[0].SetActive(false);
            msjs[1].SetActive(false);
            msjs[2].SetActive(true);
            msjs[3].SetActive(false);
            msjs[4].SetActive(false);
            msjs[5].SetActive(false);
            msjs[6].SetActive(false);
            msjs[7].SetActive(false);
            msjs[8].SetActive(false);
            msjs[9].SetActive(false);
            msjs[10].SetActive(false);
            msjs[11].SetActive(false);
            msjs[12].SetActive(false);
            msjs[13].SetActive(false);
            msjs[14].SetActive(false);
            msjs[15].SetActive(false);
            msjs[16].SetActive(false);
        }
        if (msj == 4 && !msjMostrado4) //L1
        {
            msjMostrado1 = false;
            msjMostrado2 = false;
            msjMostrado3 = false;
            msjMostrado4 = true;
            msjMostrado5 = false;
            msjMostrado6 = false;
            msjMostrado7 = false;
            msjMostrado8 = false;
            msjMostrado9 = false;
            msjMostrado10 = false;
            msjMostrado11 = false;
            msjMostrado12 = false;
            msjMostrado13 = false;
            msjMostrado14 = false;
            msjMostrado15 = false;
            msjMostrado16 = false;
            msjMostrado17 = false;

            msjs[0].SetActive(false);
            msjs[1].SetActive(false);
            msjs[2].SetActive(false);
            msjs[3].SetActive(true);
            msjs[4].SetActive(false);
            msjs[5].SetActive(false);
            msjs[6].SetActive(false);
            msjs[7].SetActive(false);
            msjs[8].SetActive(false);
            msjs[9].SetActive(false);
            msjs[10].SetActive(false);
            msjs[11].SetActive(false);
            msjs[12].SetActive(false);
            msjs[13].SetActive(false);
            msjs[14].SetActive(false);
            msjs[15].SetActive(false);
            msjs[16].SetActive(false);
        }
        if (msj == 5 && !msjMostrado5) //L2
        {
            msjMostrado1 = false;
            msjMostrado2 = false;
            msjMostrado3 = false;
            msjMostrado4 = false;
            msjMostrado5 = true;
            msjMostrado6 = false;
            msjMostrado7 = false;
            msjMostrado8 = false;
            msjMostrado9 = false;
            msjMostrado10 = false;
            msjMostrado11 = false;
            msjMostrado12 = false;
            msjMostrado13 = false;
            msjMostrado14 = false;
            msjMostrado15 = false;
            msjMostrado16 = false;
            msjMostrado17 = false;

            msjs[0].SetActive(false);
            msjs[1].SetActive(false);
            msjs[2].SetActive(false);
            msjs[3].SetActive(false);
            msjs[4].SetActive(true);
            msjs[5].SetActive(false);
            msjs[6].SetActive(false);
            msjs[7].SetActive(false);
            msjs[8].SetActive(false);
            msjs[9].SetActive(false);
            msjs[10].SetActive(false);
            msjs[11].SetActive(false);
            msjs[12].SetActive(false);
            msjs[13].SetActive(false);
            msjs[14].SetActive(false);
            msjs[15].SetActive(false);
            msjs[16].SetActive(false);
        }
        if (msj == 6 && !msjMostrado6) //L3
        {
            msjMostrado1 = false;
            msjMostrado2 = false;
            msjMostrado3 = false;
            msjMostrado4 = false;
            msjMostrado5 = false;
            msjMostrado6 = true;
            msjMostrado7 = false;
            msjMostrado8 = false;
            msjMostrado9 = false;
            msjMostrado10 = false;
            msjMostrado11 = false;
            msjMostrado12 = false;
            msjMostrado13 = false;
            msjMostrado14 = false;
            msjMostrado15 = false;
            msjMostrado16 = false;
            msjMostrado17 = false;

            msjs[0].SetActive(false);
            msjs[1].SetActive(false);
            msjs[2].SetActive(false);
            msjs[3].SetActive(false);
            msjs[4].SetActive(false);
            msjs[5].SetActive(true);
            msjs[6].SetActive(false);
            msjs[7].SetActive(false);
            msjs[8].SetActive(false);
            msjs[9].SetActive(false);
            msjs[10].SetActive(false);
            msjs[11].SetActive(false);
            msjs[12].SetActive(false);
            msjs[13].SetActive(false);
            msjs[14].SetActive(false);
            msjs[15].SetActive(false);
            msjs[16].SetActive(false);
        }
        if (msj == 7 && !msjMostrado7) //L4
        {
            msjMostrado1 = false;
            msjMostrado2 = false;
            msjMostrado3 = false;
            msjMostrado4 = false;
            msjMostrado5 = false;
            msjMostrado6 = false;
            msjMostrado7 = true;
            msjMostrado8 = false;
            msjMostrado9 = false;
            msjMostrado10 = false;
            msjMostrado11 = false;
            msjMostrado12 = false;
            msjMostrado13 = false;
            msjMostrado14 = false;
            msjMostrado15 = false;
            msjMostrado16 = false;
            msjMostrado17 = false;

            msjs[0].SetActive(false);
            msjs[1].SetActive(false);
            msjs[2].SetActive(false);
            msjs[3].SetActive(false);
            msjs[4].SetActive(false);
            msjs[5].SetActive(false);
            msjs[6].SetActive(true);
            msjs[7].SetActive(false);
            msjs[8].SetActive(false);
            msjs[9].SetActive(false);
            msjs[10].SetActive(false);
            msjs[11].SetActive(false);
            msjs[12].SetActive(false);
            msjs[13].SetActive(false);
            msjs[14].SetActive(false);
            msjs[15].SetActive(false);
            msjs[16].SetActive(false);
        }
        if (msj == 8 && !msjMostrado8) //L5
        {
            msjMostrado1 = false;
            msjMostrado2 = false;
            msjMostrado3 = false;
            msjMostrado4 = false;
            msjMostrado5 = false;
            msjMostrado6 = false;
            msjMostrado7 = false;
            msjMostrado8 = true;
            msjMostrado9 = false;
            msjMostrado10 = false;
            msjMostrado11 = false;
            msjMostrado12 = false;
            msjMostrado13 = false;
            msjMostrado14 = false;
            msjMostrado15 = false;
            msjMostrado16 = false;
            msjMostrado17 = false;

            msjs[0].SetActive(false);
            msjs[1].SetActive(false);
            msjs[2].SetActive(false);
            msjs[3].SetActive(false);
            msjs[4].SetActive(false);
            msjs[5].SetActive(false);
            msjs[6].SetActive(false);
            msjs[7].SetActive(true);
            msjs[8].SetActive(false);
            msjs[9].SetActive(false);
            msjs[10].SetActive(false);
            msjs[11].SetActive(false);
            msjs[12].SetActive(false);
            msjs[13].SetActive(false);
            msjs[14].SetActive(false);
            msjs[15].SetActive(false);
            msjs[16].SetActive(false);
        }
        if (msj == 9 && !msjMostrado9) //L6
        {
            msjMostrado1 = false;
            msjMostrado2 = false;
            msjMostrado3 = false;
            msjMostrado4 = false;
            msjMostrado5 = false;
            msjMostrado6 = false;
            msjMostrado7 = false;
            msjMostrado8 = false;
            msjMostrado9 = true;
            msjMostrado10 = false;
            msjMostrado11 = false;
            msjMostrado12 = false;
            msjMostrado13 = false;
            msjMostrado14 = false;
            msjMostrado15 = false;
            msjMostrado16 = false;
            msjMostrado17 = false;

            msjs[0].SetActive(false);
            msjs[1].SetActive(false);
            msjs[2].SetActive(false);
            msjs[3].SetActive(false);
            msjs[4].SetActive(false);
            msjs[5].SetActive(false);
            msjs[6].SetActive(false);
            msjs[7].SetActive(false);
            msjs[8].SetActive(true);
            msjs[9].SetActive(false);
            msjs[10].SetActive(false);
            msjs[11].SetActive(false);
            msjs[12].SetActive(false);
            msjs[13].SetActive(false);
            msjs[14].SetActive(false);
            msjs[15].SetActive(false);
            msjs[16].SetActive(false);
        }
        if (msj == 10 && !msjMostrado10) //L7
        {
            msjMostrado1 = false;
            msjMostrado2 = false;
            msjMostrado3 = false;
            msjMostrado4 = false;
            msjMostrado5 = false;
            msjMostrado6 = false;
            msjMostrado7 = false;
            msjMostrado8 = false;
            msjMostrado9 = false;
            msjMostrado10 = true;
            msjMostrado11 = false;
            msjMostrado12 = false;
            msjMostrado13 = false;
            msjMostrado14 = false;
            msjMostrado15 = false;
            msjMostrado16 = false;
            msjMostrado17 = false;

            msjs[0].SetActive(false);
            msjs[1].SetActive(false);
            msjs[2].SetActive(false);
            msjs[3].SetActive(false);
            msjs[4].SetActive(false);
            msjs[5].SetActive(false);
            msjs[6].SetActive(false);
            msjs[7].SetActive(false);
            msjs[8].SetActive(false);
            msjs[9].SetActive(true);
            msjs[10].SetActive(false);
            msjs[11].SetActive(false);
            msjs[12].SetActive(false);
            msjs[13].SetActive(false);
            msjs[14].SetActive(false);
            msjs[15].SetActive(false);
            msjs[16].SetActive(false);
        }
        if (msj == 11 && !msjMostrado11) ////L8
        {
            msjMostrado1 = false;
            msjMostrado2 = false;
            msjMostrado3 = false;
            msjMostrado4 = false;
            msjMostrado5 = false;
            msjMostrado6 = false;
            msjMostrado7 = false;
            msjMostrado8 = false;
            msjMostrado9 = false;
            msjMostrado10 = false;
            msjMostrado11 = true;
            msjMostrado12 = false;
            msjMostrado13 = false;
            msjMostrado14 = false;
            msjMostrado15 = false;
            msjMostrado16 = false;
            msjMostrado17 = false;

            msjs[0].SetActive(false);
            msjs[1].SetActive(false);
            msjs[2].SetActive(false);
            msjs[3].SetActive(false);
            msjs[4].SetActive(false);
            msjs[5].SetActive(false);
            msjs[6].SetActive(false);
            msjs[7].SetActive(false);
            msjs[8].SetActive(false);
            msjs[9].SetActive(false);
            msjs[10].SetActive(true);
            msjs[11].SetActive(false);
            msjs[12].SetActive(false);
            msjs[13].SetActive(false);
            msjs[14].SetActive(false);
            msjs[15].SetActive(false);
            msjs[16].SetActive(false);

            Invoke("showScore", 20.0f);
        }
        if (msj == 12 && !msjMostrado12) //L9
        {
            msjMostrado1 = false;
            msjMostrado2 = false;
            msjMostrado3 = false;
            msjMostrado4 = false;
            msjMostrado5 = false;
            msjMostrado6 = false;
            msjMostrado7 = false;
            msjMostrado8 = false;
            msjMostrado9 = false;
            msjMostrado10 = false;
            msjMostrado11 = false;
            msjMostrado12 = true;
            msjMostrado13 = false;
            msjMostrado14 = false;
            msjMostrado15 = false;
            msjMostrado16 = false;
            msjMostrado17 = false;

            msjs[0].SetActive(false);
            msjs[1].SetActive(false);
            msjs[2].SetActive(false);
            msjs[3].SetActive(false);
            msjs[4].SetActive(false);
            msjs[5].SetActive(false);
            msjs[6].SetActive(false);
            msjs[7].SetActive(false);
            msjs[8].SetActive(false);
            msjs[9].SetActive(false);
            msjs[10].SetActive(false);
            msjs[11].SetActive(true);
            msjs[12].SetActive(false);
            msjs[13].SetActive(false);
            msjs[14].SetActive(false);
            msjs[15].SetActive(false);
            msjs[16].SetActive(false);
        }
        if (msj == 13 && !msjMostrado13) //L10
        {
            msjMostrado1 = false;
            msjMostrado2 = false;
            msjMostrado3 = false;
            msjMostrado4 = false;
            msjMostrado5 = false;
            msjMostrado6 = false;
            msjMostrado7 = false;
            msjMostrado8 = false;
            msjMostrado9 = false;
            msjMostrado10 = false;
            msjMostrado11 = false;
            msjMostrado12 = false;
            msjMostrado13 = true;
            msjMostrado14 = false;
            msjMostrado15 = false;
            msjMostrado16 = false;
            msjMostrado17 = false;

            msjs[0].SetActive(false);
            msjs[1].SetActive(false);
            msjs[2].SetActive(false);
            msjs[3].SetActive(false);
            msjs[4].SetActive(false);
            msjs[5].SetActive(false);
            msjs[6].SetActive(false);
            msjs[7].SetActive(false);
            msjs[8].SetActive(false);
            msjs[9].SetActive(false);
            msjs[10].SetActive(false);
            msjs[11].SetActive(false);
            msjs[12].SetActive(true);
            msjs[13].SetActive(false);
            msjs[14].SetActive(false);
            msjs[15].SetActive(false);
            msjs[16].SetActive(false);
        }
        if (msj == 14 && !msjMostrado14) //L11
        {
            msjMostrado1 = false;
            msjMostrado2 = false;
            msjMostrado3 = false;
            msjMostrado4 = false;
            msjMostrado5 = false;
            msjMostrado6 = false;
            msjMostrado7 = false;
            msjMostrado8 = false;
            msjMostrado9 = false;
            msjMostrado10 = false;
            msjMostrado11 = false;
            msjMostrado12 = false;
            msjMostrado13 = false;
            msjMostrado14 = true;
            msjMostrado15 = false;
            msjMostrado16 = false;
            msjMostrado17 = false;

            msjs[0].SetActive(false);
            msjs[1].SetActive(false);
            msjs[2].SetActive(false);
            msjs[3].SetActive(false);
            msjs[4].SetActive(false);
            msjs[5].SetActive(false);
            msjs[6].SetActive(false);
            msjs[7].SetActive(false);
            msjs[8].SetActive(false);
            msjs[9].SetActive(false);
            msjs[10].SetActive(false);
            msjs[11].SetActive(false);
            msjs[12].SetActive(false);
            msjs[13].SetActive(true);
            msjs[14].SetActive(false);
            msjs[15].SetActive(false);
            msjs[16].SetActive(false);
        }
        if (msj == 15 && !msjMostrado15) //L12
        {
            msjMostrado1 = false;
            msjMostrado2 = false;
            msjMostrado3 = false;
            msjMostrado4 = false;
            msjMostrado5 = false;
            msjMostrado6 = false;
            msjMostrado7 = false;
            msjMostrado8 = false;
            msjMostrado9 = false;
            msjMostrado10 = false;
            msjMostrado11 = false;
            msjMostrado12 = false;
            msjMostrado13 = false;
            msjMostrado14 = false;
            msjMostrado15 = true;
            msjMostrado16 = false;
            msjMostrado17 = false;

            msjs[0].SetActive(false);
            msjs[1].SetActive(false);
            msjs[2].SetActive(false);
            msjs[3].SetActive(false);
            msjs[4].SetActive(false);
            msjs[5].SetActive(false);
            msjs[6].SetActive(false);
            msjs[7].SetActive(false);
            msjs[8].SetActive(false);
            msjs[9].SetActive(false);
            msjs[10].SetActive(false);
            msjs[11].SetActive(false);
            msjs[12].SetActive(false);
            msjs[13].SetActive(false);
            msjs[14].SetActive(true);
            msjs[15].SetActive(false);
            msjs[16].SetActive(false);
        }
        if (msj == 16 && !msjMostrado16) //JUEGO COMPLETADO CON EXITO
        {
            msjMostrado1 = false;
            msjMostrado2 = false;
            msjMostrado3 = false;
            msjMostrado4 = false;
            msjMostrado5 = false;
            msjMostrado6 = false;
            msjMostrado7 = false;
            msjMostrado8 = false;
            msjMostrado9 = false;
            msjMostrado10 = false;
            msjMostrado11 = false;
            msjMostrado12 = false;
            msjMostrado13 = false;
            msjMostrado14 = false;
            msjMostrado15 = false;
            msjMostrado16 = true;
            msjMostrado17 = false;

            msjs[0].SetActive(false);
            msjs[1].SetActive(false);
            msjs[2].SetActive(false);
            msjs[3].SetActive(false);
            msjs[4].SetActive(false);
            msjs[5].SetActive(false);
            msjs[6].SetActive(false);
            msjs[7].SetActive(false);
            msjs[8].SetActive(false);
            msjs[9].SetActive(false);
            msjs[10].SetActive(false);
            msjs[11].SetActive(false);
            msjs[12].SetActive(false);
            msjs[13].SetActive(false);
            msjs[14].SetActive(false);
            msjs[15].SetActive(true);
            msjs[16].SetActive(false);

            Invoke("showScore", 20.0f);
        }
        if (msj == 17 && !msjMostrado17) //FIN DEL JUEGO
        {
            msjMostrado1 = false;
            msjMostrado2 = false;
            msjMostrado3 = false;
            msjMostrado4 = false;
            msjMostrado5 = false;
            msjMostrado6 = false;
            msjMostrado7 = false;
            msjMostrado8 = false;
            msjMostrado9 = false;
            msjMostrado10 = false;
            msjMostrado11 = false;
            msjMostrado12 = false;
            msjMostrado13 = false;
            msjMostrado14 = false;
            msjMostrado15 = false;
            msjMostrado16 = false;
            msjMostrado17 = true;

            msjs[0].SetActive(false);
            msjs[1].SetActive(false);
            msjs[2].SetActive(false);
            msjs[3].SetActive(false);
            msjs[4].SetActive(false);
            msjs[5].SetActive(false);
            msjs[6].SetActive(false);
            msjs[7].SetActive(false);
            msjs[8].SetActive(false);
            msjs[9].SetActive(false);
            msjs[10].SetActive(false);
            msjs[11].SetActive(false);
            msjs[12].SetActive(false);
            msjs[13].SetActive(false);
            msjs[14].SetActive(false);
            msjs[15].SetActive(false);
            msjs[16].SetActive(true);

            Invoke("showScore", 20.0f);
        }
    }

    void showScore()
    {
        //ScoreCliente.SetActive(true);

        bool fail = false;
        string bundleId = "com.ii.Launcher_BD"; // your target bundle id
        AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject ca = up.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject packageManager = ca.Call<AndroidJavaObject>("getPackageManager");

        AndroidJavaObject launchIntent = null;
        try
        {
            launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", bundleId);
        }
        catch (System.Exception e)
        {
            fail = true;
        }

        if (fail)
        { //open app in store
            //Application.OpenURL("https://google.com");
        }
        else //open the app
            ca.Call("startActivity", launchIntent);

        up.Dispose();
        ca.Dispose();
        packageManager.Dispose();
        launchIntent.Dispose();

        //CIERRA EL JUEGO
        AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call("finish");

        System.Diagnostics.Process.GetCurrentProcess().Kill();
    }
}
