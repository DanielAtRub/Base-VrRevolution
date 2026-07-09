using UnityEngine;
using Mirror;
using TMPro;

public class IdPlayer : NetworkBehaviour
{
    public uint idPlayerEntrada;//se metera desde las oculus no sera necesario que sea public
    [SerializeField]
    private TextMeshPro textId;
    public uint idPlayer;

    // Start is called before the first frame update
    void Start()
    {
        // for local player
        if (!isLocalPlayer) return;

        CmdIdPlayer(idPlayerEntrada);
    }

    //LO QUE HAY DENTRO DEL COMMAND ES DEL SERVIDOR
    [Command]
    void CmdIdPlayer(uint _idPlayer)
    {
        idPlayer = _idPlayer;
        textId.gameObject.SetActive(true);
        textId.text = "id: " + _idPlayer.ToString();
    }

}
