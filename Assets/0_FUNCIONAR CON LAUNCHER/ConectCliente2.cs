using UnityEngine;
using Mirror;
using TMPro;

public class ConectCliente2 : MonoBehaviour
{
    [SerializeField]
    private GameObject ManagerHud;
    [SerializeField]
    private TextMeshPro textConect;

    bool mountedCalled;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        if (!ManagerHud)
        {
            ManagerHud = GameObject.Find("NetworkManager");
            return;
        }

        if (OVRPlugin.userPresent && !mountedCalled)
        {
            mountedCalled = true;
            Mounted();
        }

        if (!OVRPlugin.userPresent && mountedCalled)
        {
            mountedCalled = false;
            UnMounted();
        }
    }

    void Mounted()
    {
        ManagerHud.GetComponent<NetworkManagerHUD>().QuestC = true;

        if (NetworkClient.isConnected) //DEBUG
            textConect.text = "Loading...";
        else
            textConect.text = "";
    }

    void UnMounted()
    {
        ManagerHud.GetComponent<NetworkManagerHUD>().QuestC = false;
        textConect.text = "";
    }
}