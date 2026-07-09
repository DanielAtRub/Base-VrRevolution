using UnityEngine;
using Mirror;
//using TMPro;//DEBUG

public class ConectCliente : MonoBehaviour
{
    [SerializeField]
    private GameObject ManagerHud;
    [SerializeField]
    private GameObject t_offline, t_loading;

    //[SerializeField]
    //private TextMeshPro textDebug; //DEBUG

    [SerializeField]
    private GameObject LanzaApp;

    private bool trig;

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
                //Mounted();
                Invoke("Mounted", 2.0f);
            else
                //UnMounted();
                Invoke("UnMounted", 2.0f);
        }
    }

    void Mounted()
    {
        ManagerHud.GetComponent<NetworkManagerHUD>().QuestC = true;
        t_offline.SetActive(false);
        t_loading.SetActive(true);
        
        if (NetworkClient.isConnected && !trig) //DEBUG
        {
            trig = true;
            //textDebug.text = "SI";
            LanzaApp.GetComponent<EcesuteAppJUEGO>().ExeAppJuego();
        }
        
    }
    void UnMounted()
    {
        ManagerHud.GetComponent<NetworkManagerHUD>().QuestC = false;
        t_offline.SetActive(true);
        t_loading.SetActive(false);
        /*
        trig = false;
        textDebug.text = "NO";
        */
    }
}