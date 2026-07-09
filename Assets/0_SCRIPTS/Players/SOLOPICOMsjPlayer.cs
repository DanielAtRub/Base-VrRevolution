using UnityEngine;
using Mirror;

public class SOLOPICOMsjPlayer : NetworkBehaviour
{
    [SerializeField]
    private GameObject GameManager;
    public int msj;
    [SerializeField]
    private GameObject[] msjs;

    [SerializeField]
    private bool msjMostrado1, msjMostrado2, msjMostrado3, msjMostrado4, msjMostrado5, msjMostrado6,
        msjMostrado7, msjMostrado8, msjMostrado9;

    [SerializeField]
    private GameObject ScoreCliente;
    [SerializeField]
    private string targetPackage;
    [SerializeField]
    private float tiempoScore;

    [Header("A NEGRO - ENTRE LEVELS")]
    [SerializeField]
    private Camera camara;
    [SerializeField]
    private LayerMask mascaraMINIMO;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // for local player
        if (!isLocalPlayer) return;

        if (!GameManager)
            GameManager = GameObject.Find("GameManager");

        msj = GameManager.GetComponent<JuegoManager>().MsjServerToPlayers;

        if (msj == 1 && !msjMostrado1) //UBICACION L0-L1
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

            msjs[0].SetActive(true);
            msjs[1].SetActive(false);
            msjs[2].SetActive(false);
            msjs[3].SetActive(false);
            msjs[4].SetActive(false);
            msjs[5].SetActive(false);
            msjs[6].SetActive(false);
            msjs[7].SetActive(false);
            msjs[8].SetActive(false);
        }
        if (msj == 2 && !msjMostrado2) //LEVEL 1
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

            msjs[0].SetActive(false);
            msjs[1].SetActive(true);
            msjs[2].SetActive(false);
            msjs[3].SetActive(false);
            msjs[4].SetActive(false);
            msjs[5].SetActive(false);
            msjs[6].SetActive(false);
            msjs[7].SetActive(false);
            msjs[8].SetActive(false);
        }
        if (msj == 3 && !msjMostrado3) //CARAMELIZA
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

            msjs[0].SetActive(false);
            msjs[1].SetActive(false);
            msjs[2].SetActive(true);
            msjs[3].SetActive(false);
            msjs[4].SetActive(false);
            msjs[5].SetActive(false);
            msjs[6].SetActive(false);
            msjs[7].SetActive(false);
            msjs[8].SetActive(false);
        }
        if (msj == 4 && !msjMostrado4) //UBICACION L1-L2
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

            msjs[0].SetActive(false);
            msjs[1].SetActive(false);
            msjs[2].SetActive(false);
            msjs[3].SetActive(true);
            msjs[4].SetActive(false);
            msjs[5].SetActive(false);
            msjs[6].SetActive(false);
            msjs[7].SetActive(false);
            msjs[8].SetActive(false);
        }
        if (msj == 5 && !msjMostrado5) //LEVEL 2
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

            msjs[0].SetActive(false);
            msjs[1].SetActive(false);
            msjs[2].SetActive(false);
            msjs[3].SetActive(false);
            msjs[4].SetActive(true);
            msjs[5].SetActive(false);
            msjs[6].SetActive(false);
            msjs[7].SetActive(false);
            msjs[8].SetActive(false);
        }
        if (msj == 6 && !msjMostrado6) //CIERRA PORTAL
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

            msjs[0].SetActive(false);
            msjs[1].SetActive(false);
            msjs[2].SetActive(false);
            msjs[3].SetActive(false);
            msjs[4].SetActive(false);
            msjs[5].SetActive(true);
            msjs[6].SetActive(false);
            msjs[7].SetActive(false);
            msjs[8].SetActive(false);
        }
        if (msj == 7 && !msjMostrado7) //JUEGO COMPLETADO
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

            msjs[0].SetActive(false);
            msjs[1].SetActive(false);
            msjs[2].SetActive(false);
            msjs[3].SetActive(false);
            msjs[4].SetActive(false);
            msjs[5].SetActive(false);
            msjs[6].SetActive(true);
            msjs[7].SetActive(false);
            msjs[8].SetActive(false);

            Invoke("showScore", 10.0f);
        }
        if (msj == 8 && !msjMostrado8) //GAME OVER
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

            msjs[0].SetActive(false);
            msjs[1].SetActive(false);
            msjs[2].SetActive(false);
            msjs[3].SetActive(false);
            msjs[4].SetActive(false);
            msjs[5].SetActive(false);
            msjs[6].SetActive(false);
            msjs[7].SetActive(true);
            msjs[8].SetActive(false);

            Invoke("showScore", 10.0f);
        }
        if (msj == 9 && !msjMostrado9) //CAPTURA USER
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

            msjs[0].SetActive(false);
            msjs[1].SetActive(false);
            msjs[2].SetActive(false);
            msjs[3].SetActive(false);
            msjs[4].SetActive(false);
            msjs[5].SetActive(false);
            msjs[6].SetActive(false);
            msjs[7].SetActive(false);
            msjs[8].SetActive(true);
        }
    }

    void showScore()
    {
        ScoreCliente.SetActive(true);
        Invoke("abreLauncher", tiempoScore);
    }

    void abreLauncher()
    {
        try
        {
            string activityName = "com.unity3d.player.UnityPlayerActivity";

            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent");
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");

            intent.Call<AndroidJavaObject>("setClassName", targetPackage, activityName);
            intent.Call<AndroidJavaObject>("addFlags", intentClass.GetStatic<int>("FLAG_ACTIVITY_NEW_TASK"));

            currentActivity.Call("startActivity", intent);

            currentActivity.Call("finish");

            AndroidJavaClass process = new AndroidJavaClass("android.os.Process");
            int pid = process.CallStatic<int>("myPid");
            process.CallStatic("killProcess", pid);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error launching app: " + e.Message);
        }
    }

}
