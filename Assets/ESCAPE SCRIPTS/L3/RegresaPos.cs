using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RegresaPos : NetworkBehaviour
{
    [SerializeField]
    private Vector3 PosOriginal;
    [SerializeField]
    private Quaternion RotOriginal;
    [SyncVar]
    public bool InSnap;

    // Start is called before the first frame update
    [Server]
    void Start()
    {
        PosOriginal = transform.position;
    }

    [Server]
    public void RegresaPosOriginal()
    {
        if (!InSnap)
        {
            //MUEVE A POS ORIGINAL
            //transform.position = Vector3.MoveTowards(transform.position, PosOriginal, 3f); //DEBE ESTAR EN UPDATE
            transform.position = PosOriginal;
            transform.rotation = RotOriginal;
            InSnap = false;

            //Debug.Log("Pos Original: " + PosOriginal + " Pos: " + transform.position);
        }
    }
}
