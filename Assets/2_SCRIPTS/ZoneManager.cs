using UnityEngine;
using Mirror;
using NodeCanvas.BehaviourTrees;
using System.Collections.Generic;

public class ZoneManager : NetworkBehaviour
{
    [SerializeField]
    private bool haveTime;
    [SerializeField]
    private float ZoneTime;
    public bool ZonaFinalizada;

    //[SerializeField]
    //private GameObject Behaviour;

    void Start()
    {
    }

    [ServerCallback]
    void Update()
    {
        //ACT ZONA AL FINALIZAR EL TIEMPO
        if (haveTime)
        {
            ZoneTime -= Time.deltaTime;
            if (ZoneTime <= 0)
            {
                ZoneTime = 0;
                //if (Behaviour)
                    //Behaviour.GetComponent<BehaviourTreeOwner>().enabled = true;
                /*var found = new List<Transform>();
                foreach (Transform t in transform)
                {
                    t.gameObject.GetComponent<Enemigo0>().EnemyHealth = 0; //PRUEBAS
                    t.gameObject.GetComponent<Enemigo0>().destruir = true;  //PRUEBAS
                    found.Add(t);
                }*/
                ZonaFinalizada = true;
            }
        }
    }
    
}
