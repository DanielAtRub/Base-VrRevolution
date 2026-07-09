using UnityEngine;

public class StopPartida : MonoBehaviour
{
    private GameObject netGui;

    // Start is called before the first frame update
    void Start()
    { 
    }

    public void ParaPartida()
    {
        //PARA PARTIDA
        netGui = GameObject.Find("NetworkManager");
        netGui.GetComponent<Mirror.NetworkManagerHUD>().StopButtons();
        //GUARDA SCORES

    }
}
