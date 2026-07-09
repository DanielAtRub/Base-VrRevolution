using UnityEngine;
using Mirror;
using TMPro;//DEBUG

public class ConectClienteQ2 : MonoBehaviour
{
    [SerializeField]
    private GameObject ManagerHud;
    [SerializeField]
    private GameObject t_offline, t_loading;
    [SerializeField]
    private GameObject LanzaApp;

    [SerializeField]
    private TextMeshPro textConect; 

    //private bool trig;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!ManagerHud)
            ManagerHud = GameObject.Find("NetworkManager");
        else
        {
            if (OVRPlugin.userPresent)
            {
                //Mounted();
                if (PlayerPrefs.GetInt("trig") != 1) //PRUEBAS
                {
                    if (!NetworkClient.isConnected)
                    {
                        PlayerPrefs.SetInt("trig", 1);
                        LanzaApp.GetComponent<EcesuteAppJUEGO>().ExeAppJuego();
                        cierra();
                    }
                }
                Invoke("Mounted", 2.0f);
            }
            else
            {
                //UnMounted();
                PlayerPrefs.SetInt("trig", 0); //PRUEBAS
                //if (trig)
                    Invoke("UnMounted", 2.0f);
            }
        }

        //if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
            //UnMounted();
    }

    void Mounted()
    {
        //trig = true;//PRUEBAS

        ManagerHud.GetComponent<NetworkManagerHUD>().QuestC = true;
        t_offline.SetActive(false);
        t_loading.SetActive(true);

        if (NetworkClient.isConnected) //DEBUG
            textConect.text = "Loading...";
        else
            textConect.text = "";
    }
    void UnMounted()
    {
        ManagerHud.GetComponent<NetworkManagerHUD>().QuestC = false;
        t_offline.SetActive(true);
        t_loading.SetActive(false);
        /*
        if (!NetworkClient.isConnected) //DEBUG
        {
            LanzaApp.GetComponent<EcesuteAppJUEGO>().ExeAppJuego();
            cierra();
        }
        */
    }
    
    void cierra()
    {
        using (AndroidJavaClass javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject unityActivity = javaClass.GetStatic<AndroidJavaObject>("currentActivity");
            unityActivity.Call<bool>("moveTaskToBack", false);
        }
        AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call("finish");
    }
   
}