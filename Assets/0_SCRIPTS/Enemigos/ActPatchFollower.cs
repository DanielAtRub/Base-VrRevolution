using UnityEngine;
using Mirror;

public class ActPatchFollower : NetworkBehaviour
{
    public override void OnStartServer()
    {
        if (isServer)
          servidor();
    }

    void Start()
    {
    }

    void Update()  //OJO CON LA POSICION DEL UPDATE, ESTAR DONDE DEBE
    {
    }
    
    [Server]
    void servidor()
    {
        //ACTIVA EN EL SERVIDOR
        GetComponent<PathCreation.Examples.PathFollower>().enabled = true;
    }
}
