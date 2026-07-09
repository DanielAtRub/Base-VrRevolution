using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetParent : NetworkBehaviour
{
    [SerializeField]
    private Transform NetPadre;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void OnStartClient()
    {
        transform.SetParent(NetPadre);
    }
}
