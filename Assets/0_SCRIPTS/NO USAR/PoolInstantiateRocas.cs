using UnityEngine;
using Mirror;
using System.Collections.Generic;

public class PoolInstantiateRocas : NetworkBehaviour
{
    public bool haveTime;
    public float OrdaTime;
    public bool OrdaKilled;

    void Start()
    {
    }
    
    [ServerCallback]
    void Update()
    {
        if (haveTime)
        {
            OrdaTime -= Time.deltaTime;
            if (OrdaTime <= 0)
            {
                OrdaTime = 0;
                var found = new List<Transform>();
                foreach (Transform t in transform)
                {
                    t.gameObject.GetComponent<Rocas>().destruir = true;
                    found.Add(t);
                }
                OrdaKilled = true;
            }
        }
    }
    
    /*
    [Server]
    public void MataOrdaParcial()
    {
        var found = new List<Transform>();
        foreach (Transform t in transform)
        {
            t.gameObject.GetComponent<Rocas>().destruir = true;
            found.Add(t);
        }
    }

    [Server]
    public void MataOrdaTotal()
    {
        var found = new List<Transform>();
        foreach (Transform t in transform)
        {
            t.gameObject.GetComponent<Rocas>().destruir = true;
            found.Add(t);
        }
        OrdaKilled = true;
    }
    */
}
